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

# Function to check if a tag exists
check_tag_exists() {
    local tag="$1"
    local response
    response=$(curl -s -o /dev/null -w "%{http_code}" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        "https://api.github.com/repos/$GITHUB_REPOSITORY/git/refs/tags/$tag")
    
    if [ "$response" = "200" ]; then
        return 0  # Tag exists
    else
        return 1  # Tag doesn't exist
    fi
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
        }")
    
    local http_status
    http_status=$(echo "$response" | tail -n1 | sed 's/.*HTTP_STATUS://')
    local body
    body=$(echo "$response" | sed '$d')
    
    if [ "$http_status" = "201" ]; then
        echo "✅ Successfully created release $tag"
        echo "$body" | jq -r '.html_url'
        return 0
    else
        echo "❌ Failed to create release $tag (HTTP $http_status)"
        echo "$body"
        return 1
    fi
}

# Function to increment patch version
increment_patch_version() {
    local version="$1"
    # Split version into major.minor.patch
    local major minor patch
    IFS='.' read -r major minor patch <<< "$version"
    
    # Increment patch version
    patch=$((patch + 1))
    
    echo "$major.$minor.$patch"
}

# Main logic
current_version="$BASE_VERSION"
max_attempts=10
attempt=1

while [ $attempt -le $max_attempts ]; do
    tag="v$current_version"
    
    echo "Attempt $attempt: Checking if tag $tag exists..."
    
    if check_tag_exists "$tag"; then
        echo "⚠️  Tag $tag already exists, incrementing version..."
        current_version=$(increment_patch_version "$current_version")
        attempt=$((attempt + 1))
    else
        echo "✅ Tag $tag is available, creating release..."
        if create_release "$current_version"; then
            echo "🎉 Release created successfully with version $current_version"
            exit 0
        else
            echo "❌ Failed to create release, this might be a race condition"
            current_version=$(increment_patch_version "$current_version")
            attempt=$((attempt + 1))
        fi
    fi
    
    if [ $attempt -le $max_attempts ]; then
        echo "Waiting 2 seconds before next attempt..."
        sleep 2
    fi
done

echo "❌ Failed to create release after $max_attempts attempts"
exit 1