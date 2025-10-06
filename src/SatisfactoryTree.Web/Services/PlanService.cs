using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Logic; // For Calculator

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
            get { return _plan; }
            set
            {
                _plan = value;
                PlanChanged?.Invoke();
            }
        }

        public FactoryCatalog? FactoryCatalog
        {
            get { return _factoryCatalog; }
            set { _factoryCatalog = value; }
        }

        public bool HasPlan
        {
            get { return _plan != null && _plan.Factories.Any(); }
        }

        public int? LastAddedFactoryId
        {
            get { return _lastAddedFactoryId; }
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

            int nextId;
            if (_plan.Factories.Any())
            {
                nextId = _plan.Factories.Max(f => f.Id) + 1;
            }
            else
            {
                nextId = 1;
            }

            string factoryName = $"Factory {nextId}";
            Factory newFactory = new Factory(nextId, factoryName);

            _plan.Factories.Add(newFactory);
            _lastAddedFactoryId = newFactory.Id;

            PlanChanged?.Invoke();
        }

        public void AddExportedPartToFactory(int factoryId, string itemName, double quantity, string? recipeName = null)
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

            Recipe? recipe = null;
            if (!string.IsNullOrEmpty(recipeName))
            {
                recipe = _factoryCatalog.Recipes.FirstOrDefault(r => r.Name == recipeName);
            }
            if (recipe == null)
            {
                recipe = FindRecipe(_factoryCatalog, itemName);
            }

            ExportedItem? existingExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == itemName);
            if (existingExport != null)
            {
                // User action: accumulate additional quantity requested
                existingExport.Item.Quantity += quantity;
                if (recipe != null)
                {
                    existingExport.Item.Recipe = recipe;
                }
            }
            else
            {
                factory.ExportedParts.Add(new ExportedItem(new Item { Name = itemName, Quantity = quantity, Recipe = recipe }));
            }
            
            factory.UserDefinedExports.Add(itemName);

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
                factory.UserDefinedExports.Remove(itemName);
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

            ImportedItem importedItem = new ImportedItem(sourceFactoryId, sourceFactoryName, new Item { Name = itemName, Quantity = quantity });
            factory.ImportedParts[sourceFactoryId] = importedItem;

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
                _plan.UpdatePlanCalculations(_factoryCatalog);
                PlanChanged?.Invoke();
            }
            catch (Exception ex)
            {
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

            List<string> missingIngredients = GetMissingIngredients(factoryId);
            AddIngredientsToFactory(factory, missingIngredients);
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

            List<string> missingIngredients = new List<string>();
            foreach (Item item in factory.ComponentParts)
            {
                if (item.HasMissingIngredients)
                {
                    missingIngredients.AddRange(item.MissingIngredients);
                }
            }
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

            if (!componentItem.HasMissingIngredients)
            {
                return;
            }

            AddIngredientsToFactory(factory, componentItem.MissingIngredients);
            RefreshPlanCalculations();
        }

        public void AddSingleMissingIngredientToFactory(int factoryId, string ingredientName)
        {
            if (_plan == null || _factoryCatalog == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ingredientName))
            {
                return;
            }

            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return;
            }

            // Compute total needed across all component parts (immediate ingredients lists)
            double totalNeeded = 0;
            foreach (Item componentPart in factory.ComponentParts)
            {
                if (componentPart.Ingredients != null)
                {
                    foreach (Item ingredient in componentPart.Ingredients)
                    {
                        if (ingredient.Name == ingredientName)
                        {
                            totalNeeded += ingredient.Quantity;
                        }
                    }
                }
            }

            // Fallback: if no ingredients list present, attempt default recipe rate
            if (Math.Abs(totalNeeded) < 0.0001)
            {
                Recipe? recipeFallback = FindRecipe(_factoryCatalog, ingredientName);
                if (recipeFallback != null && recipeFallback.Products != null && recipeFallback.Products.Any())
                {
                    totalNeeded = recipeFallback.Products[0].perMin;
                }
            }

            if (totalNeeded <= 0)
            {
                return;
            }

            // Add or update exported part (auto-added — NOT user-defined) ensuring quantity is sufficient for all usages
            ExportedItem? existingExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == ingredientName);
            if (existingExport == null)
            {
                Recipe? ingredientRecipe = FindRecipe(_factoryCatalog, ingredientName);
                factory.ExportedParts.Add(new ExportedItem(new Item { Name = ingredientName, Quantity = totalNeeded, Recipe = ingredientRecipe }));
            }
            else
            {
                // Ensure quantity covers total need (do not accumulate beyond requirement)
                if (existingExport.Item.Quantity < totalNeeded)
                {
                    existingExport.Item.Quantity = totalNeeded;
                }
            }

            // Refresh to update UI / dependent calculations
            RefreshPlanCalculations();
        }

        public List<Item> GetRawResourceTotals(int factoryId)
        {
            List<Item> rawResources = new List<Item>();
            if (_plan == null || _factoryCatalog == null)
            {
                return rawResources;
            }
            Factory? factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            {
                return rawResources;
            }

            // Use full production calculation to get entire dependency graph
            Calculator calculator = new Calculator();
            List<Item> fullBreakdown = calculator.CalculateFactoryProduction(_factoryCatalog, factory);

            if (_factoryCatalog.RawResources == null || _factoryCatalog.RawResources.Count == 0)
            {
                return rawResources;
            }

            Dictionary<string, Item> aggregate = new Dictionary<string, Item>();
            foreach (Item item in fullBreakdown)
            {
                if (_factoryCatalog.RawResources.ContainsKey(item.Name))
                {
                    if (aggregate.ContainsKey(item.Name))
                    {
                        aggregate[item.Name].Quantity += item.Quantity;
                    }
                    else
                    {
                        aggregate[item.Name] = new Item
                        {
                            Name = item.Name,
                            Quantity = item.Quantity
                        };
                    }
                }
            }

            rawResources = aggregate.Values.OrderBy(r => r.Name).ToList();
            return rawResources;
        }

        private void AddIngredientsToFactory(Factory factory, IEnumerable<string> ingredientNames)
        {
            if (_factoryCatalog == null)
            {
                return;
            }

            Dictionary<string, double> calculatedIngredientQuantities = new Dictionary<string, double>();
            foreach (Item componentPart in factory.ComponentParts)
            {
                if (componentPart.Ingredients != null)
                {
                    foreach (Item ingredient in componentPart.Ingredients)
                    {
                        if (ingredientNames.Contains(ingredient.Name))
                        {
                            if (calculatedIngredientQuantities.TryGetValue(ingredient.Name, out double existingQuantity))
                            {
                                calculatedIngredientQuantities[ingredient.Name] = existingQuantity + ingredient.Quantity;
                            }
                            else
                            {
                                calculatedIngredientQuantities[ingredient.Name] = ingredient.Quantity;
                            }
                        }
                    }
                }
            }

            IEnumerable<string> distinctIngredientNames = ingredientNames.Distinct();
            foreach (string ingredientName in distinctIngredientNames)
            {
                Recipe? recipe = FindRecipe(_factoryCatalog, ingredientName);
                if (recipe != null && recipe.Products != null && recipe.Products.Any())
                {
                    double quantity = calculatedIngredientQuantities.ContainsKey(ingredientName) ? calculatedIngredientQuantities[ingredientName] : recipe.Products[0].perMin;

                    ExportedItem? existingExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == ingredientName);
                    if (existingExport == null)
                    {
                        factory.ExportedParts.Add(new ExportedItem(new Item { Name = ingredientName, Quantity = quantity, Recipe = recipe }));
                    }
                    else
                    {
                        // Set to required quantity (do not stack duplicates)
                        if (existingExport.Item.Quantity < quantity)
                        {
                            existingExport.Item.Quantity = quantity;
                        }
                    }
                }
            }
        }

        private Recipe? FindRecipe(FactoryCatalog factoryCatalog, string partName)
        {
            // Collect all matching recipes first
            List<Recipe> candidates = factoryCatalog.Recipes
                .Where(r => r.Products != null && r.Products.Any(p => p.part == partName))
                .ToList();
            if (candidates.Count == 0)
            {
                return null;
            }
            // Prefer non-alternate, non-converter recipes
            Recipe? preferred = candidates
                .FirstOrDefault(r => r.IsAlternate == false && r.Building.Name != "converter");
            if (preferred != null)
            {
                return preferred;
            }
            // Otherwise fall back to first candidate (data order)
            return candidates.First();
        }

        public void NotifyPlanChanged()
        {
            PlanChanged?.Invoke();
        }
    }
}