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

        [Obsolete("This method no longer adds ingredients to ExportedParts. Missing ingredients should be resolved by importing from other factories. This method is kept for API compatibility but does nothing.")]
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

            // Missing ingredients are already tracked in ComponentParts.
            // They should be resolved by either:
            // 1. Importing them from another factory
            // 2. Creating a separate factory to produce them
            // 
            // Adding them to ExportedParts would make them exports, which is incorrect.
            // ComponentParts are calculated from ExportedParts, so missing ingredients
            // remain as warnings in ComponentParts until properly resolved.
            //
            // This method now does nothing - the missing ingredients alert serves as
            // a reminder to the user to set up proper imports or production chains.

            // Recalculate the entire plan (in case anything changed)
            RefreshPlanCalculations();
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