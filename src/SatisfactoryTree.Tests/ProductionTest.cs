using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionTest
    {
        [TestMethod]
        public void IronOreProductionTest()
        {
            //Arrange
            SatisfactoryGraph graph = new("", ResearchType.Tier8, true);
            string itemName = "Iron Ore";
            decimal quantity = 90;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(1, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(90, results[0].Quantity);
            Assert.AreEqual(0.75M, results[0].BuildingQuantityRequired);
        }

        [TestMethod]
        public void IronIngotProductionTest()
        {
            //Arrange
            SatisfactoryGraph graph = new("", ResearchType.Tier8, true);
            string itemName = "Iron Ingot";
            decimal quantity = 30;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(2, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1M, results[0].BuildingQuantityRequired);
            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
            Assert.AreEqual(30, results[1].Quantity); 
            Assert.AreEqual(0.5M, results[1].BuildingQuantityRequired);

        }


        [TestMethod]
        public void IronPlateProductionTest()
        {
            //Arrange
            SatisfactoryGraph graph = new("", ResearchType.Tier8, true);
            string itemName = "Iron Plate";
            decimal quantity = 30;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(3, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(30, results[0].Quantity); 
            Assert.AreEqual(0.5M, results[0].BuildingQuantityRequired);
            Assert.AreEqual("Iron Ingot", results[1].Item?.Name);
            Assert.AreEqual(45, results[1].Quantity);
            Assert.AreEqual(1M, results[1].BuildingQuantityRequired);
        }
    }
}
