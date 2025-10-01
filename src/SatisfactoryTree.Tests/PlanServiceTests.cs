using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Web.Services;
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
        public void AddAllMissingIngredients_ShouldNotAddToExportedParts()
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
            var missingIngredients = planService.GetMissingIngredients(factory.Id);

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // The missing ingredients should NOT be added to ExportedParts
            // They should remain in ComponentParts only
            int exportedPartsCountAfter = factory.ExportedParts.Count;
            
            // The key assertion: ExportedParts count should not increase
            Assert.AreEqual(exportedPartsCountBefore, exportedPartsCountAfter, 
                "Adding missing ingredients should not add items to ExportedParts");

            // Missing ingredients should still be tracked in ComponentParts
            Assert.IsTrue(factory.ComponentParts.Count > 0, 
                "ComponentParts should still contain items");
        }

        [TestMethod]
        public void AddAllMissingIngredients_ComponentPartsShouldRemainCalculated()
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
            var componentPartsCountBefore = factory.ComponentParts.Count;

            // Act
            planService.AddAllMissingIngredients(factory.Id);

            // Assert
            // ComponentParts should still be present and calculated
            Assert.IsTrue(factory.ComponentParts.Count > 0, 
                "ComponentParts should still be calculated");
            
            // The structure of ComponentParts should be based on ExportedParts
            // which should not have changed
            Assert.AreEqual(1, factory.ExportedParts.Count,
                "Should still have only the original exported part");
            Assert.AreEqual("IronPlate", factory.ExportedParts[0].Item.Name,
                "Original exported part should remain unchanged");
        }
    }
}
