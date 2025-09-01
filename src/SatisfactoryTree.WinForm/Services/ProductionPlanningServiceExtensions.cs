using SatisfactoryTree.Models;
using SatisfactoryTree.Services;

namespace SatisfactoryTree.WinForm.Services
{
    public static class ProductionPlanningServiceExtensions
    {
        public static ProductionGoal AddProductionGoalWithDependencies(this ProductionPlanningService service, string itemName, decimal targetQuantity, string factoryId = "default", bool autoDependencies = false, string? selectedRecipeClassName = null)
        {
            var factory = service.GetFactory(factoryId);
            if (factory == null)
            {
                throw new ArgumentException($"Factory with ID '{factoryId}' not found.");
            }

            var goal = factory.AddProductionGoal(itemName, targetQuantity);
            goal.RecipeClassName = selectedRecipeClassName;

            // Auto-populate dependencies if requested
            if (autoDependencies)
            {
                var dataService = SatisfactoryDataService.Instance;
                if (!dataService.IsRawMaterial(itemName))
                {
                    CreateProductionDependencies(goal, factoryId, dataService);
                }
            }

            return goal;
        }

        private static void CreateProductionDependencies(ProductionGoal parentGoal, string factoryId, SatisfactoryDataService dataService, HashSet<string>? processedItems = null)
        {
            // Prevent infinite loops by tracking processed items
            processedItems ??= new HashSet<string>();
            
            if (processedItems.Contains(parentGoal.ItemName))
            {
                return; // Circular dependency detected, skip
            }
            
            processedItems.Add(parentGoal.ItemName);

            // Skip if this is a raw material
            if (dataService.IsRawMaterial(parentGoal.ItemName))
            {
                return;
            }

            // Get the recipe for this item
            var recipe = string.IsNullOrEmpty(parentGoal.RecipeClassName) 
                ? dataService.GetPrimaryRecipeForItem(parentGoal.ItemName)
                : dataService.GetRecipeByClassName(parentGoal.RecipeClassName);

            if (recipe == null)
            {
                return;
            }

            // Get required inputs for this production goal
            var inputs = dataService.GetRecipeInputsWithQuantities(recipe, parentGoal.TargetQuantity);

            foreach (var input in inputs)
            {
                var inputItemName = input.Key;
                var requiredQuantity = input.Value;

                // Create dependent production goal
                var dependentGoal = new ProductionGoal(inputItemName, requiredQuantity, factoryId)
                {
                    ProduceInternally = !dataService.IsRawMaterial(inputItemName)
                };

                parentGoal.AddDependentGoal(dependentGoal);

                // Recursively create dependencies for this input if it's not a raw material
                if (!dataService.IsRawMaterial(inputItemName))
                {
                    CreateProductionDependencies(dependentGoal, factoryId, dataService, new HashSet<string>(processedItems));
                }
            }
        }

        public static void RecreateProductionDependencies(this ProductionPlanningService service, string goalId)
        {
            var goal = service.GetAllActiveGoals().FirstOrDefault(g => g.Id == goalId);
            if (goal != null)
            {
                var dataService = SatisfactoryDataService.Instance;
                
                if (goal.ProduceInternally && !dataService.IsRawMaterial(goal.ItemName))
                {
                    // Re-create dependencies if switching to internal production
                    goal.DependentGoals.Clear();
                    CreateProductionDependencies(goal, goal.FactoryId, dataService);
                }
                else if (!goal.ProduceInternally)
                {
                    // Clear dependencies if switching to import
                    goal.DependentGoals.Clear();
                }
            }
        }
    }
}