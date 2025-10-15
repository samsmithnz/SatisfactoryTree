using SatisfactoryTree.Logic.Calculations;
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
            if (factoryCatalog != null)
            {
                // Arrange
                Factory2 factory = new(1, "Iron Plates Factory", factoryCatalog);
                Recipe? recipe = Lookups.GetRecipes(factoryCatalog, "IronPlate").Find(r => r.Name == "IronPlate");
                factory.AddIngredient("IronPlate", 30, recipe);
                Calculator calculator = new();

                // Act
                factory = calculator.ValidateFactory(factory);

                // Assert
                Assert.AreEqual("IronPlate", factory.Ingredients[0].Name);
                Assert.AreEqual(30, factory.Ingredients[0].Quantity);
                Assert.AreEqual(true, factory.Ingredients[0].HasMissingIngredients);
                Assert.AreEqual(1, factory.Ingredients[0].MissingIngredients.Count);
                Assert.AreEqual("IronIngot", factory.Ingredients[0].MissingIngredients.FirstOrDefault().Key);
                Assert.AreEqual(45, factory.Ingredients[0].MissingIngredients.FirstOrDefault().Value);
            }
        }



        [TestMethod]
        public void Factory2PlasticTest()
        {
            if (factoryCatalog != null)
            {
                // Arrange
                Factory2 factory = new(1, "Plastic Factory", factoryCatalog);
                Recipe? recipe = Lookups.GetRecipes(factoryCatalog, "Plastic").Find(r => r.Name == "Plastic");
                factory.AddIngredient("Plastic", 30, recipe);
                Calculator calculator = new();

                // Act
                factory = calculator.ValidateFactory(factory);

                // Assert
                Assert.AreEqual("Plastic", factory.Ingredients[0].Name);
                Assert.AreEqual(30, factory.Ingredients[0].Quantity);
                Assert.AreEqual(true, factory.Ingredients[0].HasMissingIngredients);
                Assert.AreEqual(1, factory.Ingredients[0].MissingIngredients.Count);
                Assert.AreEqual("LiquidOil", factory.Ingredients[0].MissingIngredients.FirstOrDefault().Key);
                Assert.AreEqual(45, factory.Ingredients[0].MissingIngredients.FirstOrDefault().Value);

            }
        }
    }
}
