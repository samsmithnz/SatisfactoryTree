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
                factory = calculator.ValidateFactoryIngredient(factory);

                // Assert
                Assert.IsTrue(factory.Ingredients != null); 
                Assert.IsTrue(factory.Ingredients.Count > 0);
                Item ingredient = factory.Ingredients[0];
                Assert.AreEqual("IronPlate", ingredient.Name);
                Assert.AreEqual(30, ingredient.Quantity);
                Assert.AreEqual("images/parts/IronPlate_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(1, ingredient.MissingIngredients.Count);
                Assert.AreEqual("IronIngot", ingredient.MissingIngredients.FirstOrDefault().Key);
                Assert.AreEqual(45, ingredient.MissingIngredients.FirstOrDefault().Value);
                Assert.AreEqual("constructormk1", ingredient.Building);
                Assert.AreEqual("Constructor", ingredient.BuildingDisplayName);
                Assert.AreEqual(1.5, ingredient.BuildingQuantity);
                Assert.AreEqual(5.6, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/ConstructorMk1_256.png", ingredient.BuildingImagePath);

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
                factory = calculator.ValidateFactoryIngredient(factory);

                // Assert
                Assert.IsTrue(factory.Ingredients != null);
                Assert.IsTrue(factory.Ingredients.Count > 0);
                Item ingredient = factory.Ingredients[0];
                Assert.AreEqual("Plastic", ingredient.Name);
                Assert.AreEqual(30, ingredient.Quantity);
                Assert.AreEqual("images/parts/Plastic_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(1, ingredient.MissingIngredients.Count);
                Assert.AreEqual("LiquidOil", ingredient.MissingIngredients.FirstOrDefault().Key);
                Assert.AreEqual(45, ingredient.MissingIngredients.FirstOrDefault().Value);
                Assert.AreEqual("oilrefinery", ingredient.Building);
                Assert.AreEqual("Refinery", ingredient.BuildingDisplayName);
                Assert.AreEqual(1.5, ingredient.BuildingQuantity);
                Assert.AreEqual(42, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/OilRefinery_256.png", ingredient.BuildingImagePath);

            }
        }

        [TestMethod]
        public void Factory2HeavyModularFrameTest()
        {
            if (factoryCatalog != null)
            {
                // Arrange
                Factory2 factory = new(1, "Heavy Modular Frame Factory", factoryCatalog);
                Recipe? recipe = Lookups.GetRecipes(factoryCatalog, "ModularFrameHeavy").Find(r => r.Name == "ModularFrameHeavy");
                factory.AddIngredient("ModularFrameHeavy", 1, recipe);
                Calculator calculator = new();

                // Act
                factory = calculator.ValidateFactoryIngredient(factory);

                // Assert
                Assert.IsTrue(factory.Ingredients != null);
                Assert.IsTrue(factory.Ingredients.Count > 0);
                Item ingredient = factory.Ingredients[0];
                Assert.AreEqual("ModularFrameHeavy", ingredient.Name);
                Assert.AreEqual(1, ingredient.Quantity);
                Assert.AreEqual("images/parts/ModularFrameHeavy_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(4, ingredient.MissingIngredients.Count);
                Assert.AreEqual(5, ingredient.MissingIngredients["ModularFrame"]);
                Assert.AreEqual(20, ingredient.MissingIngredients["SteelPipe"]);
                Assert.AreEqual(5, ingredient.MissingIngredients["SteelPlateReinforced"]);
                Assert.AreEqual(120, ingredient.MissingIngredients["IronScrew"]);
                Assert.AreEqual("manufacturermk1", ingredient.Building);
                Assert.AreEqual("Manufacturer", ingredient.BuildingDisplayName);
                Assert.AreEqual(0.5, ingredient.BuildingQuantity);
                Assert.AreEqual(22, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/Manufacturer_256.png", ingredient.BuildingImagePath);
            }
        }
    }
}
