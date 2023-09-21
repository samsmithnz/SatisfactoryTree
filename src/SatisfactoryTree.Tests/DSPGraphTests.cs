using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryTree.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class DSPGraphTests
{
    [TestMethod]
    public void DSPGraphTest()
    {
        //Arrange
        SatisfactoryGraph graph = new();

        //Act

        //Assert
        Assert.IsNotNull(graph);
        Assert.IsTrue(graph.Items.Count > 0);
        Assert.AreEqual("Iron Ore", graph.Items[0].Name);
        Assert.AreEqual("Iron_Ore.webp", graph.Items[0].Image);
        Assert.AreEqual(0, graph.Items[0].Level);
    }

    //[TestMethod]
    //public void MatrixTest()
    //{
    //    //Arrange
    //    DSPGraph graph = new();

    //    //Act

    //    //Assert

    //    foreach (Item item in graph.Items)
    //    {
    //        if ((item.Name.ToLower().Contains("matrix") == true &&
    //            item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.MatrixLab) ||
    //            (item.Name.ToLower().Contains("matrix") == false &&
    //            item.Recipes[0].ManufactoringBuilding == ManufactoringBuildingType.MatrixLab))
    //        {
    //            Assert.AreEqual("", item.Name);
    //            Assert.IsTrue(false);
    //        }
    //    }
    //    Assert.IsTrue(true);
    //}

    [TestMethod]
    public void TreeHasValidParentsAndChildrenTest()
    {
        //Arrange
        SatisfactoryGraph graph = new("", ResearchType.Tier8);//, true);

        //Act

        //Assert
        Dictionary<string, decimal> rawMaterials = new();
        foreach (Item item in graph.Items)
        {
            foreach (Recipe recipe in item.Recipes)
            {
                //Check each input (if it's not gathered)
                foreach (KeyValuePair<string, decimal> input in recipe.Inputs)
                {
                    if (!rawMaterials.ContainsKey(input.Key) == true)
                    {
                        rawMaterials.Add(input.Key, input.Value);
                    }
                }
                //Check each output (if it's not gathered)
                foreach (KeyValuePair<string, decimal> output in recipe.Outputs)
                {
                    if (!rawMaterials.ContainsKey(output.Key) == true)
                    {
                        rawMaterials.Add(output.Key, output.Value);
                    }
                }
            }
        }
        foreach (KeyValuePair<string, decimal> item in rawMaterials)
        {
            //if (item.Key == "Nitrogen Gas")
            //{
            //    int i = 4;
            //}
            if (!graph.Items.Where(a => a.Name == item.Key).Any())
            {
                Assert.AreEqual("child not found", item.Key);
            }
        }
        Assert.IsTrue(true);
    }

    //[TestMethod]
    //public void TreeImageIsUsedOnlyOnceTest()
    //{
    //    //Arrange
    //    DSPGraph graph = new("", ResearchType.WhiteScience, true);

    //    //Act

    //    //Assert
    //    HashSet<string> images = new();
    //    foreach (Item item in graph.Items)
    //    {
    //        if (images.Contains(item.Image) == true)
    //        {
    //            Assert.AreEqual("", item.Image);
    //        }
    //        images.Add(item.Image);
    //    }
    //}

    //[TestMethod]
    //public void FilterBlueScienceTest()
    //{
    //    //Arrange
    //    string filter = "Electromagnetic Matrix";
    //    DSPGraph graph = new(filter);

    //    //Act

    //    //Assert
    //    Assert.IsTrue(graph.Items.Count > 1);
    //    Assert.AreEqual(filter, graph.Items[graph.Items.Count - 1].Name);
    //}

    //[TestMethod]
    //public void FilterRedScienceTest()
    //{
    //    //Arrange
    //    string filter = "Energy Matrix";
    //    DSPGraph graph = new(filter);

    //    //Act

    //    //Assert
    //    Assert.IsTrue(graph.Items.Count > 1);
    //    Assert.AreEqual(filter, graph.Items[graph.Items.Count - 1].Name);
    //}

    //[TestMethod]
    //public void FilterItemThatDoesNotExistTest()
    //{
    //    //Arrange
    //    string filter = "Widget";

    //    //Act
    //    try
    //    {
    //        DSPGraph graph = new(filter);
    //    }
    //    catch (Exception ex)
    //    {
    //        //Assert
    //        Assert.AreEqual("Widget item not found", ex.Message);
    //    }

    //}
}