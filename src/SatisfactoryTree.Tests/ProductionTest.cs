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
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    IronOre[""x1.5 MiningMachine<br>(Iron Ore)""]
    IronOre_Item[90 Iron Ore]
    IronOre--""Iron Ore<br>(90 units/min)""-->IronOre_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.GetMermaidString(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(1, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(90, results[0].Quantity);
            Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
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
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    IronIngot[""x1 Smelter<br>(Iron Ingot)""]
    IronOre[""x0.5 MiningMachine<br>(Iron Ore)""]
    IronIngot_Item[30 Iron Ingot]
    IronOre--""Iron Ore<br>(30 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(30 units/min)""-->IronIngot_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.GetMermaidString(startingItem);
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
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
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
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    IronPlate[""x1.5 Constructor<br>(Iron Plate)""]
    IronIngot[""x1.5 Smelter<br>(Iron Ingot)""]
    IronOre[""x0.5 MiningMachine<br>(Iron Ore)""]
    IronPlate_Item[30 Iron Plate]
    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
    IronOre--""Iron Ore<br>(30 units/min)""-->IronIngot
    IronPlate--""Iron Plate<br>(30 units/min)""-->IronPlate_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.GetMermaidString(startingItem);
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
            Assert.AreEqual("Iron Ore", results[2].Item?.Name);
            Assert.AreEqual(45, results[2].Quantity);
            Assert.AreEqual(1.5M, results[2].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
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
