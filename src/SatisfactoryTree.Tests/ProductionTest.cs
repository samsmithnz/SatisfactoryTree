using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionTest
    {
        [TestMethod]
        public void CopperIngotProductionTest()
        {
            //Arrange
            SatisfactoryGraph graph = new("", ResearchType.Tier8, true);
            ProductionItem? startingItem = new ProductionItem(graph.FindItem("Copper Ingot"), null, 30);
            List<ProductionItem> results = new();

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
            }

            //Assert
            //1 Copper Ore -> Copper Ingot
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(2, results.Count);
            //Assert.IsNotNull(results[0].Building)
            //Assert.IsTrue(rawMaterials.ContainsKey("Copper Ore"));
            //Assert.AreEqual(1, rawMaterials["Copper Ore"]);
        }
    }
}
