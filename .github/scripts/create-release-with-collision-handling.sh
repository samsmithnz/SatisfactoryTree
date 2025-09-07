#!/bin/bash

# Script to create a GitHub release with automatic tag collision handling
# This script will check if a tag exists and increment the patch version if there's a collision

set -e

# Input parameters
BASE_VERSION="$1"
GITHUB_TOKEN="$2"
GITHUB_REPOSITORY="$3"

if [ -z "$BASE_VERSION" ] || [ -z "$GITHUB_TOKEN" ] || [ -z "$GITHUB_REPOSITORY" ]; then
    echo "Usage: $0 <base_version> <github_token> <github_repository>"
    echo "Example: $0 2.0.26 \$GITHUB_TOKEN samsmithnz/SatisfactoryTree"
    exit 1
fi

echo "Starting release creation with base version: $BASE_VERSION"
echo "Repository: $GITHUB_REPOSITORY"

# Validate version format
if ! [[ "$BASE_VERSION" =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    echo "❌ Invalid version format. Expected: major.minor.patch (e.g., 2.0.26)"
    exit 1
fi

# Function to check if a tag exists
check_tag_exists() {
    local tag="$1"
    local response
    local http_code
    
    echo "Checking if tag $tag exists..."
    response=$(curl -s -o /dev/null -w "%{http_code}" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        "https://api.github.com/repos/$GITHUB_REPOSITORY/git/refs/tags/$tag" 2>/dev/null)
    
    http_code="$response"
    
    case "$http_code" in
        "200")
            echo "Tag $tag exists"
            return 0  # Tag exists
            ;;
        "404")
            echo "Tag $tag does not exist"
            return 1  # Tag doesn't exist
            ;;
        "403")
            echo "⚠️  API rate limit or authentication issue (HTTP 403)"
            return 2  # Rate limit or auth issue
            ;;
        *)
            echo "⚠️  Unexpected API response: HTTP $http_code"
            return 3  # Other error
            ;;
    esac
}

# Function to create a release
create_release() {
    local version="$1"
    local tag="v$version"
    
    echo "Attempting to create release with tag: $tag"
    
    local response
    response=$(curl -s -w "\nHTTP_STATUS:%{http_code}" \
        -X POST \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        "https://api.github.com/repos/$GITHUB_REPOSITORY/releases" \
        -d "{
            \"tag_name\": \"$tag\",
            \"name\": \"$tag\",
            \"draft\": false,
            \"prerelease\": false
        }" 2>/dev/null)
    
    local http_status
    http_status=$(echo "$response" | tail -n1 | sed 's/.*HTTP_STATUS://')
    local body
    body=$(echo "$response" | sed '$d')
    
    case "$http_status" in
        "201")
            echo "✅ Successfully created release $tag"
            if command -v jq >/dev/null 2>&1; then
                echo "$body" | jq -r '.html_url' 2>/dev/null || echo "Release created but couldn't parse URL"
            fi
            return 0
            ;;
        "422")
            echo "❌ Release creation failed - tag might already exist (HTTP 422)"
            echo "$body"
            return 1
            ;;
        "403")
            echo "❌ Authentication or rate limit issue (HTTP 403)"
            echo "$body"
            return 2
            ;;
        *)
            echo "❌ Failed to create release $tag (HTTP $http_status)"
            echo "$body"
            return 1
            ;;
    esac
}

# Function to increment patch version
increment_patch_version() {
    local version="$1"
    # Split version into major.minor.patch
    local major minor patch
    IFS='.' read -r major minor patch <<< "$version"
    
    # Validate that all parts are numbers
    if ! [[ "$major" =~ ^[0-9]+$ ]] || ! [[ "$minor" =~ ^[0-9]+$ ]] || ! [[ "$patch" =~ ^[0-9]+$ ]]; then
        echo "❌ Invalid version format: $version"
        return 1
    fi
    
    # Increment patch version
    patch=$((patch + 1))
    
    echo "$major.$minor.$patch"
}

# Main logic
current_version="$BASE_VERSION"
max_attempts=10
attempt=1

echo "Starting release creation process..."

while [ $attempt -le $max_attempts ]; do
    tag="v$current_version"
    
    echo ""
    echo "═══ Attempt $attempt/$max_attempts ═══"
    
    # Check if tag exists
    tag_check_result=0
    check_tag_exists "$tag" || tag_check_result=$?
    
    case $tag_check_result in
        0)
            # Tag exists, increment version
            echo "⚠️  Tag $tag already exists, incrementing version..."
            current_version=$(increment_patch_version "$current_version")
            if [ $? -ne 0 ]; then
                echo "❌ Failed to increment version"
                exit 1
            fi
            attempt=$((attempt + 1))
            ;;
        1)
            # Tag doesn't exist, try to create release
            echo "✅ Tag $tag is available, attempting to create release..."
            if create_release "$current_version"; then
                echo "🎉 Release created successfully with version $current_version"
                exit 0
            else
                create_result=$?
                if [ $create_result -eq 2 ]; then
                    echo "❌ Authentication/rate limit issue, aborting"
                    exit 1
                else
                    echo "❌ Failed to create release, this might be a race condition"
                    current_version=$(increment_patch_version "$current_version")
                    if [ $? -ne 0 ]; then
                        echo "❌ Failed to increment version"
                        exit 1
                    fi
                    attempt=$((attempt + 1))
                fi
            fi
            ;;
        2|3)
            # API error, wait and retry
            echo "⚠️  API error, waiting before retry..."
            sleep 5
            attempt=$((attempt + 1))
            ;;
    esac
    
    if [ $attempt -le $max_attempts ]; then
        echo "Waiting 2 seconds before next attempt..."
        sleep 2
    fi
done

echo ""
echo "❌ Failed to create release after $max_attempts attempts"
echo "Last attempted version: $current_version"
exit 1