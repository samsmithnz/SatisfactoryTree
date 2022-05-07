using DSPTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DSPTree.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class D3GraphTests
{
    [TestMethod]
    public void D3GraphTest()
    {
        //Arrange
        DSPGraph dspGraph = new();
        Graph graph = new(dspGraph.Items);

        //Act

        //Assert
        Assert.IsNotNull(graph);
        Assert.IsTrue(graph.nodes.Count > 0);
        Assert.IsTrue(graph.links.Count > 0);
        //Assert.AreEqual("Iron Ore Vein", graph.Items[0].Name);
        //Assert.AreEqual("80px-Icon_Iron_Ore_Vein.png", graph.Items[0].Image);
        //Assert.AreEqual(0, graph.Items[0].Level);
    }

    [TestMethod]
    public void AssemblerWhenThereAre2InputsTest()
    {
        //Arrange
        DSPGraph graph = new();

        //Act

        //Assert
        foreach (Item item in graph.Items)
        {
            if (item.Recipes[0].Inputs.Count == 2 &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.Assembler &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.Foundry &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.Refinery &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.NuclearPowerPlant&&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.ParticleAccelerator)
            {
                Assert.AreEqual("", item.Name);
                Assert.IsTrue(false);
            }
        }
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void ManufacturerWhenThereAre4InputsTest()
    {
        //Arrange
        DSPGraph graph = new();

        //Act

        //Assert
        foreach (Item item in graph.Items)
        {
            if ((item.Recipes[0].Inputs.Count == 4 || item.Recipes[0].Inputs.Count == 3) &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.Manufacturer &&
                item.Recipes[0].ManufactoringBuilding != ManufactoringBuildingType.Blender)
            {
                Assert.AreEqual("", item.Name);
                Assert.IsTrue(false);
            }
        }
        Assert.IsTrue(true);
    }

    //[TestMethod]
    //public void TreeHasValidParentsAndChildrenTest()
    //{
    //    //Arrange
    //    DSPGraph graph = new("", ResearchType.WhiteScience, true);

    //    //Act

    //    //Assert
    //    Dictionary<string, int> rawMaterials = new();
    //    foreach (Item item in graph.Items)
    //    {
    //        foreach (Recipe recipe in item.Recipes)
    //        {
    //            //Check each input (if it's not gathered)
    //            foreach (KeyValuePair<string, int> input in recipe.Inputs)
    //            {
    //                if (recipe.ManufactoringMethod != ManufactoringMethodType.Gathered)
    //                {
    //                    if (!rawMaterials.ContainsKey(input.Key) == true)
    //                    {
    //                        rawMaterials.Add(input.Key, input.Value);
    //                    }
    //                }
    //            }
    //            //Check each output (if it's not gathered)
    //            foreach (KeyValuePair<string, int> output in recipe.Outputs)
    //            {
    //                if (recipe.ManufactoringMethod != ManufactoringMethodType.Gathered)
    //                {
    //                    if (!rawMaterials.ContainsKey(output.Key) == true)
    //                    {
    //                        rawMaterials.Add(output.Key, output.Value);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    foreach (KeyValuePair<string, int> item in rawMaterials)
    //    {
    //        if (!graph.Items.Where(a => a.Name == item.Key).Any())
    //        {
    //            Assert.AreEqual("child not found", item.Key);
    //        }
    //    }
    //    Assert.IsTrue(true);
    //}

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
}