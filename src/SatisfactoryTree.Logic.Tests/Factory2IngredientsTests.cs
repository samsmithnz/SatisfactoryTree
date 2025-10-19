using SatisfactoryTree.Logic.Calculations;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                Factory2 ironPlatesFactory = new(1, "Iron Plates Factory", factoryCatalog);
                Recipe? ironPlatesRecipe = Lookups.GetRecipes(factoryCatalog, "IronPlate").Find(r => r.Name == "IronPlate");
                ironPlatesFactory.AddIngredient("IronPlate", 30, ironPlatesRecipe);
                Calculator calculator = new();

                // Act
                ironPlatesFactory = calculator.ValidateFactoryIngredients(ironPlatesFactory);

                // Assert
                Assert.IsTrue(ironPlatesFactory.Ingredients != null);
                Assert.IsTrue(ironPlatesFactory.Ingredients.Count > 0);
                Item ingredient = ironPlatesFactory.Ingredients[0];
                Assert.AreEqual("IronPlate", ingredient.Name);
                Assert.AreEqual("Iron Plate", ingredient.DisplayName);
                Assert.AreEqual(30, ingredient.Quantity);
                Assert.AreEqual("images/parts/IronPlate_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(1, ingredient.MissingIngredients.Count);
                Assert.AreEqual("IronIngot", ingredient.MissingIngredients[0].Name);
                Assert.AreEqual("Iron Ingot", ingredient.MissingIngredients[0].DisplayName); ;
                Assert.AreEqual(45, ingredient.MissingIngredients[0].Quantity);
                Assert.AreEqual("images/parts/IronIngot_256.png", ingredient.MissingIngredients[0].IngredientImagePart);
                Assert.AreEqual("constructormk1", ingredient.Building);
                Assert.AreEqual("Constructor", ingredient.BuildingDisplayName);
                Assert.AreEqual(1.5, ingredient.BuildingQuantity);
                Assert.AreEqual(5.6, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/ConstructorMk1_256.png", ingredient.BuildingImagePath);

            }
        }

        [TestMethod]
        public void Factory2IronPlatesAndIngotsTest()
        {
            if (factoryCatalog != null)
            {
                // Arrange
                Factory2 ironPlatesFactory = new(1, "Iron Plates Factory", factoryCatalog);
                Recipe? ironPlatesRecipe = Lookups.GetRecipes(factoryCatalog, "IronPlate").Find(r => r.Name == "IronPlate");
                ironPlatesFactory.AddIngredient("IronPlate", 30, ironPlatesRecipe);
                ironPlatesFactory.AddIngredient("IronIngot", 45, ironPlatesRecipe);
                Calculator calculator = new();

                // Act
                ironPlatesFactory = calculator.ValidateFactoryIngredients(ironPlatesFactory);

                // Assert
                Assert.IsTrue(ironPlatesFactory.Ingredients != null);
                Assert.IsTrue(ironPlatesFactory.Ingredients.Count > 0);
                Item ingredient = ironPlatesFactory.Ingredients[0];
                Assert.AreEqual("IronPlate", ingredient.Name);
                Assert.AreEqual("Iron Plate", ingredient.DisplayName);
                Assert.AreEqual(30, ingredient.Quantity);
                Assert.AreEqual("images/parts/IronPlate_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(false, ingredient.HasMissingIngredients);
                Assert.AreEqual(0, ingredient.MissingIngredients.Count);
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
                factory = calculator.ValidateFactoryIngredients(factory);

                // Assert
                Assert.IsTrue(factory.Ingredients != null);
                Assert.IsTrue(factory.Ingredients.Count > 0);
                Item ingredient = factory.Ingredients[0];
                Assert.AreEqual("Plastic", ingredient.Name);
                Assert.AreEqual("Plastic", ingredient.DisplayName);
                Assert.AreEqual(30, ingredient.Quantity);
                Assert.AreEqual("images/parts/Plastic_256.png", ingredient.ItemImagePath);
                Assert.AreEqual("HeavyOilResidue", ingredient.ByProductName);
                Assert.AreEqual("Heavy Oil Residue", ingredient.ByProductDisplayName);
                Assert.AreEqual(15, ingredient.ByProductQuantity);
                Assert.AreEqual("images/parts/HeavyOilResidue_256.png", ingredient.ByProductImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(1, ingredient.MissingIngredients.Count);
                Assert.AreEqual("LiquidOil", ingredient.MissingIngredients[0].Name);
                Assert.AreEqual("Crude Oil", ingredient.MissingIngredients[0].DisplayName); ;
                Assert.AreEqual(45, ingredient.MissingIngredients[0].Quantity);
                Assert.AreEqual("images/parts/CrudeOil_256.png", ingredient.MissingIngredients[0].IngredientImagePart);
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
                factory = calculator.ValidateFactoryIngredients(factory);

                // Assert
                Assert.IsTrue(factory.Ingredients != null);
                Assert.IsTrue(factory.Ingredients.Count > 0);
                Item ingredient = factory.Ingredients[0];
                Assert.AreEqual("ModularFrameHeavy", ingredient.Name);
                Assert.AreEqual("Heavy Modular Frame", ingredient.DisplayName);
                Assert.AreEqual(1, ingredient.Quantity);
                Assert.AreEqual("images/parts/HeavyModularFrame_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(4, ingredient.MissingIngredients.Count);
                Assert.AreEqual("ModularFrame", ingredient.MissingIngredients[0].Name);
                Assert.AreEqual("Modular Frame", ingredient.MissingIngredients[0].DisplayName); ;
                Assert.AreEqual(5, ingredient.MissingIngredients[0].Quantity);
                Assert.AreEqual("images/parts/ModularFrame_256.png", ingredient.MissingIngredients[0].IngredientImagePart);
                Assert.AreEqual("SteelPipe", ingredient.MissingIngredients[1].Name);
                Assert.AreEqual("Steel Pipe", ingredient.MissingIngredients[1].DisplayName); ;
                Assert.AreEqual(20, ingredient.MissingIngredients[1].Quantity);
                Assert.AreEqual("images/parts/SteelPipe_256.png", ingredient.MissingIngredients[1].IngredientImagePart);
                Assert.AreEqual("SteelPlateReinforced", ingredient.MissingIngredients[2].Name);
                Assert.AreEqual("Encased Industrial Beam", ingredient.MissingIngredients[2].DisplayName); ;
                Assert.AreEqual(5, ingredient.MissingIngredients[2].Quantity);
                Assert.AreEqual("images/parts/EncasedIndustrialBeam_256.png", ingredient.MissingIngredients[2].IngredientImagePart);
                Assert.AreEqual("IronScrew", ingredient.MissingIngredients[3].Name);
                Assert.AreEqual("Screws", ingredient.MissingIngredients[3].DisplayName); ;
                Assert.AreEqual(120, ingredient.MissingIngredients[3].Quantity);
                Assert.AreEqual("images/parts/Screws_256.png", ingredient.MissingIngredients[3].IngredientImagePart);
                Assert.AreEqual("manufacturermk1", ingredient.Building);
                Assert.AreEqual("Manufacturer", ingredient.BuildingDisplayName);
                Assert.AreEqual(0.5, ingredient.BuildingQuantity);
                Assert.AreEqual(22, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/Manufacturer_256.png", ingredient.BuildingImagePath);
            }
        }


        [TestMethod]
        public void Factory2ReinforcedIronPlatesTest()
        {
            if (factoryCatalog != null)
            {
                // Arrange
                Factory2? reinforcedPlatesFactory = new(1, "Reinforced Iron Plates factory", factoryCatalog);
                reinforcedPlatesFactory.AddIngredient("IronPlateReinforced", 5, Lookups.GetRecipes(factoryCatalog, "IronPlateReinforced").Find(r => r.Name == "IronPlateReinforced"));
                reinforcedPlatesFactory.AddIngredient("IronPlate", 30, Lookups.GetRecipes(factoryCatalog, "IronPlate").Find(r => r.Name == "IronPlate"));
                reinforcedPlatesFactory.AddIngredient("IronScrew", 30, Lookups.GetRecipes(factoryCatalog, "IronScrew").Find(r => r.Name == "Alternate_Screw"));
                Calculator calculator = new();

                // Act
                reinforcedPlatesFactory = calculator.ValidateFactoryIngredients(reinforcedPlatesFactory);

                // Assert
                Assert.IsTrue(reinforcedPlatesFactory.Ingredients != null);
                Assert.IsTrue(reinforcedPlatesFactory.Ingredients.Count > 0);
                Item ingredient = reinforcedPlatesFactory.Ingredients[0];
                Assert.AreEqual("IronPlateReinforced", ingredient.Name);
                Assert.AreEqual("Reinforced Iron Plate", ingredient.DisplayName);
                Assert.AreEqual(5, ingredient.Quantity);
                Assert.AreEqual("images/parts/ReinforcedIronPlate_256.png", ingredient.ItemImagePath);
                Assert.AreEqual(true, ingredient.HasMissingIngredients);
                Assert.AreEqual(1, ingredient.MissingIngredients.Count);
                Assert.AreEqual("IronScrew", ingredient.MissingIngredients[0].Name);
                Assert.AreEqual("Screws", ingredient.MissingIngredients[0].DisplayName); ;
                Assert.AreEqual(30, ingredient.MissingIngredients[0].Quantity);
                Assert.AreEqual("images/parts/Screws_256.png", ingredient.MissingIngredients[0].IngredientImagePart);
                Assert.AreEqual("assemblermk1", ingredient.Building);
                Assert.AreEqual("Assembler", ingredient.BuildingDisplayName);
                Assert.AreEqual(1.5, ingredient.BuildingQuantity);
                Assert.AreEqual(5.6, ingredient.BuildingPowerUsage);
                Assert.AreEqual("images/buildings/AssemblerMk1_256.png", ingredient.BuildingImagePath);

            }
        }
    }
}
