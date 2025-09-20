using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Web.Services;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class FactoryItemDisplayServiceTests
    {
        private FactoryItemDisplayService service;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new FactoryItemDisplayService();
        }

        [TestMethod]
        public void GetPartImagePath_WithReinforcedIronPlate_ReturnsCorrectMapping()
        {
            // Arrange
            string partName = "IronPlateReinforced";

            // Act
            string result = service.GetPartImagePath(partName);

            // Assert
            Assert.AreEqual("images/parts/ReinforcedIronPlate_256.png", result);
        }

        [TestMethod]
        public void GetPartImagePath_WithIronOre_ReturnsCorrectMapping()
        {
            // Arrange
            string partName = "OreIron";

            // Act
            string result = service.GetPartImagePath(partName);

            // Assert
            Assert.AreEqual("images/parts/IronOre_256.png", result);
        }

        [TestMethod]
        public void GetPartImagePath_WithIronScrews_ReturnsCorrectMapping()
        {
            // Arrange
            string partName = "IronScrew";

            // Act
            string result = service.GetPartImagePath(partName);

            // Assert
            Assert.AreEqual("images/parts/IronScrews_256.png", result);
        }

        [TestMethod]
        public void GetPartImagePath_WithDefaultPart_RemovesSpaces()
        {
            // Arrange
            string partName = "Iron Ingot";

            // Act
            string result = service.GetPartImagePath(partName);

            // Assert
            Assert.AreEqual("images/parts/IronIngot_256.png", result);
        }

        [TestMethod]
        public void GetBuildingImagePath_WithSmelter_ReturnsCorrectMapping()
        {
            // Arrange
            string buildingName = "smeltermk1";

            // Act
            string result = service.GetBuildingImagePath(buildingName);

            // Assert
            Assert.AreEqual("images/buildings/SmelterMk1_256.png", result);
        }

        [TestMethod]
        public void HasBuildingImage_WithKnownBuilding_ReturnsTrue()
        {
            // Arrange
            string buildingName = "smeltermk1";

            // Act
            bool result = service.HasBuildingImage(buildingName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasBuildingImage_WithUnknownBuilding_ReturnsFalse()
        {
            // Arrange
            string buildingName = "unknownbuilding";

            // Act
            bool result = service.HasBuildingImage(buildingName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetPartDisplayName_WithValidPart_ReturnsPartName()
        {
            // Arrange
            var part = new Part() { Name = "Iron Ingot" };

            // Act
            string result = service.GetPartDisplayName(part);

            // Assert
            Assert.AreEqual("Iron Ingot", result);
        }

        [TestMethod]
        public void GetPartDisplayName_WithNullName_ReturnsUnknown()
        {
            // Arrange
            var part = new Part() { Name = null };

            // Act
            string result = service.GetPartDisplayName(part);

            // Assert
            Assert.AreEqual("Unknown", result);
        }

        [TestMethod]
        public void GetPartIsFluid_WithFluidPart_ReturnsTrue()
        {
            // Arrange
            var part = new Part() { IsFluid = true };

            // Act
            bool result = service.GetPartIsFluid(part);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetPartIsFicsmas_WithFicsmaPart_ReturnsTrue()
        {
            // Arrange
            var part = new Part() { IsFicsmas = true };

            // Act
            bool result = service.GetPartIsFicsmas(part);

            // Assert
            Assert.IsTrue(result);
        }
    }
}