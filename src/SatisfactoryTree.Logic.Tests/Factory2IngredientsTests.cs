using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class Factory2IngredientsTests
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
        public void Factory2IronPlatesTest()
        {
            // Arrange
            Factory2 factory = new(1, "Iron Plates Factory", factoryCatalog);
            factory.AddIngredient("IronPlate",30);
            Calculator calculator = new();

            // Act
            factory = calculator.ValidateFactory(factory);

            // Assert
            Assert.AreEqual("IronPlate", factory.Ingredients[0].Name);
            Assert.AreEqual(30, factory.Ingredients[0].Quantity);
            Assert.AreEqual(true, factory.Ingredients[0].HasMissingIngredients);
        }



        //[TestMethod]
        //public async Task Factory2PlasticTest()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //}
    }
}
