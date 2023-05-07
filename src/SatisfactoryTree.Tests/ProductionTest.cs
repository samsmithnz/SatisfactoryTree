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
            SatisfactoryProduction graph = new();
            string itemName = "Iron Ore";
            decimal quantity = 90;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaid = "";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaid = graph.GetMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(1, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(90, results[0].Quantity);
            Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
            Assert.IsNotNull(mermaid);
        }

        [TestMethod]
        public void IronIngotProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new(); 
            string itemName = "Iron Ingot";
            decimal quantity = 30;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaid = "";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaid = graph.GetMermaidString();
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
            Assert.IsNotNull(mermaid);
        }

        [TestMethod]
        public void IronPlateProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new(); 
            string itemName = "Iron Plate";
            decimal quantity = 30;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaid = "";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaid = graph.GetMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(3, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual("Iron Plate", results[0].Item?.Name);
            Assert.AreEqual(30, results[0].Quantity); 
            Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
            Assert.AreEqual("Iron Ingot", results[1].Item?.Name);
            Assert.AreEqual(45, results[1].Quantity);
            Assert.AreEqual(1.5M, results[1].BuildingQuantityRequired);
            Assert.IsNotNull(mermaid);
        }


        //[TestMethod]
        //public void ReinforcedIronPlateProductionTest()
        //{
        //    //Arrange
        //    SatisfactoryProduction graph = new(); 
        //    string itemName = "Reinforced Iron Plate";
        //    decimal quantity = 30;
        //    ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
        //    List<ProductionItem> results = new();

        //    //Act
        //    if (startingItem != null)
        //    {
        //        results = graph.BuildSatisfactoryProductionPlan(startingItem);
        //    }

        //    //Assert
        //    Assert.IsNotNull(startingItem);
        //    //Assert.AreEqual(3, results.Count);
        //    //Assert.IsNotNull(results[0].Item);
        //    //Assert.AreEqual("Iron Plate", results[0].Item?.Name);
        //    //Assert.AreEqual(30, results[0].Quantity); 
        //    //Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
        //    //Assert.AreEqual("Iron Ingot", results[1].Item?.Name);
        //    //Assert.AreEqual(45, results[1].Quantity);
        //    //Assert.AreEqual(1.5M, results[1].BuildingQuantityRequired);
        //}
    }
}
