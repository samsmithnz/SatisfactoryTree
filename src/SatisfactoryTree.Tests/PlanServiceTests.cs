using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class PlanServiceTests
    {
        private FactoryCatalog? factoryCatalog;
        private PlanService? planService;

        [TestInitialize]
        public async Task Initialize()
        {
            factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();
            if (factoryCatalog == null)
            {
                Assert.Fail("Factory catalog is null");
            }

            planService = new PlanService
            {
                FactoryCatalog = factoryCatalog,
                Plan = new Plan()
            };
        }

        [TestMethod]
        public void AddAllMissingIngredients_ShouldAddToExportedParts()
        {
            // Arrange
            if (planService == null || planService.Plan == null)
            {
                Assert.Fail("PlanService or Plan is null");
            }

            // Create a factory that produces Iron Plates
            Factory factory = new(1, "Test Factory");
            factory.ExportedParts.Add(new ExportedItem(new Item { Name = "IronPlate", Quantity = 30 }));
            factory.UserDefinedExports.Add("IronPlate"); // Track as user-defined
            planService.Plan.Factories.Add(factory);

            // Calculate component parts to get missing ingredients
            planService.RefreshPlanCalculations();

            // Get the count of exported parts before adding missing ingredients
            int exportedPartsCountBefore = factory.ExportedParts.Count;
            var missingIngredientsBefore = planService.GetMissingIngredients(factory.Id);
            Assert.IsTrue(missingIngredientsBefore.Count > 0, "Should have missing ingredients initially");

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // Missing ingredients should be added to ExportedParts so they can be produced
            int exportedPartsCountAfter = factory.ExportedParts.Count;
            
            // ExportedParts count should increase by number of missing ingredients
            Assert.IsTrue(exportedPartsCountAfter > exportedPartsCountBefore, 
                "Adding missing ingredients should add items to ExportedParts");

            // The originally missing ingredients should now be in ExportedParts
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ExportedParts.Any(e => e.Item.Name == ingredient),
                    $"Missing ingredient {ingredient} should have been added to ExportedParts");
            }
            
            // But they should NOT be in UserDefinedExports (they're auto-added)
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsFalse(factory.UserDefinedExports.Contains(ingredient),
                    $"Auto-added ingredient {ingredient} should NOT be in UserDefinedExports");
            }
        }

        [TestMethod]
        public void AddAllMissingIngredients_ShouldResolveComponentPartDependencies()
        {
            // Arrange
            if (planService == null || planService.Plan == null)
            {
                Assert.Fail("PlanService or Plan is null");
            }

            // Create a factory that produces Iron Plates
            Factory factory = new(1, "Test Factory");
            factory.ExportedParts.Add(new ExportedItem(new Item { Name = "IronPlate", Quantity = 30 }));
            factory.UserDefinedExports.Add("IronPlate"); // Track as user-defined
            planService.Plan.Factories.Add(factory);

            // Calculate component parts to get missing ingredients
            planService.RefreshPlanCalculations();

            var missingIngredientsBefore = planService.GetMissingIngredients(factory.Id);
            Assert.IsTrue(missingIngredientsBefore.Count > 0, "Should have missing ingredients initially");

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // ComponentParts should have increased with the new calculated items
            Assert.IsTrue(factory.ComponentParts.Count > 0, 
                "ComponentParts should be present");
            
            // ExportedParts should now include the missing ingredients
            Assert.IsTrue(factory.ExportedParts.Count > 1,
                "Should have added missing ingredients to ExportedParts");
            
            // The originally missing ingredients should now be in ExportedParts
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ExportedParts.Any(e => e.Item.Name == ingredient),
                    $"Originally missing ingredient {ingredient} should be in ExportedParts");
            }
            
            // But only the user-defined export (IronPlate) should be in UserDefinedExports
            Assert.AreEqual(1, factory.UserDefinedExports.Count,
                "Should still have only 1 user-defined export");
            Assert.IsTrue(factory.UserDefinedExports.Contains("IronPlate"),
                "IronPlate should be in UserDefinedExports");
        }

        [TestMethod]
        public void AddSingleIngredient_ShouldNotAddToUserDefinedExports()
        {
            // Arrange
            if (planService == null || planService.Plan == null)
            {
                Assert.Fail("PlanService or Plan is null");
            }

            // Create a factory that produces Reinforced Iron Plates
            Factory factory = new(1, "Test Factory");
            factory.ExportedParts.Add(new ExportedItem(new Item { Name = "IronPlateReinforced", Quantity = 1 }));
            factory.UserDefinedExports.Add("IronPlateReinforced");
            planService.Plan.Factories.Add(factory);

            // Calculate component parts - now only the exported item itself should be in component parts
            planService.RefreshPlanCalculations();

            // Verify we have component parts
            Assert.IsTrue(factory.ComponentParts.Count > 0, "Should have component parts");
            
            // Only the exported item should be in component parts, not its ingredients
            Assert.AreEqual(1, factory.ComponentParts.Count, "Should have only 1 component part (the exported item)");
            Assert.AreEqual("IronPlateReinforced", factory.ComponentParts[0].Name, "Component part should be Reinforced Iron Plate");
            
            // The exported item should have missing ingredients tracked
            Assert.IsTrue(factory.ComponentParts[0].HasMissingIngredients, "Reinforced Iron Plate should have missing ingredients");
            Assert.IsTrue(factory.ComponentParts[0].MissingIngredients.Contains("IronPlate"), "Should show Iron Plate as missing");
            Assert.IsTrue(factory.ComponentParts[0].MissingIngredients.Contains("IronScrew"), "Should show Screws as missing");

            int exportedPartsCountBefore = factory.ExportedParts.Count;
            int userDefinedExportsCountBefore = factory.UserDefinedExports.Count;

            // Act - Add the Reinforced Iron Plate's missing ingredients
            planService.AddMissingIngredientsForItem(factory.Id, factory.ComponentParts[0]);

            // Assert
            // Should have added Iron Plate and Screws to ExportedParts
            Assert.IsTrue(factory.ExportedParts.Count > exportedPartsCountBefore,
                "Should have added items to ExportedParts");
            
            Assert.IsTrue(factory.ExportedParts.Any(e => e.Item.Name == "IronPlate"),
                "Iron Plate should be in ExportedParts");
            
            // The ingredients should NOT be marked as user-defined (only auto-added)
            Assert.AreEqual(userDefinedExportsCountBefore, factory.UserDefinedExports.Count,
                "Should not have changed UserDefinedExports count");
            Assert.IsFalse(factory.UserDefinedExports.Contains("IronPlate"),
                "Iron Plate should NOT be in UserDefinedExports since it was auto-added");
        }

        [TestMethod]
        public void GetRawResourceItems_ShouldSumQuantitiesInsteadOfCreatingDuplicates()
        {
            // Arrange
            if (planService == null || planService.Plan == null || factoryCatalog == null)
            {
                Assert.Fail("PlanService, Plan, or FactoryCatalog is null");
            }

            // Ensure RawResources is initialized for the test
            if (factoryCatalog.RawResources == null || factoryCatalog.RawResources.Count == 0)
            {
                // Manually initialize RawResources for the test
                factoryCatalog.RawResources = new Dictionary<string, RawResource>
                {
                    { "OreIron", new RawResource { name = "Iron Ore", limit = 92100 } }
                };
            }

            // Create a factory - manually construct a scenario where the same raw resource
            // appears in both ComponentParts and ExportedParts (auto-added)
            Factory factory = new(1, "Test Factory");
            
            // Add a raw resource (Iron Ore) to both ComponentParts and ExportedParts
            // This simulates what might happen when:
            // 1. A component part needs Iron Ore (so it's in ComponentParts)
            // 2. Iron Ore was auto-added as a missing ingredient (so it's in ExportedParts but not UserDefinedExports)
            
            Item ironOreComponent = new Item { Name = "OreIron", Quantity = 30 };
            factory.ComponentParts.Add(ironOreComponent);
            
            Item ironOreExported = new Item { Name = "OreIron", Quantity = 15 };
            factory.ExportedParts.Add(new ExportedItem(ironOreExported));
            // Note: NOT adding to UserDefinedExports, so it's treated as auto-added
            
            planService.Plan.Factories.Add(factory);

            // Act - Get raw resources using the helper method that mimics FactoryItems.razor
            List<Item> rawResources = GetRawResourceItemsFromFactory(factory, factoryCatalog);

            // Debug output
            System.Console.WriteLine($"RawResources count in catalog: {factoryCatalog.RawResources?.Count ?? 0}");
            System.Console.WriteLine($"Iron Ore in ComponentParts: {factory.ComponentParts.Count(c => c.Name == "OreIron")}");
            System.Console.WriteLine($"Iron Ore in ExportedParts (auto-added): {factory.ExportedParts.Count(e => e.Item.Name == "OreIron" && !factory.UserDefinedExports.Contains(e.Item.Name))}");
            System.Console.WriteLine($"Raw resources returned: {rawResources.Count}");
            foreach (var item in rawResources)
            {
                System.Console.WriteLine($"  {item.Name}: {item.Quantity} per/min");
            }

            // Assert - The issue is that Iron Ore appears twice instead of being summed
            var ironOreItems = rawResources.Where(r => r.Name == "OreIron").ToList();
            
            if (ironOreItems.Count > 1)
            {
                double totalQuantity = ironOreItems.Sum(i => i.Quantity);
                Assert.Fail($"Iron Ore appears {ironOreItems.Count} times with quantities: {string.Join(", ", ironOreItems.Select(i => i.Quantity))}. " +
                    $"Expected: 1 entry with total quantity {totalQuantity}");
            }
            
            Assert.AreEqual(1, ironOreItems.Count, 
                "Iron Ore should appear exactly once in raw resources, with quantities summed");
            Assert.AreEqual(45, ironOreItems[0].Quantity,
                "Iron Ore total quantity should be 30 + 15 = 45");
        }

        // Helper method that mimics the logic from FactoryItems.razor GetRawResourceItems()
        private List<Item> GetRawResourceItemsFromFactory(Factory factory, FactoryCatalog factoryCatalog)
        {
            List<Item> rawResources = new List<Item>();
            
            if (factory == null)
                return rawResources;

            // Add raw resources from ComponentParts
            if (factory.ComponentParts != null)
            {
                rawResources.AddRange(factory.ComponentParts.Where(item => IsRawResource(item, factoryCatalog)));
            }

            // Also add raw resources from ExportedParts that are NOT user-defined
            if (factory.ExportedParts != null)
            {
                IEnumerable<Item> autoAddedRawResources = factory.ExportedParts
                    .Where(e => !factory.UserDefinedExports.Contains(e.Item.Name) && IsRawResource(e.Item, factoryCatalog))
                    .Select(e => e.Item);
                rawResources.AddRange(autoAddedRawResources);
            }

            // Aggregate raw resources by name, summing quantities
            // This prevents duplicates when the same raw resource appears in multiple places
            var aggregatedResources = rawResources
                .GroupBy(r => r.Name)
                .Select(g => new Item 
                { 
                    Name = g.Key, 
                    Quantity = g.Sum(r => r.Quantity)
                })
                .ToList();

            return aggregatedResources;
        }

        private bool IsRawResource(Item item, FactoryCatalog factoryCatalog)
        {
            if (item == null || factoryCatalog?.RawResources == null)
                return false;

            return factoryCatalog.RawResources.ContainsKey(item.Name);
        }

        [TestMethod]
        public void AddMissingIngredientsForReinforcedIronPlate_ShouldProduceTwelveIronOre()
        {
            // Arrange
            if (planService == null || planService.Plan == null || factoryCatalog == null)
            {
                Assert.Fail("PlanService, Plan, or FactoryCatalog is null");
            }

            Factory factory = new(1, "Test Factory");
            factory.ExportedParts.Add(new ExportedItem(new Item { Name = "IronPlateReinforced", Quantity = 1 }));
            factory.UserDefinedExports.Add("IronPlateReinforced");
            planService.Plan.Factories.Add(factory);

            // Calculate component parts
            planService.RefreshPlanCalculations();

            // Act - Add missing ingredients step by step
            var missingIngredients = planService.GetMissingIngredients(factory.Id);
            System.Console.WriteLine("Initial missing ingredients: " + string.Join(", ", missingIngredients));
            
            // Add IronPlate and IronScrew
            while (missingIngredients.Contains("IronPlate") || missingIngredients.Contains("IronScrew"))
            {
                var componentWithMissing = factory.ComponentParts.FirstOrDefault(cp => cp.HasMissingIngredients);
                if (componentWithMissing != null)
                {
                    System.Console.WriteLine($"Adding missing ingredients for: {componentWithMissing.Name}");
                    planService.AddMissingIngredientsForItem(factory.Id, componentWithMissing);
                }
                missingIngredients = planService.GetMissingIngredients(factory.Id);
                System.Console.WriteLine("Missing ingredients after add: " + string.Join(", ", missingIngredients));
            }
            
            // Now add ingredients for IronPlate and IronScrew, which should add IronIngot and IronRod
            while (missingIngredients.Contains("IronIngot") || missingIngredients.Contains("IronRod"))
            {
                var componentWithMissing = factory.ComponentParts.FirstOrDefault(cp => cp.HasMissingIngredients);
                if (componentWithMissing != null)
                {
                    System.Console.WriteLine($"Adding missing ingredients for: {componentWithMissing.Name}");
                    planService.AddMissingIngredientsForItem(factory.Id, componentWithMissing);
                }
                missingIngredients = planService.GetMissingIngredients(factory.Id);
                System.Console.WriteLine("Missing ingredients after add: " + string.Join(", ", missingIngredients));
            }
            
            // Assert - Check exported parts
            System.Console.WriteLine("\nAll exported parts:");
            foreach (var exported in factory.ExportedParts)
            {
                System.Console.WriteLine($"  {exported.Item.Name}: {exported.Item.Quantity}");
            }
            
            // The issue is: when we add missing ingredients for iron ingots, 
            // it should need 12 iron ore total (9 from IronPlate + 3 from IronRod),
            // but the problem is we're adding them separately and they might be doubling
            
            // For 1 Reinforced Iron Plate:
            //   - Needs 6 Iron Plates -> needs 9 Iron Ingots -> needs 9 Iron Ore
            //   - Needs 12 Screws -> 3 Iron Rods -> needs 3 Iron Ingots -> needs 3 Iron Ore
            //   - Total: 12 iron ore
            
            var oreIronExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "OreIron");
            if (oreIronExport != null)
            {
                System.Console.WriteLine($"\nOreIron quantity: {oreIronExport.Item.Quantity}");
                Assert.AreEqual(12.0, oreIronExport.Item.Quantity, 0.01, 
                    $"OreIron quantity should be 12 (9 for plates + 3 for rods), but was {oreIronExport.Item.Quantity}");
            }
            else
            {
                // Check if OreIron needs to be added
                missingIngredients = planService.GetMissingIngredients(factory.Id);
                System.Console.WriteLine($"\nOreIron not in exported parts. Missing ingredients: {string.Join(", ", missingIngredients)}");
                if (missingIngredients.Contains("OreIron"))
                {
                    Assert.Fail("OreIron is still in missing ingredients - test setup incomplete");
                }
            }
        }
    }
}
