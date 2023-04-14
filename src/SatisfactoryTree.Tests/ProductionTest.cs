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
            string itemName = "Copper Ingot";
            decimal quantity = 30;
            ProductionItem? startingItem = new(graph.FindItem(itemName), null, quantity);
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
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual("Copper Ore", results[1].Item.Name);
            Assert.AreEqual(30, results[1].Quantity);
            //Assert.IsTrue(rawMaterials.ContainsKey("Copper Ore"));
            //Assert.AreEqual(1, rawMaterials["Copper Ore"]);
        }
    }
}
