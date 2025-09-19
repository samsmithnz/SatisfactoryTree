using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System.Xml.Linq;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public sealed class FactoriesCalculationTest
    {
        private FactoryCatalog? factoryCatalog = null;

        [TestInitialize]
        public async Task Initialize()
        {
            //arrange

            //act
            factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();

            //assert
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }
        }

        [TestMethod]
        public void TwoFactoriesReinforcedPlatesWithFullScrewImportCalculationTest()
        {
            //Arrange
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }
            Factory screwsFactory = new("Screws factory");
            screwsFactory.TargetParts.Add(new() { Name = "IronScrew", Quantity = 12 });
            Factory reinforcedPlatesFactory = new("Reinforced Iron Plates factory");
            reinforcedPlatesFactory.TargetParts.Add(new() { Name = "IronPlateReinforced", Quantity = 1 });
            reinforcedPlatesFactory.ImportedParts.Add("Screws factory", new() { Name = "IronScrew", Quantity = 12 });
            //Plan plan = new();
            //plan.Factories.Add(screwsFactory);
            //plan.Factories.Add(reinforcedPlatesFactory);

            //Act
            Calculator calculator = new();
            screwsFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, screwsFactory);
            reinforcedPlatesFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, reinforcedPlatesFactory);

            //Assert
            Assert.IsNotNull(reinforcedPlatesFactory);
            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
            List<Item> results = reinforcedPlatesFactory.ComponentParts;
            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(4, results[0].Counter);
            Assert.AreEqual("IronPlate", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(3, results[1].Counter);
            Assert.AreEqual("IronIngot", results[2].Name);
            Assert.AreEqual(9, results[2].Quantity);
            Assert.AreEqual(2, results[2].Counter);
            Assert.AreEqual("OreIron", results[3].Name);
            Assert.AreEqual(9, results[3].Quantity);
            Assert.AreEqual(1, results[3].Counter);
            Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey("Screws factory"));
            Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts["Screws factory"].Name);
            Assert.AreEqual(12, reinforcedPlatesFactory.ImportedParts["Screws factory"].Quantity);

            Assert.IsNotNull(screwsFactory);
            Assert.IsNotNull(screwsFactory.ComponentParts);
            List<Item> results2 = screwsFactory.ComponentParts;
            Assert.AreEqual(4, results2.Count);
            Assert.AreEqual("IronScrew", results2[0].Name);
            Assert.AreEqual(12, results2[0].Quantity);
            Assert.AreEqual(4, results2[0].Counter);
            Assert.AreEqual("IronRod", results2[1].Name);
            Assert.AreEqual(3, results2[1].Quantity);
            Assert.AreEqual(3, results2[1].Counter);
            Assert.AreEqual("IronIngot", results2[2].Name);
            Assert.AreEqual(3, results2[2].Quantity);
            Assert.AreEqual(2, results2[2].Counter);
            Assert.AreEqual("OreIron", results2[3].Name);
            Assert.AreEqual(3, results2[3].Quantity);
            Assert.AreEqual(1, results2[3].Counter);
        }

        [TestMethod]
        public void TwoFactoriesReinforcedPlatesWithPartialScrewImportCalculationTest()
        {
            //Arrange
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }
            Factory screwsFactory = new("Screws factory");
            screwsFactory.TargetParts.Add(new() { Name = "IronScrew", Quantity = 6 });
            Factory reinforcedPlatesFactory = new("Reinforced Iron Plates factory");
            reinforcedPlatesFactory.TargetParts.Add(new() { Name = "IronPlateReinforced", Quantity = 1 });
            reinforcedPlatesFactory.ImportedParts.Add("Screws factory",new() { Name = "IronScrew", Quantity = 6 });
            //Plan plan = new();
            //plan.Factories.Add(screwsFactory);
            //plan.Factories.Add(reinforcedPlatesFactory);

            //Act
            Calculator calculator = new();
            screwsFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, screwsFactory);
            reinforcedPlatesFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, reinforcedPlatesFactory);

            //Assert
            Assert.IsNotNull(reinforcedPlatesFactory);
            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
            List<Item> results = reinforcedPlatesFactory.ComponentParts;
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(5, results[0].Counter);
            Assert.AreEqual("IronScrew", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(4, results[1].Counter);
            Assert.AreEqual("IronPlate", results[2].Name);
            Assert.AreEqual(6, results[2].Quantity);
            Assert.AreEqual(3, results[2].Counter);
            Assert.AreEqual("IronRod", results[3].Name);
            Assert.AreEqual(1.5, results[3].Quantity);
            Assert.AreEqual(3, results[3].Counter);
            Assert.AreEqual("IronIngot", results[4].Name);
            Assert.AreEqual(10.5, results[4].Quantity);
            Assert.AreEqual(2, results[4].Counter);
            Assert.AreEqual("OreIron", results[5].Name);
            Assert.AreEqual(10.5, results[5].Quantity);
            Assert.AreEqual(1, results[5].Counter);
            Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey("Screws factory"));
            Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts["Screws factory"].Name);
            Assert.AreEqual(6, reinforcedPlatesFactory.ImportedParts["Screws factory"].Quantity);

            Assert.IsNotNull(screwsFactory);
            Assert.IsNotNull(screwsFactory.ComponentParts);
            List<Item> results2 = screwsFactory.ComponentParts;
            Assert.AreEqual(4, results2.Count);
            Assert.AreEqual("IronScrew", results2[0].Name);
            Assert.AreEqual(6, results2[0].Quantity);
            Assert.AreEqual(4, results2[0].Counter);
            Assert.AreEqual("IronRod", results2[1].Name);
            Assert.AreEqual(1.5, results2[1].Quantity);
            Assert.AreEqual(3, results2[1].Counter);
            Assert.AreEqual("IronIngot", results2[2].Name);
            Assert.AreEqual(1.5, results2[2].Quantity);
            Assert.AreEqual(2, results2[2].Counter);
            Assert.AreEqual("OreIron", results2[3].Name);
            Assert.AreEqual(1.5, results2[3].Quantity);
            Assert.AreEqual(1, results2[3].Counter);
        }
    }
}
