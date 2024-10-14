using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.ContentExtractor;
using SatisfactoryTree.Models;

namespace SatisfactoryTree.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class JsonExtractionTests
    {
        [TestMethod]
        public void TestExtractItemsFromJson()
        {
            // Arrange


            // Act
            NewContent result = JsonExtraction.ExtractJsonFile();


            // Assert
            Assert.IsNotNull(result);
            foreach (NewItem item in result.Items)
            {
                Debug.WriteLine($"DisplayName: {item.DisplayName}, ClassName: {item.ClassName}, StackSize: {item.StackSize}");
            }
            foreach (NewRecipe item in result.Recipes)
            {
                Debug.WriteLine(($"ClassName: {item.ClassName}, DisplayName: {item.DisplayName}, Ingredients: {item.Ingredients}"));
            }
            Debug.WriteLine(result.Items.Count + " items");
            Debug.WriteLine(result.Recipes.Count + " recipes");
        }
    }
}
