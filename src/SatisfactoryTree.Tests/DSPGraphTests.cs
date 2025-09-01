using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryTree.Tests;

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
        Assert.AreEqual("IronOre_256.png", graph.Items[0].Image);
        Assert.AreEqual(0, graph.Items[0].Level);
    }

    [TestMethod]
    public void TreeHasValidParentsAndChildrenTest()
    {
        //Arrange
        SatisfactoryGraph graph = new("", ResearchType.Tier8);

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
                    if (!rawMaterials.ContainsKey(input.Key))
                    {
                        rawMaterials.Add(input.Key, input.Value);
                    }
                }
                //Check each output (if it's not gathered)
                foreach (KeyValuePair<string, decimal> output in recipe.Outputs)
                {
                    if (!rawMaterials.ContainsKey(output.Key))
                    {
                        rawMaterials.Add(output.Key, output.Value);
                    }
                }
            }
        }
        foreach (KeyValuePair<string, decimal> item in rawMaterials)
        {
            if (!graph.Items.Where(a => a.Name == item.Key).Any())
            {
                Assert.AreEqual("child not found", item.Key);
            }
        }
        Assert.IsTrue(true);
    }
}