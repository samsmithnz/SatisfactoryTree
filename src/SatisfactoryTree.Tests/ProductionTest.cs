using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionTest
    {
        [TestMethod]
        public void CopperIngotRawProductionTest()
        {
            //Arrange
            SatisfactoryGraph graph = new("", ResearchType.Tier8, true);
            Item? startingItem = graph.FindItem("Copper Ingot");
            List<Item> results = new();

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
            }

            //Assert
            //1 Copper Ore -> Copper Ingot
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(1, results.Count);
            //Assert.IsTrue(rawMaterials.ContainsKey("Copper Ore"));
            //Assert.AreEqual(1, rawMaterials["Copper Ore"]);
        }
    }
}
