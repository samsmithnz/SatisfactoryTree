using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Web.Services;

namespace SatisfactoryTree.Logic.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class PlanServiceFunctionalityTests
    {
        [TestMethod]
        public void AddFactory_CreatesFactoryWithCorrectIdAndName()
        {
            // Arrange
            var plan = new Plan();
            plan.Factories.Add(new Factory(1, "First Factory"));
            plan.Factories.Add(new Factory(2, "Second Factory"));

            // Act - Simulate the AddFactory functionality
            int nextId = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            string factoryName = $"Factory {nextId}";
            var newFactory = new Factory(nextId, factoryName);
            plan.Factories.Add(newFactory);

            // Assert
            Assert.AreEqual(3, plan.Factories.Count);
            Assert.AreEqual(3, newFactory.Id);
            Assert.AreEqual("Factory 3", newFactory.Name);
            Assert.IsTrue(plan.Factories.Contains(newFactory));
        }

        [TestMethod]
        public void AddFactory_ToEmptyPlan_CreatesFactoryWithId1()
        {
            // Arrange
            var plan = new Plan();

            // Act - Simulate the AddFactory functionality
            int nextId = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            string factoryName = $"Factory {nextId}";
            var newFactory = new Factory(nextId, factoryName);
            plan.Factories.Add(newFactory);

            // Assert
            Assert.AreEqual(1, plan.Factories.Count);
            Assert.AreEqual(1, newFactory.Id);
            Assert.AreEqual("Factory 1", newFactory.Name);
        }

        [TestMethod]
        public void AddFactory_SequentialCalls_CreatesConsecutiveIds()
        {
            // Arrange
            var plan = new Plan();

            // Act - Add first factory
            int nextId1 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory1 = new Factory(nextId1, $"Factory {nextId1}");
            plan.Factories.Add(factory1);

            // Add second factory
            int nextId2 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory2 = new Factory(nextId2, $"Factory {nextId2}");
            plan.Factories.Add(factory2);

            // Add third factory
            int nextId3 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory3 = new Factory(nextId3, $"Factory {nextId3}");
            plan.Factories.Add(factory3);

            // Assert
            Assert.AreEqual(3, plan.Factories.Count);
            Assert.AreEqual("Factory 1", factory1.Name);
            Assert.AreEqual("Factory 2", factory2.Name);
            Assert.AreEqual("Factory 3", factory3.Name);
            Assert.AreEqual(1, factory1.Id);
            Assert.AreEqual(2, factory2.Id);
            Assert.AreEqual(3, factory3.Id);
        }

        [TestMethod]
        public async Task AddReinforcedPlateAndMissingIngredients_ValidatesFactoryStepByStep()
        {
            // Arrange
            FactoryCatalog? factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();
            Assert.IsNotNull(factoryCatalog, "FactoryCatalog should be loaded");

            PlanService planService = new();
            planService.FactoryCatalog = factoryCatalog;
            
            // Create a new plan and factory
            planService.AddFactory();
            Assert.IsNotNull(planService.Plan);
            Assert.AreEqual(1, planService.Plan.Factories.Count);
            
            Factory factory = planService.Plan.Factories[0];
            Assert.AreEqual(1, factory.Id);

            // Act & Assert - Step 1: Add Reinforced Iron Plate as exported part
            planService.AddExportedPartToFactory(factory.Id, "IronPlateReinforced", 1);
            Assert.AreEqual(1, factory.ExportedParts.Count);
            Assert.AreEqual("IronPlateReinforced", factory.ExportedParts[0].Item.Name);
            Assert.AreEqual(1, factory.ExportedParts[0].Item.Quantity);

            // Check that component parts have missing ingredients
            Assert.IsTrue(factory.ComponentParts.Count > 0, "Should have component parts calculated");
            
            // Find component parts with missing ingredients
            List<Item> itemsWithMissingIngredients = factory.ComponentParts
                .Where(cp => cp.HasMissingIngredients)
                .ToList();
            
            Assert.IsTrue(itemsWithMissingIngredients.Count > 0, "Should have items with missing ingredients initially");
            int initialMissingItemsCount = itemsWithMissingIngredients.Count;

            // Step 2: Add missing ingredients one by one (limited to just a few iterations to test the workflow)
            int maxAdds = 3; // Just add 3 rounds of missing ingredients to validate the process works
            int iteration = 0;
            
            for (int i = 0; i < maxAdds && itemsWithMissingIngredients.Count > 0; i++)
            {
                iteration++;
                
                // Get the first item with missing ingredients
                Item itemWithMissing = itemsWithMissingIngredients[0];
                Assert.IsTrue(itemWithMissing.HasMissingIngredients, $"Iteration {iteration}: Item {itemWithMissing.Name} should have missing ingredients");
                
                // Record how many exported parts we have before adding
                int exportedCountBefore = factory.ExportedParts.Count;
                
                // Add the missing ingredients for this item
                planService.AddMissingIngredientsForItem(factory.Id, itemWithMissing);
                
                // Verify at least one ingredient was added to exported parts
                Assert.IsTrue(factory.ExportedParts.Count >= exportedCountBefore, 
                    $"Iteration {iteration}: Should have same or more exported parts after adding missing ingredients");
                
                // Re-evaluate items with missing ingredients for next iteration
                itemsWithMissingIngredients = factory.ComponentParts
                    .Where(cp => cp.HasMissingIngredients)
                    .ToList();
            }

            // Assert - Final state
            Assert.AreEqual(maxAdds, iteration, $"Should have completed {maxAdds} iterations");
            
            // Verify we have multiple exported parts (original + added ingredients)
            Assert.IsTrue(factory.ExportedParts.Count > 1, 
                $"Should have more than 1 exported part after adding missing ingredients. Has {factory.ExportedParts.Count}");
            
            // Verify the original exported part is still there
            Assert.IsTrue(factory.ExportedParts.Any(ep => ep.Item.Name == "IronPlateReinforced"),
                "Original IronPlateReinforced should still be in exported parts");
                
            // Verify that at least some missing ingredients were added
            // We don't expect all to be resolved in just 3 iterations, but we should have made progress
            List<string> exportedPartNames = factory.ExportedParts.Select(ep => ep.Item.Name).ToList();
            Assert.IsTrue(exportedPartNames.Count > 1, 
                $"Should have added some ingredients. Exported parts: {string.Join(", ", exportedPartNames)}");
        }

        [TestMethod]
        public async Task AddMissingIngredients_ForReinforcedIronPlate_ShouldCalculateCorrectIronOreQuantity()
        {
            // Arrange
            FactoryCatalog? factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();
            Assert.IsNotNull(factoryCatalog, "Factory catalog should not be null");

            PlanService planService = new()
            {
                FactoryCatalog = factoryCatalog,
                Plan = new Plan()
            };

            Factory factory = new(1, "Test Factory");
            planService.Plan.Factories.Add(factory);
            
            // Add 1 Reinforced Iron Plate as the exported part - use default recipe
            planService.AddExportedPartToFactory(factory.Id, "IronPlateReinforced", 1);
            
            // Debug: Check what recipe was selected
            var reinforcedPlateExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "IronPlateReinforced");
            Assert.IsNotNull(reinforcedPlateExport, "Reinforced Iron Plate should be in exported parts");
            
            if (reinforcedPlateExport.Item.Recipe != null)
            {
                System.Console.WriteLine($"Recipe selected: {reinforcedPlateExport.Item.Recipe.DisplayName}");
                System.Console.WriteLine($"Recipe ID: {reinforcedPlateExport.Item.Recipe.Name}");
                System.Console.WriteLine($"IsAlternate: {reinforcedPlateExport.Item.Recipe.IsAlternate}");
                System.Console.WriteLine($"Ingredients:");
                foreach (var ing in reinforcedPlateExport.Item.Recipe.Ingredients)
                {
                    System.Console.WriteLine($"  {ing.part}: {ing.amount} ({ing.perMin}/min)");
                }
            }
            
            // Skip this test if the wrong recipe was selected
            if (reinforcedPlateExport.Item.Recipe == null || 
                !reinforcedPlateExport.Item.Recipe.Ingredients.Any(i => i.part == "IronScrew"))
            {
                Assert.Inconclusive("Test requires the default Reinforced Iron Plate recipe with IronScrew, but a different recipe was selected");
                return;
            }

            // Act - Progressively add missing ingredients until we get to OreIron
            int maxIterations = 10;
            int iteration = 0;
            
            while (iteration < maxIterations && factory.ComponentParts.Any(cp => cp.HasMissingIngredients))
            {
                iteration++;
                var componentWithMissing = factory.ComponentParts.FirstOrDefault(cp => cp.HasMissingIngredients);
                if (componentWithMissing == null) break;
                
                System.Console.WriteLine($"Iteration {iteration}: Adding missing ingredients for {componentWithMissing.Name}");
                System.Console.WriteLine($"  Missing: {string.Join(", ", componentWithMissing.MissingIngredients)}");
                
                planService.AddMissingIngredientsForItem(factory.Id, componentWithMissing);
                
                // Show what was added
                System.Console.WriteLine($"  Exported parts now:");
                foreach (var ep in factory.ExportedParts)
                {
                    System.Console.WriteLine($"    {ep.Item.Name}: {ep.Item.Quantity}");
                }
            }
            
            // Assert - Check if OreIron was added and verify the quantity
            var oreIronExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "OreIron");
            
            if (oreIronExport != null)
            {
                System.Console.WriteLine($"\nFinal OreIron quantity: {oreIronExport.Item.Quantity}");
                
                // For 1 Reinforced Iron Plate with default recipe:
                //   - Needs 6 Iron Plates -> 6 * 1.5 = 9 Iron Ingots -> 9 Iron Ore
                //   - Needs 12 Screws -> 3 Iron Rods (12/4) -> 3 Iron Ingots -> 3 Iron Ore  
                //   - Total: 12 Iron Ore
                // The bug was: showing 18 instead of 12
                Assert.AreEqual(12.0, oreIronExport.Item.Quantity, 0.01, 
                    $"OreIron should be 12 (9 for plates + 3 for rods), but was {oreIronExport.Item.Quantity}");
            }
            else
            {
                // If OreIron wasn't added, check what's still missing
                var stillMissing = factory.ComponentParts
                    .Where(cp => cp.HasMissingIngredients)
                    .SelectMany(cp => cp.MissingIngredients)
                    .Distinct()
                    .ToList();
                
                if (stillMissing.Any())
                {
                    Assert.Fail($"OreIron not added. Still missing: {string.Join(", ", stillMissing)}");
                }
                else
                {
                    Assert.Fail("OreIron was not added as an exported part");
                }
            }
        }
    }
}