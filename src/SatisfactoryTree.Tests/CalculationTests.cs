using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class CalculationTests
    {
        private Console.OldModels.FinalData? finalData = null;

        [TestInitialize]
        public async Task Initialize()
        {
            //arrange
            Processor processor = new();
            processor.GetContentFiles();
            if (processor != null)
            {
                string inputFile = processor.InputFile;
                string outputFile = processor.OutputFile;

                //act
                finalData = await Processor.ProcessFileOldModel(inputFile, outputFile);

                //assert
                if (finalData == null)
                {
                    Assert.Fail("Final data is null");
                }
            }
        }

        [TestMethod]
        public void IronIngotsCalculationTest()
        {
            //Arrange
            string partName = "IronIngot";
            double quantity = 30;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("IronIngot", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("OreIron", results[1].Name);
            Assert.AreEqual(30, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
        }

        [TestMethod]
        public void IronPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlate";
            double quantity = 30;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("IronPlate", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("IronIngot", results[1].Name);
            Assert.AreEqual(45, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("OreIron", results[2].Name);
            Assert.AreEqual(45, results[2].Quantity);
            Assert.AreEqual(3, results[2].Counter);
        }

        [TestMethod]
        public void ReinforcedPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlateReinforced";
            double quantity = 1;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            Calculator calculator = new();
            List<Item>? results = calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("IronPlate", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("IronScrew", results[2].Name);
            Assert.AreEqual(12, results[2].Quantity);
            Assert.AreEqual(2, results[2].Counter);
            Assert.AreEqual("IronIngot", results[3].Name);
            Assert.AreEqual(12, results[3].Quantity);
            Assert.AreEqual(3, results[3].Counter);
            Assert.AreEqual("IronRod", results[4].Name);
            Assert.AreEqual(3, results[4].Quantity);
            Assert.AreEqual(3, results[4].Counter);
            Assert.AreEqual("OreIron", results[5].Name);
            Assert.AreEqual(12, results[5].Quantity);
            Assert.AreEqual(4, results[5].Counter);
        }
    }
}
