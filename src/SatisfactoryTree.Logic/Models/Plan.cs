using System.Security.Cryptography.X509Certificates;

namespace SatisfactoryTree.Logic.Models
{
    public class Plan
    {
        public List<Part> Parts { get; set; } = new();
        public List<Recipe> Recipes { get; set; } = new();
        public List<Building> Buildings { get; set; } = new();
        public List<Factory> Factories { get; set; } = new();

        public void UpdatePlanCalculations(FactoryCatalog factoryCatalog)
        {
            Calculator calculator = new();

            // Complete initial calculations
            foreach (Factory factory in Factories)
            {
                factory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, factory);
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

        private void MarkAsExported(Factory sourceFactory, string itemName, double quantity, string destinationFactory)
        {
            // Initialize exports list if it doesn't exist
            if (sourceFactory.Surplus == null)
            {
                sourceFactory.Surplus = new();
            }

            // Find existing export entry or create new one
            var exportItem = sourceFactory.Surplus.FirstOrDefault(s => s.Name == $"Export to {destinationFactory}: {itemName}");

            if (exportItem != null)
            {
                exportItem.Quantity += quantity;
            }
            else
            {
                sourceFactory.Surplus.Add(new Item
                {
                    Name = $"Export to {destinationFactory}: {itemName}",
                    Quantity = quantity
                });
            }
        }

        private void AddPlanningNote(Factory factory, string note)
        {
            // For now, we'll use the surplus list to store planning notes
            // In a more complete implementation, you might want a dedicated Notes property
            factory.Surplus ??= new();

            factory.Surplus.Add(new Item
            {
                Name = $"Planning Note",
                Quantity = 0,
                // You might want to add a Notes property to Item class for this
                Building = note
            });
        }

        public Dictionary<string, List<string>> GetPlanValidationReport()
        {
            Dictionary<string, List<string>> report = new();

            foreach (Factory factory in Factories)
            {
                List<string> factoryIssues = new();

                if (factory.Surplus != null)
                {
                    foreach (var surplus in factory.Surplus)
                    {
                        if (surplus.Name == "Planning Note")
                        {
                            factoryIssues.Add(surplus.Building); // Using Building field to store the note text
                        }
                        else if (surplus.Name.StartsWith("Export to"))
                        {
                            factoryIssues.Add($"Exporting: {surplus.Name} - {surplus.Quantity:F2} per/min");
                        }
                        else if (surplus.Quantity > 0)
                        {
                            factoryIssues.Add($"Surplus: {surplus.Name} - {surplus.Quantity:F2} per/min");
                        }
                    }
                }

                report[factory.Name] = factoryIssues;
            }

            return report;
        }
    }
}