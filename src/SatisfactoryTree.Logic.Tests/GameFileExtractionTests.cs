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
        public async Task TestExtractItemsFromJson()
        {
            // Arrange

            // Act
            ExtractedData result = await GameFileExtractor.ProcessFileOldModel();


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
            Assert.AreEqual(168, result.Parts.Count);
            Assert.AreEqual(291, result.Recipes.Count);
            Assert.AreEqual(15, result.Buildings.Count);
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
