using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public class PartCalculationTests
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
        public void IronIngotsCalculationTest()
        {
            //Arrange
            string partName = "IronIngot";
            double quantity = 30;
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(factoryCatalog, partName, quantity, new());

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual("IronIngot", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("smeltermk1", results[0].Building);
            Assert.AreEqual(1, results[0].BuildingQuantity);

            //Assert.AreEqual("OreIron", results[1].Name);
            //Assert.AreEqual(30, results[1].Quantity);
            //Assert.AreEqual(1, results[1].Counter);
            //Assert.AreEqual("", results[1].Building);
            //Assert.AreEqual(0, results[1].BuildingQuantity);
        }

        [TestMethod]
        public void IronPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlate";
            double quantity = 30;
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(factoryCatalog, partName, quantity, new());

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("IronPlate", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(2, results[0].Counter);
            Assert.AreEqual("constructormk1", results[0].Building);
            Assert.AreEqual(1.5, results[0].BuildingQuantity);
            Assert.AreEqual(1, results[0].Ingredients.Count);
            Assert.AreEqual("IronIngot", results[0].Ingredients[0].Name);
            Assert.AreEqual(45, results[0].Ingredients[0].Quantity);

            Assert.AreEqual("IronIngot", results[1].Name);
            Assert.AreEqual(45, results[1].Quantity);
            Assert.AreEqual(1, results[1].Counter);
            Assert.AreEqual("smeltermk1", results[1].Building);
            Assert.AreEqual(1.5, results[1].BuildingQuantity);

            //Assert.AreEqual("OreIron", results[2].Name);
            //Assert.AreEqual(45, results[2].Quantity);
            //Assert.AreEqual(1, results[2].Counter);
            //Assert.AreEqual("", results[2].Building);
            //Assert.AreEqual(0, results[2].BuildingQuantity);
        }

        [TestMethod]
        public void ReinforcedPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlateReinforced";
            double quantity = 1;
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(factoryCatalog, partName, quantity, new());

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(5, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(4, results[0].Counter);
            Assert.AreEqual(2, results[0].Ingredients.Count);

            Assert.AreEqual("IronScrew", results[1].Name);
            Assert.AreEqual(12, results[1].Quantity);
            Assert.AreEqual(3, results[1].Counter);
            Assert.AreEqual(1, results[1].Ingredients.Count);

            Assert.AreEqual("IronPlate", results[2].Name);
            Assert.AreEqual(6, results[2].Quantity);
            Assert.AreEqual(3, results[2].Counter);
            Assert.AreEqual(1, results[2].Ingredients.Count);

            Assert.AreEqual("IronRod", results[3].Name);
            Assert.AreEqual(3, results[3].Quantity);
            Assert.AreEqual(2, results[3].Counter);
            Assert.AreEqual(1, results[3].Ingredients.Count);

            Assert.AreEqual("IronIngot", results[4].Name);
            Assert.AreEqual(12, results[4].Quantity);
            Assert.AreEqual(1, results[4].Counter);
            Assert.AreEqual(1, results[4].Ingredients.Count);

            //Assert.AreEqual("OreIron", results[5].Name);
            //Assert.AreEqual(12, results[5].Quantity);
            //Assert.AreEqual(1, results[5].Counter);
            //Assert.AreEqual(0, results[5].Ingredients.Count);
        }

        [TestMethod]
        public void HeavyModularFrameCalculationTest()
        {
            //Arrange
            string partName = "ModularFrameHeavy";
            double quantity = 1;
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(factoryCatalog, partName, quantity, new());

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(12, results.Count);

        }
    }
}
