
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
            // Create a map of what each factory produces (exports)
            Dictionary<int, Dictionary<string, double>> factoryProduction = new();

            foreach (Factory factory in Factories)
            {
                factoryProduction[factory.Id] = new();

                // Add target parts as potential exports
                if (factory.TargetParts != null)
                {
                    foreach (Item targetPart in factory.TargetParts)
                    {
                        factoryProduction[factory.Id][targetPart.Name] = targetPart.Quantity;
                    }
                }

                // Initialize surplus list if it doesn't exist
                if (factory.Surplus == null)
                {
                    factory.Surplus = new();
                }
            }

            // Check and balance imported parts
            foreach (Factory factory in Factories)
            {
                if (factory.ImportedParts == null)
                {
                    continue;
                }

                Dictionary<int, ImportedItem> updatedImports = new();

                foreach (KeyValuePair<int, ImportedItem> import in factory.ImportedParts.ToList())
                {
                    int sourceFactoryId = import.Key;
                    ImportedItem importedItem = import.Value;

                    // Find the source factory
                    Factory? sourceFactory = Factories.FirstOrDefault(f => f.Id == sourceFactoryId);

                    if (sourceFactory == null)
                    {
                        // Source factory not found - mark as unavailable
                        importedItem.Item.Quantity = 0;
                        AddPlanningNote(factory, $"Warning: Source factory '{sourceFactoryId}' not found for import '{importedItem.Item.Name}'");
                        continue;
                    }

                    // Check if source factory produces this item
                    if (factoryProduction.ContainsKey(sourceFactoryId) &&
                        factoryProduction[sourceFactoryId].ContainsKey(importedItem.Item.Name))
                    {
                        double availableQuantity = factoryProduction[sourceFactoryId][importedItem.Item.Name];
                        double requestedQuantity = importedItem.Item.Quantity;

                        if (availableQuantity >= requestedQuantity)
                        {
                            // Sufficient production available
                            factoryProduction[sourceFactoryId][importedItem.Item.Name] -= requestedQuantity;
                            updatedImports[sourceFactoryId] = importedItem;

                            // Mark as allocated in source factory
                            MarkAsExported(sourceFactory, importedItem.Item.Name, requestedQuantity, factory.Name);
                        }
                        else if (availableQuantity > 0)
                        {
                            // Partial allocation possible
                            importedItem.Item.Quantity = availableQuantity;
                            factoryProduction[sourceFactoryId][importedItem.Item.Name] = 0;
                            updatedImports[sourceFactoryId] = importedItem;

                            AddPlanningNote(factory, $"Warning: Only {availableQuantity:F2} of {requestedQuantity:F2} '{importedItem.Item.Name}' available from '{sourceFactoryId}'");
                            MarkAsExported(sourceFactory, importedItem.Item.Name, availableQuantity, factory.Name);
                        }
                        else
                        {
                            // No production available
                            importedItem.Item.Quantity = 0;
                            AddPlanningNote(factory, $"Error: No '{importedItem.Item.Name}' available from '{sourceFactoryId}' (already fully allocated)");
                        }
                    }
                    else
                    {
                        // Source factory doesn't produce this item
                        importedItem.Item.Quantity = 0;
                        AddPlanningNote(factory, $"Error: Factory '{sourceFactoryId}' does not produce '{importedItem.Item.Name}'");
                    }
                }

                // Update the factory's imported parts with validated quantities
                factory.ImportedParts = updatedImports;
            }

            // Mark remaining production as surplus
            foreach (Factory factory in Factories)
            {
                if (factoryProduction.ContainsKey(factory.Id))
                {
                    foreach (var remainingProduction in factoryProduction[factory.Id])
                    {
                        if (remainingProduction.Value > 0)
                        {
                            // Add to surplus
                            var surplusItem = new Item
                            {
                                Name = remainingProduction.Key,
                                Quantity = remainingProduction.Value
                            };

                            factory.Surplus.Add(surplusItem);
                        }
                    }
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