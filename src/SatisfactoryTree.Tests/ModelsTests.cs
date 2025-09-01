using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Models;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ModelsTests
    {
        [TestMethod]
        public void Item_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int level = 1;
            string name = "Iron Ingot";
            string image = "IronIngot.png";
            ItemType itemType = ItemType.Production;
            ResearchType researchType = ResearchType.Tier2;

            // Act
            Item item = new(level, name, image, itemType, researchType);

            // Assert
            Assert.AreEqual(level, item.Level);
            Assert.AreEqual(name, item.Name);
            Assert.AreEqual(image, item.Image);
            Assert.AreEqual(itemType, item.ItemType);
            Assert.AreEqual(researchType, item.ResearchType);
            Assert.IsNotNull(item.Recipes);
            Assert.AreEqual(0, item.Recipes.Count);
        }

        [TestMethod]
        public void Item_Constructor_WithDefaultValues_UsesDefaults()
        {
            // Arrange
            int level = 0;
            string name = "Test Item";
            string image = "test.png";

            // Act
            Item item = new(level, name, image);

            // Assert
            Assert.AreEqual(ItemType.Production, item.ItemType);
            Assert.AreEqual(ResearchType.Tier1, item.ResearchType);
        }

        [TestMethod]
        public void Recipe_Constructor_WithValidInputsAndOutputs_SetsPropertiesCorrectly()
        {
            // Arrange
            Dictionary<string, decimal> inputs = new() { { "Iron Ore", 1 } };
            Dictionary<string, decimal> outputs = new() { { "Iron Ingot", 1 } };
            string building = "Smelter";
            bool primaryMethod = true;

            // Act
            Recipe recipe = new(inputs, outputs, building, primaryMethod);

            // Assert
            Assert.AreEqual("Iron Ingot", recipe.Name);
            Assert.AreEqual(inputs, recipe.Inputs);
            Assert.AreEqual(outputs, recipe.Outputs);
            Assert.AreEqual(building, recipe.Building);
            Assert.AreEqual(primaryMethod, recipe.PrimaryMethodOfManufacture);
        }

        [TestMethod]
        public void Recipe_Constructor_WithCustomName_UsesCustomName()
        {
            // Arrange
            Dictionary<string, decimal> inputs = new() { { "Iron Ore", 1 } };
            Dictionary<string, decimal> outputs = new() { { "Iron Ingot", 1 } };
            string building = "Smelter";
            string customName = "Custom Recipe";

            // Act
            Recipe recipe = new(inputs, outputs, building, true, customName);

            // Assert
            Assert.AreEqual(customName, recipe.Name);
        }

        [TestMethod]
        public void Recipe_Constructor_WithNullOutputs_CreatesEmptyOutputs()
        {
            // Arrange
            Dictionary<string, decimal> inputs = new() { { "Iron Ore", 1 } };
            Dictionary<string, decimal>? outputs = null;
            string building = "Smelter";

            // Act
            Recipe recipe = new(inputs, outputs, building);

            // Assert
            Assert.IsNotNull(recipe.Outputs);
            Assert.AreEqual(0, recipe.Outputs.Count);
            Assert.AreEqual("Unknown", recipe.Name);
        }

        [TestMethod]
        public void Building_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string name = "Constructor";
            string image = "constructor.png";
            Building.ManufactoringBuildingType buildingType = Building.ManufactoringBuildingType.Production;
            decimal powerConsumption = 4;
            decimal powerGeneration = 10;

            // Act
            Building building = new(name, image, buildingType, powerConsumption, powerGeneration);

            // Assert
            Assert.AreEqual(name, building.Name);
            Assert.AreEqual(image, building.Image);
            Assert.AreEqual(buildingType, building.BuildingType);
            Assert.AreEqual(powerConsumption, building.PowerConsumption);
            Assert.AreEqual(powerGeneration, building.PowerGeneration);
        }

        [TestMethod]
        public void Building_Constructor_WithDefaultPowerGeneration_SetsToZero()
        {
            // Arrange
            string name = "Constructor";
            string image = "constructor.png";
            Building.ManufactoringBuildingType buildingType = Building.ManufactoringBuildingType.Production;
            decimal powerConsumption = 4;

            // Act
            Building building = new(name, image, buildingType, powerConsumption);

            // Assert
            Assert.AreEqual(0, building.PowerGeneration);
        }

        [TestMethod]
        public void ItemType_Enum_HasExpectedValues()
        {
            // Assert
            Assert.AreEqual(0, (int)ItemType.All);
            Assert.AreEqual(1, (int)ItemType.Production);
            Assert.AreEqual(2, (int)ItemType.PowerGeneration);
        }

        [TestMethod]
        public void ResearchType_Enum_HasExpectedValues()
        {
            // Assert
            Assert.AreEqual(0, (int)ResearchType.Tier1);
            Assert.AreEqual(1, (int)ResearchType.Tier2);
            Assert.AreEqual(2, (int)ResearchType.Tier3);
            Assert.AreEqual(3, (int)ResearchType.Tier4);
            Assert.AreEqual(4, (int)ResearchType.Tier5);
            Assert.AreEqual(5, (int)ResearchType.Tier6);
            Assert.AreEqual(6, (int)ResearchType.Tier7);
            Assert.AreEqual(7, (int)ResearchType.Tier8);
        }

        [TestMethod]
        public void ManufactoringBuildingType_Enum_HasExpectedValues()
        {
            // Assert
            Assert.AreEqual(0, (int)Building.ManufactoringBuildingType.None);
            Assert.AreEqual(1, (int)Building.ManufactoringBuildingType.Extraction);
            Assert.AreEqual(2, (int)Building.ManufactoringBuildingType.Production);
            Assert.AreEqual(3, (int)Building.ManufactoringBuildingType.PowerGeneration);
            Assert.AreEqual(4, (int)Building.ManufactoringBuildingType.Special);
        }

        [TestMethod]
        public void ProductionItem_Constructor_WithItem_SetsPropertiesCorrectly()
        {
            // Arrange
            Item item = new(1, "Iron Ingot", "iron.png");
            decimal quantity = 30;

            // Act
            ProductionItem productionItem = new(item, quantity);

            // Assert
            Assert.AreEqual(item, productionItem.Item);
            Assert.AreEqual(quantity, productionItem.Quantity);
            Assert.IsNotNull(productionItem.Dependencies);
            Assert.AreEqual(0, productionItem.Dependencies.Count);
            Assert.AreEqual("Iron Ingot", productionItem.Name);
        }

        [TestMethod]
        public void ProductionItem_Constructor_WithItemName_SetsPropertiesCorrectly()
        {
            // Arrange
            string itemName = "Iron Plate";
            decimal quantity = 20;

            // Act
            ProductionItem productionItem = new(itemName, quantity);

            // Assert
            Assert.AreEqual(itemName, productionItem.ItemName);
            Assert.AreEqual(quantity, productionItem.Quantity);
        }

        [TestMethod]
        public void ProductionItem_Name_WithNullItem_ReturnsUnknown()
        {
            // Arrange
            ProductionItem productionItem = new((Item?)null, 10);

            // Act
            string name = productionItem.Name;

            // Assert
            Assert.AreEqual("Unknown", name);
        }

        [TestMethod]
        public void ProductionCalculation_Constructor_InitializesProperties()
        {
            // Act
            ProductionCalculation calculation = new();

            // Assert
            Assert.IsNotNull(calculation.ProductionItems);
            Assert.AreEqual(0, calculation.ProductionItems.Count);
            Assert.AreEqual(0, calculation.PowerConsumption);
        }

        [TestMethod]
        public void ProductionCalculation_PowerConsumption_RoundsToTwoDecimalPlaces()
        {
            // Arrange
            ProductionCalculation calculation = new();

            // Act
            calculation.PowerConsumption = 15.6789m;

            // Assert
            Assert.AreEqual(15.68m, calculation.PowerConsumption);
        }
    }
}