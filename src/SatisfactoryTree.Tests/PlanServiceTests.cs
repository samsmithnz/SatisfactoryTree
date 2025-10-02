using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Web.Services;
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
            planService.Plan.Factories.Add(factory);

            // Calculate component parts to get missing ingredients
            planService.RefreshPlanCalculations();

            // Get the count of exported parts before adding missing ingredients
            int exportedPartsCountBefore = factory.ExportedParts.Count;
            var missingIngredientsBefore = planService.GetMissingIngredients(factory.Id);

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // Missing ingredients should be added to ExportedParts so they can be produced
            int exportedPartsCountAfter = factory.ExportedParts.Count;
            
            // The key assertion: ExportedParts count should increase by number of missing ingredients
            Assert.IsTrue(exportedPartsCountAfter > exportedPartsCountBefore, 
                "Adding missing ingredients should add items to ExportedParts");
            Assert.AreEqual(missingIngredientsBefore.Count, exportedPartsCountAfter - exportedPartsCountBefore,
                "Should add exactly the number of missing ingredients that were present");

            // After adding missing ingredients, there might be new missing ingredients at lower levels
            // (e.g., adding IronIngot makes IronOre missing), which is expected behavior
            var missingIngredientsAfter = planService.GetMissingIngredients(factory.Id);
            // We just verify that the originally missing ingredients were added
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ExportedParts.Any(e => e.Item.Name == ingredient),
                    $"Missing ingredient {ingredient} should have been added to ExportedParts");
                
                // Verify that auto-added ingredients are marked with IsAutoAdded flag
                var addedItem = factory.ExportedParts.First(e => e.Item.Name == ingredient);
                Assert.IsTrue(addedItem.IsAutoAdded,
                    $"Auto-added ingredient {ingredient} should have IsAutoAdded flag set to true");
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
            planService.Plan.Factories.Add(factory);

            // Calculate component parts to get missing ingredients
            planService.RefreshPlanCalculations();

            var missingIngredientsBefore = planService.GetMissingIngredients(factory.Id);
            Assert.IsTrue(missingIngredientsBefore.Count > 0, "Should have missing ingredients initially");

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // ComponentParts should still be present and calculated
            Assert.IsTrue(factory.ComponentParts.Count > 0, 
                "ComponentParts should still be calculated");
            
            // ExportedParts should now include the missing ingredients
            Assert.IsTrue(factory.ExportedParts.Count > 1,
                "Should have added missing ingredients to ExportedParts");
            
            // The originally missing ingredients should now be in ExportedParts with IsAutoAdded flag
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ExportedParts.Any(e => e.Item.Name == ingredient),
                    $"Originally missing ingredient {ingredient} should be in ExportedParts");
                
                // Verify that auto-added ingredients are marked with IsAutoAdded flag
                var addedItem = factory.ExportedParts.First(e => e.Item.Name == ingredient);
                Assert.IsTrue(addedItem.IsAutoAdded,
                    $"Auto-added ingredient {ingredient} should have IsAutoAdded flag set to true");
            }
        }
    }
}
