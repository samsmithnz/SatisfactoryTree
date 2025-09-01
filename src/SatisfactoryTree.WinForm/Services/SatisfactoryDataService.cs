using SatisfactoryTree.DataAccess;
using SatisfactoryTree.Models;

namespace SatisfactoryTree.WinForm.Services
{
    public class SatisfactoryDataService
    {
        private static SatisfactoryDataService? _instance;
        private NewContent? _content;

        private SatisfactoryDataService()
        {
            LoadContent();
        }

        public static SatisfactoryDataService Instance
        {
            get
            {
                _instance ??= new SatisfactoryDataService();
                return _instance;
            }
        }

        private void LoadContent()
        {
            try
            {
                _content = FileContent.LoadJsonContent();
            }
            catch (Exception ex)
            {
                // Fallback to empty content if loading fails
                _content = new NewContent();
                System.Diagnostics.Debug.WriteLine($"Failed to load content: {ex.Message}");
            }
        }

        public List<NewItem> GetAllItems()
        {
            return _content?.Items ?? new List<NewItem>();
        }

        public List<NewRecipe> GetAllRecipes()
        {
            return _content?.Recipes ?? new List<NewRecipe>();
        }

        public List<NewRecipe> GetRecipesForItem(string itemClassName)
        {
            if (_content?.Recipes == null) return new List<NewRecipe>();

            return _content.Recipes
                .Where(r => r.Products?.Any(p => p.Key == itemClassName) == true)
                .ToList();
        }

        public NewItem? GetItemByClassName(string className)
        {
            return _content?.Items?.FirstOrDefault(i => i.ClassName == className);
        }

        public NewItem? GetItemByDisplayName(string displayName)
        {
            return _content?.Items?.FirstOrDefault(i => i.DisplayName == displayName);
        }

        public NewRecipe? GetRecipeByClassName(string className)
        {
            return _content?.Recipes?.FirstOrDefault(r => r.ClassName == className);
        }

        public List<string> GetItemDisplayNames()
        {
            return _content?.Items?
                .Where(i => !string.IsNullOrEmpty(i.DisplayName))
                .Select(i => i.DisplayName!)
                .OrderBy(name => name)
                .ToList() ?? new List<string>();
        }

        public Dictionary<string, decimal> GetRecipeInputRequirements(NewRecipe recipe, decimal targetQuantity)
        {
            var requirements = new Dictionary<string, decimal>();
            
            if (recipe.Ingredients == null || recipe.Products == null) 
                return requirements;

            // Calculate how much this recipe produces per minute
            var outputPerMinute = CalculateOutputPerMinute(recipe);
            if (outputPerMinute <= 0) return requirements;

            // Calculate how many recipes we need to run to get the target quantity per minute
            var recipesNeeded = targetQuantity / outputPerMinute;

            // Calculate input requirements
            foreach (var ingredient in recipe.Ingredients)
            {
                if (ingredient.Key != null)
                {
                    var itemDisplayName = GetItemByClassName(ingredient.Key)?.DisplayName ?? ingredient.Key;
                    var requiredPerMinute = ingredient.Value * recipesNeeded * (60m / recipe.ManufactoringDuration);
                    requirements[itemDisplayName] = requiredPerMinute;
                }
            }

            return requirements;
        }

        public decimal CalculateOutputPerMinute(NewRecipe recipe)
        {
            if (recipe.Products == null || !recipe.Products.Any() || recipe.ManufactoringDuration <= 0)
                return 0;

            // Use the first product's output rate
            var primaryOutput = recipe.Products.First();
            return primaryOutput.Value * (60m / recipe.ManufactoringDuration);
        }

        public decimal CalculateBuildingsRequired(NewRecipe recipe, decimal targetQuantityPerMinute)
        {
            var outputPerMinute = CalculateOutputPerMinute(recipe);
            if (outputPerMinute <= 0) return 0;

            return Math.Ceiling(targetQuantityPerMinute / outputPerMinute);
        }

        public string GetBuildingDisplayName(string? producedIn)
        {
            // Map building class names to display names
            var buildingMap = new Dictionary<string, string>
            {
                { "ConstructorMk1", "Constructor" },
                { "AssemblerMk1", "Assembler" },
                { "FoundryMk1", "Foundry" },
                { "ManufacturerMk1", "Manufacturer" },
                { "RefineryMk1", "Refinery" },
                { "SmelterMk1", "Smelter" },
                { "BuildGun", "Build Gun" },
                { "WorkBenchComponent", "Equipment Workshop" }
            };

            return buildingMap.ContainsKey(producedIn ?? "") ? buildingMap[producedIn!] : producedIn ?? "Unknown";
        }

        public bool IsRawMaterial(string itemDisplayName)
        {
            var item = GetItemByDisplayName(itemDisplayName);
            if (item?.ClassName == null) return true; // Assume unknown items are raw materials

            var recipes = GetRecipesForItem(item.ClassName);
            
            // If no recipes produce this item, it's likely a raw material
            if (!recipes.Any()) return true;

            // Check if all recipes for this item have no inputs (raw material recipes)
            return recipes.All(recipe => recipe.Ingredients == null || !recipe.Ingredients.Any());
        }

        public NewRecipe? GetPrimaryRecipeForItem(string itemDisplayName)
        {
            var item = GetItemByDisplayName(itemDisplayName);
            if (item?.ClassName == null) return null;

            var recipes = GetRecipesForItem(item.ClassName);
            
            // Prefer non-alternate recipes, then take the first one
            return recipes.FirstOrDefault(r => !r.IsAlternateRecipe) ?? recipes.FirstOrDefault();
        }

        public List<KeyValuePair<string, decimal>> GetRecipeInputsWithQuantities(NewRecipe recipe, decimal targetQuantityPerMinute)
        {
            var inputs = new List<KeyValuePair<string, decimal>>();
            
            if (recipe.Ingredients == null || recipe.Products == null) 
                return inputs;

            // Calculate how much this recipe produces per minute
            var outputPerMinute = CalculateOutputPerMinute(recipe);
            if (outputPerMinute <= 0) return inputs;

            // Calculate how many recipes we need to run to get the target quantity per minute
            var recipesNeeded = targetQuantityPerMinute / outputPerMinute;

            // Calculate input requirements
            foreach (var ingredient in recipe.Ingredients)
            {
                if (ingredient.Key != null)
                {
                    var itemDisplayName = GetItemByClassName(ingredient.Key)?.DisplayName ?? ingredient.Key;
                    var requiredPerMinute = ingredient.Value * recipesNeeded * (60m / recipe.ManufactoringDuration);
                    inputs.Add(new KeyValuePair<string, decimal>(itemDisplayName, requiredPerMinute));
                }
            }

            return inputs;
        }
    }
}