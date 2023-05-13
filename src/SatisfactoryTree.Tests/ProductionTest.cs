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
    IronOre_Item([90 Iron Ore])
    IronOre--""Iron Ore<br>(90 units/min)""-->IronOre_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    IronIngot_Item([15 Iron Ingot])
    IronOre[""x0.3 MiningMachine<br>(Iron Ore)""]
    IronOre--""Iron Ore<br>(15 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronIngot_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    IronIngot_Item([30 Iron Ingot])
    IronOre[""x0.5 MiningMachine<br>(Iron Ore)""]
    IronOre--""Iron Ore<br>(30 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(30 units/min)""-->IronIngot_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    IronIngot_Item([60 Iron Ingot])
    IronOre[""x1 MiningMachine<br>(Iron Ore)""]
    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
    IronIngot--""Iron Ingot<br>(60 units/min)""-->IronIngot_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    IronPlate_Item([30 Iron Plate])
    IronIngot[""x1.5 Smelter<br>(Iron Ingot)""]
    IronOre[""x0.8 MiningMachine<br>(Iron Ore)""]
    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
    IronPlate--""Iron Plate<br>(30 units/min)""-->IronPlate_Item
    IronOre--""Iron Ore<br>(45 units/min)""-->IronIngot
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    ReinforcedIronPlate_Item([12 Reinforced Iron Plate])
    IronPlate[""x3.6 Constructor<br>(Iron Plate)""]
    IronIngot[""x4.8 Smelter<br>(Iron Ingot)""]
    IronOre[""x2.4 MiningMachine<br>(Iron Ore)""]
    Screw[""x3.6 Constructor<br>(Screw)""]
    IronRod[""x2.4 Constructor<br>(Iron Rod)""]
    IronPlate--""Iron Plate<br>(72 units/min)""-->ReinforcedIronPlate
    Screw--""Screw<br>(144 units/min)""-->ReinforcedIronPlate
    ReinforcedIronPlate--""Reinforced Iron Plate<br>(12 units/min)""-->ReinforcedIronPlate_Item
    IronIngot--""Iron Ingot<br>(108 units/min)""-->IronPlate
    IronOre--""Iron Ore<br>(144 units/min)""-->IronIngot
    IronRod--""Iron Rod<br>(36 units/min)""-->Screw
    IronIngot--""Iron Ingot<br>(36 units/min)""-->IronRod
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
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
    SteelIngot_Item([1025 Steel Ingot])
    IronOre[""x17.1 MiningMachine<br>(Iron Ore)""]
    Coal[""x17.1 MiningMachine<br>(Coal)""]
    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
    SteelIngot--""Steel Ingot<br>(1025 units/min)""-->SteelIngot_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(3, results.Count);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }


        [TestMethod]
        public void HeavyModularFrameProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Heavy Modular Frame";
            decimal quantity = 10;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    HeavyModularFrame[""x5 Manufacturer<br>(Heavy Modular Frame)""]
    HeavyModularFrame_Item([10 Heavy Modular Frame])
    ModularFrame[""x25 Assembler<br>(Modular Frame)""]
    ReinforcedIronPlate[""x15 Assembler<br>(Reinforced Iron Plate)""]
    IronPlate[""x22.5 Constructor<br>(Iron Plate)""]
    IronIngot[""x48.4 Smelter<br>(Iron Ingot)""]
    IronOre[""x41.3 MiningMachine<br>(Iron Ore)""]
    Screw[""x47.5 Constructor<br>(Screw)""]
    IronRod[""x51.7 Constructor<br>(Iron Rod)""]
    SteelPipe[""x7.5 Constructor<br>(Steel Pipe)""]
    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
    Coal[""x17.1 MiningMachine<br>(Coal)""]
    EncasedIndustrialBeam[""x8.4 Assembler<br>(Encased Industrial Beam)""]
    SteelBeam[""x13.4 Constructor<br>(Steel Beam)""]
    Concrete[""x16.7 Constructor<br>(Concrete)""]
    Limestone[""x12.5 MiningMachine<br>(Limestone)""]
    ModularFrame--""Modular Frame<br>(50 units/min)""-->HeavyModularFrame
    SteelPipe--""Steel Pipe<br>(150 units/min)""-->HeavyModularFrame
    EncasedIndustrialBeam--""Encased Industrial Beam<br>(50 units/min)""-->HeavyModularFrame
    Screw--""Screw<br>(1000 units/min)""-->HeavyModularFrame
    HeavyModularFrame--""Heavy Modular Frame<br>(10 units/min)""-->HeavyModularFrame_Item
    ReinforcedIronPlate--""Reinforced Iron Plate<br>(75 units/min)""-->ModularFrame
    IronRod--""Iron Rod<br>(300 units/min)""-->ModularFrame
    IronPlate--""Iron Plate<br>(450 units/min)""-->ReinforcedIronPlate
    Screw--""Screw<br>(900 units/min)""-->ReinforcedIronPlate
    IronIngot--""Iron Ingot<br>(675 units/min)""-->IronPlate
    IronOre--""Iron Ore<br>(1450 units/min)""-->IronIngot
    IronRod--""Iron Rod<br>(475 units/min)""-->Screw
    IronIngot--""Iron Ingot<br>(775 units/min)""-->IronRod
    SteelIngot--""Steel Ingot<br>(225 units/min)""-->SteelPipe
    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
    SteelBeam--""Steel Beam<br>(200.0 units/min)""-->EncasedIndustrialBeam
    Concrete--""Concrete<br>(250.0 units/min)""-->EncasedIndustrialBeam
    SteelIngot--""Steel Ingot<br>(800 units/min)""-->SteelBeam
    Limestone--""Limestone<br>(750.0 units/min)""-->Concrete
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(15, results.Count);
            Assert.AreEqual(1025, results[9].Dependencies["Iron Ore"]);
            Assert.AreEqual(1025, results[9].Dependencies["Coal"]);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }


        [TestMethod]
        public void MotorProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Motor";
            decimal quantity = 5;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    Motor[""x1 Assembler<br>(Motor)""]
    Motor_Item([5 Motor])
    Rotor[""x2.5 Assembler<br>(Rotor)""]
    IronRod[""x7.5 Constructor<br>(Iron Rod)""]
    IronIngot[""x3.8 Smelter<br>(Iron Ingot)""]
    IronOre[""x2.7 MiningMachine<br>(Iron Ore)""]
    Screw[""x6.3 Constructor<br>(Screw)""]
    Stator[""x2 Assembler<br>(Stator)""]
    SteelPipe[""x1.5 Constructor<br>(Steel Pipe)""]
    SteelIngot[""x1 Foundry<br>(Steel Ingot)""]
    Coal[""x0.8 MiningMachine<br>(Coal)""]
    Wire[""x2.7 Constructor<br>(Wire)""]
    CopperIngot[""x1.4 Smelter<br>(Copper Ingot)""]
    CopperOre[""x0.7 MiningMachine<br>(Copper Ore)""]
    Rotor--""Rotor<br>(10 units/min)""-->Motor
    Stator--""Stator<br>(10 units/min)""-->Motor
    Motor--""Motor<br>(5 units/min)""-->Motor_Item
    IronRod--""Iron Rod<br>(50 units/min)""-->Rotor
    Screw--""Screw<br>(250 units/min)""-->Rotor
    IronIngot--""Iron Ingot<br>(112.5 units/min)""-->IronRod
    IronOre--""Iron Ore<br>(112.5 units/min)""-->IronIngot
    IronRod--""Iron Rod<br>(62.5 units/min)""-->Screw
    SteelPipe--""Steel Pipe<br>(30 units/min)""-->Stator
    Wire--""Wire<br>(80 units/min)""-->Stator
    SteelIngot--""Steel Ingot<br>(45 units/min)""-->SteelPipe
    IronOre--""Iron Ore<br>(45 units/min)""-->SteelIngot
    Coal--""Coal<br>(45 units/min)""-->SteelIngot
    CopperIngot--""Copper Ingot<br>(40 units/min)""-->Wire
    CopperOre--""Copper Ore<br>(40.0 units/min)""-->CopperIngot
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(13, results.Count);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }

        [TestMethod]
        public void PlasticProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Plastic";
            decimal quantity = 20;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    Plastic[""x1 Refinery<br>(Plastic)""]
    Plastic_Item[20 Plastic]
    CrudeOil[""x0.3 OilExtractor<br>(Crude Oil)""]
    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
    Plastic--""Plastic<br>(20 units/min)""-->Plastic_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(3, results.Count);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }

        [TestMethod]
        public void CircuitBoardProductionTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Circuit Board";
            decimal quantity = 5;
            ProductionItem? startingItem = new(graph.FindItem(itemName), quantity);
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    CircuitBoard[""x0.7 Assembler<br>(Circuit Board)""]
    CircuitBoard_Item[5 Circuit Board]
    CopperSheet[""x1 Constructor<br>(Copper Sheet)""]
    CopperIngot[""x0.7 Smelter<br>(Copper Ingot)""]
    CopperOre[""x0.4 MiningMachine<br>(Copper Ore)""]
    Plastic[""x1 Refinery<br>(Plastic)""]
    CrudeOil[""x0.3 OilExtractor<br>(Crude Oil)""]
    CopperSheet--""Copper Sheet<br>(10 units/min)""-->CircuitBoard
    Plastic--""Plastic<br>(20.0 units/min)""-->CircuitBoard
    CopperIngot--""Copper Ingot<br>(20 units/min)""-->CopperSheet
    CopperOre--""Copper Ore<br>(20.0 units/min)""-->CopperIngot
    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
    CircuitBoard--""Circuit Board<br>(5 units/min)""-->CircuitBoard_Item
";

            //Act
            if (startingItem != null)
            {
                results = graph.BuildSatisfactoryProductionPlan(startingItem);
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(startingItem);
            Assert.AreEqual(7, results.Count);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }
    }
}
