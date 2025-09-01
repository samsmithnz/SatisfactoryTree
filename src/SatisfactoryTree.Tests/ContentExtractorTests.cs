using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.ContentExtractor;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ContentExtractorTests
    {
        [TestMethod]
        public void RawItem_ClassName_Get_Set()
        {
            // Arrange
            RawItem rawItem = new();
            string className = "Desc_IronOre_C";

            // Act
            rawItem.ClassName = className;

            // Assert
            Assert.AreEqual(className, rawItem.ClassName);
        }

        [TestMethod]
        public void RawItem_DisplayName_Get_Set()
        {
            // Arrange
            RawItem rawItem = new();
            string displayName = "Iron Ore";

            // Act
            rawItem.DisplayName = displayName;

            // Assert
            Assert.AreEqual(displayName, rawItem.DisplayName);
        }

        [TestMethod]
        public void RawItem_IsAlternateRecipe_WithAlternateClassName_ReturnsTrue()
        {
            // Arrange
            RawItem rawItem = new()
            {
                ClassName = "Recipe_Alternate_IronWire_C"
            };

            // Act
            bool isAlternate = rawItem.IsAlternateRecipe;

            // Assert
            Assert.IsTrue(isAlternate);
        }

        [TestMethod]
        public void RawItem_IsAlternateRecipe_WithNormalClassName_ReturnsFalse()
        {
            // Arrange
            RawItem rawItem = new()
            {
                ClassName = "Recipe_IronWire_C"
            };

            // Act
            bool isAlternate = rawItem.IsAlternateRecipe;

            // Assert
            Assert.IsFalse(isAlternate);
        }

        [TestMethod]
        public void RawItem_IsAlternateRecipe_WithNullClassName_ReturnsFalse()
        {
            // Arrange
            RawItem rawItem = new()
            {
                ClassName = null
            };

            // Act
            bool isAlternate = rawItem.IsAlternateRecipe;

            // Assert
            Assert.IsFalse(isAlternate);
        }

        [TestMethod]
        public void RawItem_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            RawItem rawItem = new();
            string description = "Test description";
            string stackSize = "SS_MEDIUM";
            string pingColor = "Red";
            string fluidColor = "Blue";
            string resourceSinkPoints = "100";
            string ingredients = "Iron Ore";
            string products = "Iron Ingot";
            string producedIn = "Smelter";
            string duration = "2.0";

            // Act
            rawItem.Description = description;
            rawItem.StackSize = stackSize;
            rawItem.PingColor = pingColor;
            rawItem.FluidColor = fluidColor;
            rawItem.ResourceSinkPoints = resourceSinkPoints;
            rawItem.Ingredients = ingredients;
            rawItem.Products = products;
            rawItem.ProducedIn = producedIn;
            rawItem.ManufactoringDuration = duration;

            // Assert
            Assert.AreEqual(description, rawItem.Description);
            Assert.AreEqual(stackSize, rawItem.StackSize);
            Assert.AreEqual(pingColor, rawItem.PingColor);
            Assert.AreEqual(fluidColor, rawItem.FluidColor);
            Assert.AreEqual(resourceSinkPoints, rawItem.ResourceSinkPoints);
            Assert.AreEqual(ingredients, rawItem.Ingredients);
            Assert.AreEqual(products, rawItem.Products);
            Assert.AreEqual(producedIn, rawItem.ProducedIn);
            Assert.AreEqual(duration, rawItem.ManufactoringDuration);
        }
    }
}