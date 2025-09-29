namespace SatisfactoryTree.Logic.Models
{
    public class Plan
    {
        public List<Part> Parts { get; set; } = new();
        public List<Recipe> Recipes { get; set; } = new();
        public List<Building> Buildings { get; set; } = new();
        public List<Factory> Factories { get; set; } = new();
        public bool UseValidationMode { get; set; } = true; // New: Use validation instead of full calculation

        public void UpdatePlanCalculations(FactoryCatalog factoryCatalog)
        {
            Calculator calculator = new();

            // Complete initial calculations - use validation mode or full calculation based on flag
            foreach (Factory factory in Factories)
            {
                if (UseValidationMode)
                {
                    factory.ComponentParts = calculator.ValidateFactorySetup(factoryCatalog, factory);
                }
                else
                {
                    factory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, factory);
                }
            }

            // Balance imports and exports across factories
            BalanceImportsAndExports();
        }

        private void BalanceImportsAndExports()
        {
            // Initialize exported quantities based on each factory's target production
            foreach (Factory factory in Factories)
            {
                foreach (ExportedItem exportedItem in factory.ExportedParts)
                {
                    // Initialize the exported quantity to the target production amount
                    exportedItem.PartQuantityExported = exportedItem.Item.Quantity;
                }
            }

            // Process each factory's import requests
            foreach (Factory factory in Factories)
            {
                foreach (KeyValuePair<int, ImportedItem> import in factory.ImportedParts)
                {
                    int sourceFactoryId = import.Key;
                    ImportedItem importedItem = import.Value;
                    
                    // Find the source factory
                    Factory? sourceFactory = Factories.FirstOrDefault(f => f.Id == sourceFactoryId);
                    if (sourceFactory == null)
                    {
                        // Source factory not found
                        importedItem.PartQuantityImported = 0;
                        continue;
                    }

                    // Find the matching exported item
                    ExportedItem? exportedItem = sourceFactory.ExportedParts
                        .FirstOrDefault(e => e.Item.Name == importedItem.Item.Name);
                    
                    if (exportedItem == null)
                    {
                        // Source factory doesn't export this item
                        importedItem.PartQuantityImported = 0;
                        continue;
                    }

                    double requestedQuantity = importedItem.Item.Quantity;
                    double availableQuantity = exportedItem.PartQuantityExported;

                    if (availableQuantity >= requestedQuantity)
                    {
                        // Full allocation possible
                        exportedItem.PartQuantityExported -= requestedQuantity;
                        importedItem.PartQuantityImported = requestedQuantity;
                    }
                    else if (availableQuantity > 0)
                    {
                        // Partial allocation possible
                        importedItem.PartQuantityImported = availableQuantity;
                        exportedItem.PartQuantityExported = 0;
                    }
                    else
                    {
                        // No quantity available
                        importedItem.PartQuantityImported = 0;
                    }
                }
            }

            // Update the final PartQuantityExported to reflect what was actually used
            foreach (Factory factory in Factories)
            {
                foreach (ExportedItem exportedItem in factory.ExportedParts)
                {
                    // PartQuantityExported now represents what was actually exported (allocated)
                    double originalProduction = exportedItem.Item.Quantity;
                    double remainingQuantity = exportedItem.PartQuantityExported;
                    exportedItem.PartQuantityExported = originalProduction - remainingQuantity;
                }
            }
        }

    }
}