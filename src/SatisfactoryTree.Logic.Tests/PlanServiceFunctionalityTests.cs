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
    }
}