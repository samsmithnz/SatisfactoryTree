using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System.Diagnostics;

namespace SatisfactoryTree.Logic.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GameFileExtractionTests
    {
        [TestMethod]
        public void TestExtractItemsFromJson()
        {
            // Arrange


            // Act
            NewContent result = GameFileExtractor.ExtractJsonFile();


            // Assert
            Assert.IsNotNull(result);
            //foreach (NewItem item in result.Items)
            //{
            //    Debug.WriteLine($"DisplayName: {item.DisplayName}, ClassName: {item.ClassName}, StackSize: {item.StackSize}");
            //}
            //foreach (NewRecipe item in result.Recipes)
            //{
            //    Debug.WriteLine(($"ClassName: {item.ClassName}, DisplayName: {item.DisplayName}, Ingredients: {item.Ingredients}"));
            //}
            Assert.AreEqual(125, result.Items.Count);
            Assert.AreEqual(266, result.Recipes.Count);
            Assert.AreEqual(11, result.Buildings.Count);
            //Debug.WriteLine(result.Items.Count + " items");
            //Debug.WriteLine(result.Recipes.Count + " recipes");
            //Debug.WriteLine(result.Buildings.Count + " buildings");
            //foreach (Building item in result.Buildings)
            //{
            //    Debug.WriteLine(item.Name);
            //}

        }
    }
}
