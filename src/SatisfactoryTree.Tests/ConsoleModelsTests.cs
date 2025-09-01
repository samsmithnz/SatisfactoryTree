using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;
using SatisfactoryTree.Console.OldModels;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ConsoleModelsTests
    {
        [TestMethod]
        public void Item_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            Item item = new();
            string name = "Iron Ingot";
            double quantity = 30;
            string building = "Smelter";
            double buildingQuantity = 1.5;
            int counter = 1;

            // Act
            item.Name = name;
            item.Quantity = quantity;
            item.Building = building;
            item.BuildingQuantity = buildingQuantity;
            item.Counter = counter;
            item.Ingredients = new List<Item>();

            // Assert
            Assert.AreEqual(name, item.Name);
            Assert.AreEqual(quantity, item.Quantity);
            Assert.AreEqual(building, item.Building);
            Assert.AreEqual(buildingQuantity, item.BuildingQuantity);
            Assert.AreEqual(counter, item.Counter);
            Assert.IsNotNull(item.Ingredients);
            Assert.AreEqual(0, item.Ingredients.Count);
        }

        [TestMethod]
        public void Ingredient_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            Ingredient ingredient = new();
            string part = "Iron Ore";
            double amount = 1;
            double perMin = 30;
            double mwPerItem = 0.5;

            // Act
            ingredient.part = part;
            ingredient.amount = amount;
            ingredient.perMin = perMin;
            ingredient.mwPerItem = mwPerItem;

            // Assert
            Assert.AreEqual(part, ingredient.part);
            Assert.AreEqual(amount, ingredient.amount);
            Assert.AreEqual(perMin, ingredient.perMin);
            Assert.AreEqual(mwPerItem, ingredient.mwPerItem);
        }

        [TestMethod]
        public void Product_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            Product product = new();
            string part = "Iron Ingot";
            double amount = 1;
            double perMin = 30;
            bool isByProduct = false;

            // Act
            product.part = part;
            product.amount = amount;
            product.perMin = perMin;
            product.isByProduct = isByProduct;

            // Assert
            Assert.AreEqual(part, product.part);
            Assert.AreEqual(amount, product.amount);
            Assert.AreEqual(perMin, product.perMin);
            Assert.AreEqual(isByProduct, product.isByProduct);
        }

        [TestMethod]
        public void PowerIngredient_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            PowerIngredient powerIngredient = new();
            string part = "Coal";
            double perMin = 15;
            double mwPerItem = 4;
            double supplementalRatio = 0.5;

            // Act
            powerIngredient.part = part;
            powerIngredient.perMin = perMin;
            powerIngredient.mwPerItem = mwPerItem;
            powerIngredient.supplementalRatio = supplementalRatio;

            // Assert
            Assert.AreEqual(part, powerIngredient.part);
            Assert.AreEqual(perMin, powerIngredient.perMin);
            Assert.AreEqual(mwPerItem, powerIngredient.mwPerItem);
            Assert.AreEqual(supplementalRatio, powerIngredient.supplementalRatio);
        }

        [TestMethod]
        public void PowerProduct_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            PowerProduct powerProduct = new();
            string part = "Nuclear Waste";
            double perMin = 10;

            // Act
            powerProduct.part = part;
            powerProduct.perMin = perMin;

            // Assert
            Assert.AreEqual(part, powerProduct.part);
            Assert.AreEqual(perMin, powerProduct.perMin);
        }

        [TestMethod]
        public void Recipe_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            Recipe recipe = new();
            string id = "Recipe_IronIngot";
            string displayName = "Iron Ingot";
            List<Ingredient> ingredients = new();
            List<Product> products = new();
            Building building = new();
            bool isAlternate = false;
            bool isFicsmas = false;

            // Act
            recipe.id = id;
            recipe.displayName = displayName;
            recipe.ingredients = ingredients;
            recipe.products = products;
            recipe.building = building;
            recipe.isAlternate = isAlternate;
            recipe.isFicsmas = isFicsmas;

            // Assert
            Assert.AreEqual(id, recipe.id);
            Assert.AreEqual(displayName, recipe.displayName);
            Assert.AreEqual(ingredients, recipe.ingredients);
            Assert.AreEqual(products, recipe.products);
            Assert.AreEqual(building, recipe.building);
            Assert.AreEqual(isAlternate, recipe.isAlternate);
            Assert.AreEqual(isFicsmas, recipe.isFicsmas);
        }

        [TestMethod]
        public void PowerGenerationRecipe_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            PowerGenerationRecipe powerRecipe = new();
            string id = "Recipe_Coal_Power";
            string displayName = "Coal Power";
            List<PowerIngredient> ingredients = new();
            PowerProduct byproduct = new();

            // Act
            powerRecipe.id = id;
            powerRecipe.displayName = displayName;
            powerRecipe.ingredients = ingredients;
            powerRecipe.byproduct = byproduct;

            // Assert
            Assert.AreEqual(id, powerRecipe.id);
            Assert.AreEqual(displayName, powerRecipe.displayName);
            Assert.AreEqual(ingredients, powerRecipe.ingredients);
            Assert.AreEqual(byproduct, powerRecipe.byproduct);
        }
    }
}