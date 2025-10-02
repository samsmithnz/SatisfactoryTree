using SatisfactoryTree.Logic;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services
{
    public class PlanService
    {
        private Plan? _plan;
        private FactoryCatalog? _factoryCatalog;
        private int? _lastAddedFactoryId; // track last added factory

        public event Action? PlanChanged;

        public Plan? Plan
        {
            get => _plan;
            set
            {
                _plan = value;
                PlanChanged?.Invoke();
            }
        }

        public FactoryCatalog? FactoryCatalog
        {
            get
            {
                return _factoryCatalog;
            }
            set
            {
                _factoryCatalog = value;
            }
        }

        public bool HasPlan
        {
            get
            {
                return _plan != null && _plan.Factories.Any();
            }
        }

        public int? LastAddedFactoryId
        {
            get
            {
                return _lastAddedFactoryId;
            }
        }

        public void ClearLastAddedFactory()
        {
            _lastAddedFactoryId = null;
        }

        public void AddFactory()
        {
            if (_plan == null)
            {
                _plan = new Plan();
            }

            // Find the next available ID
            int nextId;
            if (_plan.Factories.Any())
            {
                nextId = _plan.Factories.Max(f => f.Id) + 1;
            }
            else
            {
                nextId = 1;
            }

            // Create a new factory with the default name pattern
            string factoryName = $"Factory {nextId}";
            Factory newFactory = new(nextId, factoryName);

            _plan.Factories.Add(newFactory);
            _lastAddedFactoryId = newFactory.Id; // mark for scrolling

            // Notify listeners so the new factory renders
            PlanChanged?.Invoke();
        }

        public void AddExportedPartToFactory(int factoryId, string itemName, double quantity)
        {
            if (_plan == null || _factoryCatalog == null)
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            // Check if this exported part already exists
            ExportedItem? existingExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == itemName);
            if (existingExport != null)
            {
                // Update existing quantity
                existingExport.Item.Quantity = quantity;
            }
            else
            {
                // Add new exported part
                factory.ExportedParts.Add(new(new Item { Name = itemName, Quantity = quantity }));
            }

            // Recalculate the entire plan
            RefreshPlanCalculations();
        }

        public void RemoveExportedPartFromFactory(int factoryId, string itemName)
        {
            if (_plan == null)
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            ExportedItem? exportToRemove = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == itemName);
            if (exportToRemove != null)
            {
                factory.ExportedParts.Remove(exportToRemove);

                // Recalculate the entire plan
                RefreshPlanCalculations();
            }
        }

        public void AddImportedPartToFactory(int factoryId, int sourceFactoryId, string sourceFactoryName, string itemName, double quantity)
        {
            if (_plan == null)
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            // Add or update imported part
            ImportedItem importedItem = new(sourceFactoryId, sourceFactoryName, new Item { Name = itemName, Quantity = quantity });
            factory.ImportedParts[sourceFactoryId] = importedItem;

            // Recalculate the entire plan
            RefreshPlanCalculations();
        }

        public void RefreshPlanCalculations()
        {
            if (_plan == null || _factoryCatalog == null)
            {
                return;
            }

            try
            {
                // Recalculate the plan
                _plan.UpdatePlanCalculations(_factoryCatalog);

                // Notify UI that plan has changed
                PlanChanged?.Invoke();
            }
            catch (Exception ex)
            {
                // Log error or handle it appropriately
                Console.WriteLine($"Error updating plan calculations: {ex.Message}");
            }
        }

        public void AddAllMissingIngredients(int factoryId)
        {
            if (_plan == null || _factoryCatalog == null)
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            // Get all missing ingredients for this factory
            var missingIngredients = GetMissingIngredients(factoryId);

            // Add missing ingredients to factory using Calculator
            AddIngredientsToFactory(factory, missingIngredients);
            
            // Notify UI that plan has changed (but don't recalculate everything)
            PlanChanged?.Invoke();
        }

        public List<string> GetMissingIngredients(int factoryId)
        {
            if (_plan == null)
            {
                return new List<string>();
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return new List<string>();
            }

            List<string> missingIngredients = new();
            
            // Collect all missing ingredients from component parts
            foreach (Item item in factory.ComponentParts)
            {
                if (item.HasMissingIngredients)
                {
                    missingIngredients.AddRange(item.MissingIngredients);
                }
            }

            // Remove duplicates and return
            return missingIngredients.Distinct().ToList();
        }

        public void AddMissingIngredientsForItem(int factoryId, Item componentItem)
        {
            if (_plan == null || _factoryCatalog == null || componentItem == null)
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            // Get missing ingredients for this specific component item
            if (!componentItem.HasMissingIngredients)
            {
                return;
            }

            // Add missing ingredients to factory using Calculator
            AddIngredientsToFactory(factory, componentItem.MissingIngredients);
            
            // Notify UI that plan has changed (but don't recalculate everything)
            PlanChanged?.Invoke();
        }

        private void AddIngredientsToFactory(Factory factory, IEnumerable<string> ingredientNames)
        {
            if (_factoryCatalog == null)
            {
                return;
            }

            // Calculate component parts for each missing ingredient using the Calculator
            Calculator calculator = new();
            foreach (string ingredientName in ingredientNames)
            {
                // Find the default recipe for this ingredient
                Recipe? recipe = FindRecipe(_factoryCatalog, ingredientName);
                if (recipe != null && recipe.Products != null && recipe.Products.Any())
                {
                    // Use the recipe's default production rate
                    double defaultQuantity = recipe.Products[0].perMin;
                    
                    // Calculate the production requirements for this ingredient
                    List<Item> calculatedItems = calculator.CalculateProduction(
                        _factoryCatalog, 
                        ingredientName, 
                        defaultQuantity, 
                        factory.ImportedParts);
                    
                    // Add the calculated items directly to ComponentParts
                    factory.ComponentParts.AddRange(calculatedItems);
                }
            }
        }

        private Recipe? FindRecipe(FactoryCatalog factoryCatalog, string partName)
        {
            foreach (Recipe recipe in factoryCatalog.Recipes)
            {
                if (recipe.Products != null)
                {
                    foreach (Product product in recipe.Products)
                    {
                        if (product.part == partName)
                        {
                            return recipe;
                        }
                    }
                }
            }
            return null;
        }

        public void NotifyPlanChanged()
        {
            PlanChanged?.Invoke();
        }
    }
}