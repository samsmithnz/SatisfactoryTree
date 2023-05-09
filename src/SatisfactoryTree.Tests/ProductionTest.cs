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
        public void IronIngotHalfProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Iron Ingot";
            decimal quantity = 15;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    IronIngot[""x0.5 Smelter<br>(Iron Ingot)""]
    IronOre[""x0.3 MiningMachine<br>(Iron Ore)""]
    IronIngot_Item[15 Iron Ingot]
    IronOre--""Iron Ore<br>(15 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronIngot_Item
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
            Assert.AreEqual(15, results[0].Quantity);
            Assert.AreEqual(0.5M, results[0].BuildingQuantityRequired);
            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
            Assert.AreEqual(15, results[1].Quantity);
            Assert.AreEqual(0.25M, results[1].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }

        [TestMethod]
        public void IronIngotNormalProductionTest()
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
        public void IronIngotDoubleProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Iron Ingot";
            decimal quantity = 60;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    IronIngot[""x2 Smelter<br>(Iron Ingot)""]
    IronOre[""x1 MiningMachine<br>(Iron Ore)""]
    IronIngot_Item[60 Iron Ingot]
    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(60 units/min)""-->IronIngot_Item
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
            Assert.AreEqual(60, results[0].Quantity);
            Assert.AreEqual(2M, results[0].BuildingQuantityRequired);
            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
            Assert.AreEqual(60, results[1].Quantity);
            Assert.AreEqual(1M, results[1].BuildingQuantityRequired);
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
    IronOre[""x0.8 MiningMachine<br>(Iron Ore)""]
    IronPlate_Item[30 Iron Plate]
    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
    IronOre--""Iron Ore<br>(45 units/min)""-->IronIngot
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
            Assert.AreEqual(0.75M, results[2].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }


        [TestMethod]
        public void ReinforcedIronPlateProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Reinforced Iron Plate";
            decimal quantity = 12;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    ReinforcedIronPlate[""x2.4 Assembler<br>(Reinforced Iron Plate)""]
    IronPlate[""x3.6 Constructor<br>(Iron Plate)""]
    IronIngot[""x4.8 Smelter<br>(Iron Ingot)""]
    IronOre[""x2.4 MiningMachine<br>(Iron Ore)""]
    Screw[""x3.6 Constructor<br>(Screw)""]
    IronRod[""x2.4 Constructor<br>(Iron Rod)""]
    ReinforcedIronPlate_Item[12 Reinforced Iron Plate]
    IronPlate--""Iron Plate<br>(72 units/min)""-->ReinforcedIronPlate
    Screw--""Screw<br>(144 units/min)""-->ReinforcedIronPlate
    IronIngot--""Iron Ingot<br>(108 units/min)""-->IronPlate
    IronOre--""Iron Ore<br>(144 units/min)""-->IronIngot
    IronRod--""Iron Rod<br>(36 units/min)""-->Screw
    IronIngot--""Iron Ingot<br>(36 units/min)""-->IronRod
    ReinforcedIronPlate--""Reinforced Iron Plate<br>(12 units/min)""-->ReinforcedIronPlate_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.GetMermaidString(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(6, results.Count);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }


        [TestMethod]
        public void SteelIngotsProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Steel Ingot";
            decimal quantity = 1025;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
    IronOre[""x17.1 MiningMachine<br>(Iron Ore)""]
    Coal[""x17.1 MiningMachine<br>(Coal)""]
    SteelIngot_Item[1025 Steel Ingot]
    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
    SteelIngot--""Steel Ingot<br>(1025 units/min)""-->SteelIngot_Item
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
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }


        [TestMethod]
        public void HeavyModularFramesProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Heavy Modular Frame";
            decimal quantity = 10;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.GetMermaidString(startingItem);
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(15, results.Count);
            Assert.AreEqual(1025, results[9].Dependencies["Iron Ore"]);
            Assert.AreEqual(1025, results[9].Dependencies["Coal"]);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }
    }
}
