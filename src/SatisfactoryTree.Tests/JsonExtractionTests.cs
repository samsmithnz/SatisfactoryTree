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
            //foreach (string item in result.ItemList)
            //{
            //    Debug.WriteLine(item);
            //}
            //foreach (string item in result.RecipeList)
            //{
            //    Debug.WriteLine(item);
            //}
            Debug.WriteLine(result.Items.Count + " items");
            Debug.WriteLine(result.Recipes.Count + " recipes");
        }
    }
}
