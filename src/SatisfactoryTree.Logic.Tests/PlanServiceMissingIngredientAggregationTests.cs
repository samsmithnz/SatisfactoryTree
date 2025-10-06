using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Web.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public class PlanServiceMissingIngredientAggregationTests
    {
        private FactoryCatalog? _factoryCatalog;
        private PlanService? _planService;

        [TestInitialize]
        public async Task Initialize()
        {
            _factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();
            if (_factoryCatalog == null)
            {
                Assert.Fail("Factory catalog failed to load");
            }
            _planService = new PlanService
            {
                FactoryCatalog = _factoryCatalog,
                Plan = new Plan()
            };
            _planService.AddFactory();
            Assert.IsNotNull(_planService.Plan);
            Assert.AreEqual(1, _planService.Plan.Factories.Count);
        }

        [TestMethod]
        public void AddAllMissingIngredients_SumsQuantitiesAcrossMultipleProducts()
        {
            if (_planService == null || _planService.Plan == null || _factoryCatalog == null)
            {
                Assert.Fail("Test not initialized properly");
            }
            Factory factory = _planService.Plan.Factories[0];

            // Export two products that both require IronIngot
            // 10 IronPlate -> needs 15 IronIngot (1.5 each)
            // 5 IronRod -> needs 5 IronIngot (1 each)
            // Total expected IronIngot = 20
            _planService.AddExportedPartToFactory(factory.Id, "IronPlate", 10);
            _planService.AddExportedPartToFactory(factory.Id, "IronRod", 5);

            // Sanity: two user-defined exports
            Assert.AreEqual(2, factory.ExportedParts.Count(e => factory.UserDefinedExports.Contains(e.Item.Name)));

            // Ensure missing ingredients include IronIngot before adding
            List<string> missingBefore = _planService.GetMissingIngredients(factory.Id);
            Assert.IsTrue(missingBefore.Contains("IronIngot"), "IronIngot should be missing before auto-add");

            _planService.AddAllMissingIngredients(factory.Id);

            // Find IronIngot export (auto-added)
            ExportedItem? ingotExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "IronIngot");
            Assert.IsNotNull(ingotExport, "IronIngot should have been added as an exported part");

            double expectedQuantity = 20.0; // 15 + 5
            Assert.AreEqual(expectedQuantity, ingotExport.Item.Quantity, 0.001, "IronIngot quantity should equal combined requirement of all products");

            // Re-run AddAllMissingIngredients to ensure it does NOT inflate beyond required total
            _planService.AddAllMissingIngredients(factory.Id);
            ExportedItem? ingotExportAfter = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == "IronIngot");
            Assert.IsNotNull(ingotExportAfter);
            Assert.AreEqual(expectedQuantity, ingotExportAfter.Item.Quantity, 0.001, "IronIngot quantity should remain at required total, not accumulate");
        }
    }
}
