using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System.Xml.Linq;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public sealed class FactoriesCalculationTest
    {
        private ExtractedData? finalData = null;

        [TestInitialize]
        public async Task Initialize()
        {
            //arrange

            //act
            finalData = await GameFileExtractor.LoadDataFromFile();

            //assert
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }
        }

        [TestMethod]
        public void TwoFactoriesReinforcedPlatesCalculationTest()
        {
            //Arrange
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }
            Factory screwsFactory = new();
            screwsFactory.ExportedParts.Add(new() { Name = "IronScrew", Quantity = 12 });
            Factory reinforcedPlatesFactory = new();
            reinforcedPlatesFactory.ExportedParts.Add(new() { Name = "IronPlateReinforced", Quantity = 1 });
            reinforcedPlatesFactory.ImportedParts.Add(new() { Name = "IronScrew", Quantity = 12 });
            Plan plan = new();
            plan.Factories.Add(screwsFactory);
            plan.Factories.Add(reinforcedPlatesFactory);

            //Act
            Calculator calculator = new();
            screwsFactory.ComponentParts = calculator.CalculateProduction(finalData, "IronScrew", 12);
            reinforcedPlatesFactory.ComponentParts = calculator.CalculateProduction(finalData, "IronPlateReinforced", 1);

            //Assert
            Assert.IsNotNull(reinforcedPlatesFactory);
            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
            List<Item> results = reinforcedPlatesFactory.ComponentParts;
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("IronPlate", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("IronIngot", results[2].Name);
            Assert.AreEqual(9, results[2].Quantity);
            Assert.AreEqual(4, results[2].Counter);
            Assert.AreEqual("OreIron", results[3].Name);
            Assert.AreEqual(9, results[3].Quantity);
            Assert.AreEqual(5, results[3].Counter);
            Assert.AreEqual("IronScrew", screwsFactory.ImportedParts[0].Name);
            Assert.AreEqual(12, screwsFactory.ImportedParts[0].Quantity);


            Assert.IsNotNull(screwsFactory);
            Assert.IsNotNull(screwsFactory.ComponentParts);
            List<Item> results2 = reinforcedPlatesFactory.ComponentParts;
            Assert.AreEqual(6, results2.Count);
            Assert.AreEqual("IronScrew", results[0].Name);
            Assert.AreEqual(12, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("IronRod", results[2].Name);
            Assert.AreEqual(3, results[2].Quantity);
            Assert.AreEqual(2, results[2].Counter);
            Assert.AreEqual("IronIngot", results[1].Name);
            Assert.AreEqual(3, results[1].Quantity);
            Assert.AreEqual(3, results[1].Counter);
            Assert.AreEqual("OreIron", results[3].Name);
            Assert.AreEqual(3, results[3].Quantity);
            Assert.AreEqual(4, results[3].Counter);
        }
    }
}
