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
        public void AddAllMissingIngredients_ShouldAddToComponentParts()
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

            // Get the count of component parts before adding missing ingredients
            int componentPartsCountBefore = factory.ComponentParts.Count;
            var missingIngredientsBefore = planService.GetMissingIngredients(factory.Id);
            Assert.IsTrue(missingIngredientsBefore.Count > 0, "Should have missing ingredients initially");

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // Missing ingredients should be added to ComponentParts, not ExportedParts
            int componentPartsCountAfter = factory.ComponentParts.Count;
            int exportedPartsCount = factory.ExportedParts.Count;
            
            // ExportedParts should NOT increase - it should still only have Iron Plate
            Assert.AreEqual(1, exportedPartsCount, 
                "ExportedParts should not change - should still only contain the user-defined Iron Plate");
            
            // ComponentParts should increase with the calculated production steps
            Assert.IsTrue(componentPartsCountAfter > componentPartsCountBefore, 
                "Adding missing ingredients should add items to ComponentParts");

            // The originally missing ingredients should now be in ComponentParts
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ComponentParts.Any(c => c.Name == ingredient),
                    $"Missing ingredient {ingredient} should have been added to ComponentParts");
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
            // ComponentParts should have increased with the new calculated items
            Assert.IsTrue(factory.ComponentParts.Count > 0, 
                "ComponentParts should be present");
            
            // ExportedParts should NOT have changed - should still only have the user-defined Iron Plate
            Assert.AreEqual(1, factory.ExportedParts.Count,
                "ExportedParts should not change - should still only contain the user-defined Iron Plate");
            
            // The originally missing ingredients should now be in ComponentParts
            foreach (var ingredient in missingIngredientsBefore)
            {
                Assert.IsTrue(factory.ComponentParts.Any(c => c.Name == ingredient),
                    $"Originally missing ingredient {ingredient} should be in ComponentParts");
            }
        }
    }
}
