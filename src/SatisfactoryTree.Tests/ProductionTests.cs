﻿//using SatisfactoryTree.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;

//namespace SatisfactoryTree.Tests
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class ProductionTests
//    {
//        [TestMethod]
//        public void IronIngotProductionTest()
//        {
//            //Arrange
//            SatisfactoryProduction graph = new();
//            string itemName = "Iron Ingot";
//            decimal quantity = 90;
//            NewItem? newItem = graph.FindItem(itemName);
//            NewTargetItem? itemTarget = null;
//            if (newItem != null)
//            {
//                itemTarget = new(newItem.DisplayName, quantity);
//            }
//            ProductionCalculation? productionPlan = null;
//            List<ProductionItem> results = new();
//            string mermaidResult = "";
//            string expectedResult = @"flowchart LR
//                IronIngot[""x3 Smelter<br>(Iron Ingot)""]
//                IronIngot_Item([90 Iron Ingot])
//                IronOre[""x3 Mining Machine Mk1<br>(Iron Ore)""]
//                IronOre--""Iron Ore<br>(90 units/min)""-->IronIngot
//                IronIngot--""Iron Ingot<br>(90 units/min)""-->IronIngot_Item
//            ";

//            //Act
//            if (itemTarget != null)
//            {
//                productionPlan = graph.NewBuildProductionPlan(itemTarget);
//                results = productionPlan.ProductionItems;
//                mermaidResult = graph.ToMermaidString();
//            }

//            //Assert
//            Assert.IsNotNull(itemTarget);
//            Assert.IsNotNull(productionPlan);
//            Assert.AreEqual(7.5M, productionPlan.PowerConsumption);
//            Assert.AreEqual(1, results.Count);
//            Assert.IsNotNull(results[0].ItemName);
//            Assert.AreEqual(90, results[0].Quantity);
//            Assert.AreEqual(3M, results[0].BuildingQuantityRequired);
//            Assert.IsNotNull(mermaidResult);
//            Assert.AreEqual(expectedResult, mermaidResult);
//        }

//        //        [TestMethod]
//        //        public void IronIngotHalfProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 15;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x0.5 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([15 Iron Ingot])
//        //    IronOre[""x0.3 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(15 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(3.25M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(15, results[0].Quantity);
//        //            Assert.AreEqual(0.5M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(15, results[1].Quantity);
//        //            Assert.AreEqual(0.25M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronIngotNormalProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 30;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x1 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([30 Iron Ingot])
//        //    IronOre[""x0.5 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(30 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(30 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(6.5M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(30, results[0].Quantity);
//        //            Assert.AreEqual(1M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(30, results[1].Quantity);
//        //            Assert.AreEqual(0.5M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronIngotDoubleProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 60;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x2 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([60 Iron Ingot])
//        //    IronOre[""x1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(60 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(13M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(60, results[0].Quantity);
//        //            Assert.AreEqual(2M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(60, results[1].Quantity);
//        //            Assert.AreEqual(1M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronPlateProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Plate";
//        //            decimal quantity = 30;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronPlate[""x1.5 Constructor<br>(Iron Plate)""]
//        //    IronPlate_Item([30 Iron Plate])
//        //    IronIngot[""x1.5 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x0.8 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->IronPlate_Item
//        //    IronOre--""Iron Ore<br>(45 units/min)""-->IronIngot
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(15.75M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual("Iron Plate", results[0].Item?.Name);
//        //            Assert.AreEqual(30, results[0].Quantity);
//        //            Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ingot", results[1].Item?.Name);
//        //            Assert.AreEqual(45, results[1].Quantity);
//        //            Assert.AreEqual(1.5M, results[1].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[2].Item?.Name);
//        //            Assert.AreEqual(45, results[2].Quantity);
//        //            Assert.AreEqual(0.75M, results[2].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void ReinforcedIronPlateProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Reinforced Iron Plate";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string mermaidWithImagesResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    ReinforcedIronPlate[""x1 Assembler<br>(Reinforced Iron Plate)""]
//        //    ReinforcedIronPlate_Item([5 Reinforced Iron Plate])
//        //    IronPlate[""x1.5 Constructor<br>(Iron Plate)""]
//        //    IronIngot[""x2 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x1.5 Constructor<br>(Screw)""]
//        //    IronRod[""x1 Constructor<br>(Iron Rod)""]
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(60 units/min)""-->ReinforcedIronPlate
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(5 units/min)""-->ReinforcedIronPlate_Item
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(15 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronRod
//        //";
//        //            string expectedWithImagesResult = @"flowchart LR
//        //    ReinforcedIronPlate[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/AssemblerMk1_256.png' style='max-width:100px' alt='Assembler'></span><br> x1 Assembler<br>(Reinforced Iron Plate)</div>""]
//        //    ReinforcedIronPlate_Item([""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Items/ReinforcedIronPlate_256.png' style='max-width:100px' alt='Reinforced Iron Plate'></span><br> x5 Reinforced Iron Plate</div>""])
//        //    IronPlate[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1.5 Constructor<br>(Iron Plate)</div>""]
//        //    IronIngot[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/SmelterMk1_256.png' style='max-width:100px' alt='Smelter'></span><br> x2 Smelter<br>(Iron Ingot)</div>""]
//        //    IronOre[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/MinerMk1_256.png' style='max-width:100px' alt='Mining Machine Mk1'></span><br> x1 Mining Machine Mk1<br>(Iron Ore)</div>""]
//        //    Screw[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1.5 Constructor<br>(Screw)</div>""]
//        //    IronRod[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1 Constructor<br>(Iron Rod)</div>""]
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(60 units/min)""-->ReinforcedIronPlate
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(5 units/min)""-->ReinforcedIronPlate_Item
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(15 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronRod
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //                mermaidWithImagesResult = graph.ToMermaidString(true);
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(44.0M, result.PowerConsumption);
//        //            Assert.AreEqual(6, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //            Assert.IsNotNull(mermaidWithImagesResult);
//        //            Assert.AreEqual(expectedWithImagesResult, mermaidWithImagesResult);
//        //        }


//        //        [TestMethod]
//        //        public void SteelIngotsProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Steel Ingot";
//        //            decimal quantity = 1025;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
//        //    SteelIngot_Item([1025 Steel Ingot])
//        //    IronOre[""x17.1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Coal[""x17.1 Mining Machine Mk1<br>(Coal)""]
//        //    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
//        //    SteelIngot--""Steel Ingot<br>(1025 units/min)""-->SteelIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(535.28M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void HeavyModularFrameProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Heavy Modular Frame";
//        //            decimal quantity = 10;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    HeavyModularFrame[""x5 Manufacturer<br>(Heavy Modular Frame)""]
//        //    HeavyModularFrame_Item([10 Heavy Modular Frame])
//        //    ModularFrame[""x25 Assembler<br>(Modular Frame)""]
//        //    ReinforcedIronPlate[""x15 Assembler<br>(Reinforced Iron Plate)""]
//        //    IronPlate[""x22.5 Constructor<br>(Iron Plate)""]
//        //    IronIngot[""x48.4 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x41.3 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x47.5 Constructor<br>(Screw)""]
//        //    IronRod[""x51.7 Constructor<br>(Iron Rod)""]
//        //    SteelPipe[""x7.5 Constructor<br>(Steel Pipe)""]
//        //    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
//        //    Coal[""x17.1 Mining Machine Mk1<br>(Coal)""]
//        //    EncasedIndustrialBeam[""x8.4 Assembler<br>(Encased Industrial Beam)""]
//        //    SteelBeam[""x13.4 Constructor<br>(Steel Beam)""]
//        //    Concrete[""x16.7 Constructor<br>(Concrete)""]
//        //    Limestone[""x12.5 Mining Machine Mk1<br>(Limestone)""]
//        //    ModularFrame--""Modular Frame<br>(50 units/min)""-->HeavyModularFrame
//        //    SteelPipe--""Steel Pipe<br>(150 units/min)""-->HeavyModularFrame
//        //    EncasedIndustrialBeam--""Encased Industrial Beam<br>(50 units/min)""-->HeavyModularFrame
//        //    Screw--""Screw<br>(1000 units/min)""-->HeavyModularFrame
//        //    HeavyModularFrame--""Heavy Modular Frame<br>(10 units/min)""-->HeavyModularFrame_Item
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(75 units/min)""-->ModularFrame
//        //    IronRod--""Iron Rod<br>(300 units/min)""-->ModularFrame
//        //    IronPlate--""Iron Plate<br>(450 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(900 units/min)""-->ReinforcedIronPlate
//        //    IronIngot--""Iron Ingot<br>(675 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(1450 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(475 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(775 units/min)""-->IronRod
//        //    SteelIngot--""Steel Ingot<br>(225 units/min)""-->SteelPipe
//        //    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
//        //    SteelBeam--""Steel Beam<br>(200.0 units/min)""-->EncasedIndustrialBeam
//        //    Concrete--""Concrete<br>(250.0 units/min)""-->EncasedIndustrialBeam
//        //    SteelIngot--""Steel Ingot<br>(800 units/min)""-->SteelBeam
//        //    Limestone--""Limestone<br>(750.0 units/min)""-->Concrete
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(2548.61M, result.PowerConsumption);
//        //            Assert.AreEqual(15, results.Count);
//        //            Assert.AreEqual(1025, results[9].Dependencies["Iron Ore"]);
//        //            Assert.AreEqual(1025, results[9].Dependencies["Coal"]);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void MotorProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Motor";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    Motor[""x1 Assembler<br>(Motor)""]
//        //    Motor_Item([5 Motor])
//        //    Rotor[""x2.5 Assembler<br>(Rotor)""]
//        //    IronRod[""x7.5 Constructor<br>(Iron Rod)""]
//        //    IronIngot[""x3.8 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x2.7 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x6.3 Constructor<br>(Screw)""]
//        //    Stator[""x2 Assembler<br>(Stator)""]
//        //    SteelPipe[""x1.5 Constructor<br>(Steel Pipe)""]
//        //    SteelIngot[""x1 Foundry<br>(Steel Ingot)""]
//        //    Coal[""x0.8 Mining Machine Mk1<br>(Coal)""]
//        //    Wire[""x2.7 Constructor<br>(Wire)""]
//        //    CopperIngot[""x1.4 Smelter<br>(Copper Ingot)""]
//        //    CopperOre[""x0.7 Mining Machine Mk1<br>(Copper Ore)""]
//        //    Rotor--""Rotor<br>(10 units/min)""-->Motor
//        //    Stator--""Stator<br>(10 units/min)""-->Motor
//        //    Motor--""Motor<br>(5 units/min)""-->Motor_Item
//        //    IronRod--""Iron Rod<br>(50 units/min)""-->Rotor
//        //    Screw--""Screw<br>(250 units/min)""-->Rotor
//        //    IronIngot--""Iron Ingot<br>(112.5 units/min)""-->IronRod
//        //    IronOre--""Iron Ore<br>(112.5 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(62.5 units/min)""-->Screw
//        //    SteelPipe--""Steel Pipe<br>(30 units/min)""-->Stator
//        //    Wire--""Wire<br>(80 units/min)""-->Stator
//        //    SteelIngot--""Steel Ingot<br>(45 units/min)""-->SteelPipe
//        //    IronOre--""Iron Ore<br>(45 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(45 units/min)""-->SteelIngot
//        //    CopperIngot--""Copper Ingot<br>(40 units/min)""-->Wire
//        //    CopperOre--""Copper Ore<br>(40.0 units/min)""-->CopperIngot
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(210.71M, result.PowerConsumption);
//        //            Assert.AreEqual(13, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void PlasticProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Plastic";
//        //            decimal quantity = 20;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    Plastic[""x1 Refinery<br>(Plastic)""]
//        //    Plastic_Item([20 Plastic])
//        //    HeavyOilResidue_Item([10 Heavy Oil Residue])
//        //    CrudeOil[""x0.3 Oil Extractor<br>(Crude Oil)""]
//        //    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
//        //    Plastic--""Plastic<br>(20 units/min)""-->Plastic_Item
//        //    Plastic--""Heavy Oil Residue<br>(10 units/min)""-->HeavyOilResidue_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(40M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void CircuitBoardProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Circuit Board";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    CircuitBoard[""x0.7 Assembler<br>(Circuit Board)""]
//        //    CircuitBoard_Item([5 Circuit Board])
//        //    CopperSheet[""x1 Constructor<br>(Copper Sheet)""]
//        //    CopperIngot[""x0.7 Smelter<br>(Copper Ingot)""]
//        //    CopperOre[""x0.4 Mining Machine Mk1<br>(Copper Ore)""]
//        //    Plastic[""x1 Refinery<br>(Plastic)""]
//        //    HeavyOilResidue_Item([10 Heavy Oil Residue])
//        //    CrudeOil[""x0.3 Oil Extractor<br>(Crude Oil)""]
//        //    CopperSheet--""Copper Sheet<br>(10 units/min)""-->CircuitBoard
//        //    Plastic--""Plastic<br>(20.1 units/min)""-->CircuitBoard
//        //    CircuitBoard--""Circuit Board<br>(5 units/min)""-->CircuitBoard_Item
//        //    CopperIngot--""Copper Ingot<br>(20 units/min)""-->CopperSheet
//        //    CopperOre--""Copper Ore<br>(20.1 units/min)""-->CopperIngot
//        //    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
//        //    Plastic--""Heavy Oil Residue<br>(10 units/min)""-->HeavyOilResidue_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(58.33M, result.PowerConsumption);
//        //            Assert.AreEqual(7, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void AluminumIngotProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Aluminum Ingot";
//        //            decimal quantity = 10;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    AluminumIngot[""x0.2 Foundry<br>(Aluminum Ingot)""]
//        //    AluminumIngot_Item([10 Aluminum Ingot])
//        //    AluminumScrap[""x0.1 Assembler<br>(Aluminum Scrap)""]
//        //    Water[""x0.2 Water Extractor<br>(Water)""]
//        //    AluminaSolution[""x0.1 Refinery<br>(Alumina Solution)""]
//        //    Silica[""x0.5 Constructor<br>(Silica)""]
//        //    Bauxite[""x0.2 Mining Machine Mk1<br>(Bauxite)""]
//        //    Coal[""x0.1 Mining Machine Mk1<br>(Coal)""]
//        //    RawQuartz[""x0.2 Assembler<br>(Raw Quartz)""]
//        //    AluminumScrap--""Aluminum Scrap<br>(15.1 units/min)""-->AluminumIngot
//        //    Silica--""Silica<br>(12.6 units/min)""-->AluminumIngot
//        //    AluminumIngot--""Aluminum Ingot<br>(10 units/min)""-->AluminumIngot_Item
//        //    AluminaSolution--""Alumina Solution<br>(10.1 units/min)""-->AluminumScrap
//        //    Coal--""Coal<br>(5.1 units/min)""-->AluminumScrap
//        //    Bauxite--""Bauxite<br>(10.1 units/min)""-->AluminaSolution
//        //    Water--""Water<br>(15.1 units/min)""-->AluminaSolution
//        //    RawQuartz--""Raw Quartz<br>(30.1 units/min)""-->Silica
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(12.75M, result.PowerConsumption);
//        //            Assert.AreEqual(8, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }//        [TestMethod]
//        //        public void IronIngotHalfProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 15;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x0.5 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([15 Iron Ingot])
//        //    IronOre[""x0.3 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(15 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(3.25M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(15, results[0].Quantity);
//        //            Assert.AreEqual(0.5M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(15, results[1].Quantity);
//        //            Assert.AreEqual(0.25M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronIngotNormalProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 30;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x1 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([30 Iron Ingot])
//        //    IronOre[""x0.5 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(30 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(30 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(6.5M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(30, results[0].Quantity);
//        //            Assert.AreEqual(1M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(30, results[1].Quantity);
//        //            Assert.AreEqual(0.5M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronIngotDoubleProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Ingot";
//        //            decimal quantity = 60;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronIngot[""x2 Smelter<br>(Iron Ingot)""]
//        //    IronIngot_Item([60 Iron Ingot])
//        //    IronOre[""x1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronIngot--""Iron Ingot<br>(60 units/min)""-->IronIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(13M, result.PowerConsumption);
//        //            Assert.AreEqual(2, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual(60, results[0].Quantity);
//        //            Assert.AreEqual(2M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[1].Item?.Name);
//        //            Assert.AreEqual(60, results[1].Quantity);
//        //            Assert.AreEqual(1M, results[1].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void IronPlateProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Iron Plate";
//        //            decimal quantity = 30;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    IronPlate[""x1.5 Constructor<br>(Iron Plate)""]
//        //    IronPlate_Item([30 Iron Plate])
//        //    IronIngot[""x1.5 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x0.8 Mining Machine Mk1<br>(Iron Ore)""]
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->IronPlate_Item
//        //    IronOre--""Iron Ore<br>(45 units/min)""-->IronIngot
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(15.75M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(results[0].Item);
//        //            Assert.AreEqual("Iron Plate", results[0].Item?.Name);
//        //            Assert.AreEqual(30, results[0].Quantity);
//        //            Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ingot", results[1].Item?.Name);
//        //            Assert.AreEqual(45, results[1].Quantity);
//        //            Assert.AreEqual(1.5M, results[1].BuildingQuantityRequired);
//        //            Assert.AreEqual("Iron Ore", results[2].Item?.Name);
//        //            Assert.AreEqual(45, results[2].Quantity);
//        //            Assert.AreEqual(0.75M, results[2].BuildingQuantityRequired);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void ReinforcedIronPlateProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Reinforced Iron Plate";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string mermaidWithImagesResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    ReinforcedIronPlate[""x1 Assembler<br>(Reinforced Iron Plate)""]
//        //    ReinforcedIronPlate_Item([5 Reinforced Iron Plate])
//        //    IronPlate[""x1.5 Constructor<br>(Iron Plate)""]
//        //    IronIngot[""x2 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x1.5 Constructor<br>(Screw)""]
//        //    IronRod[""x1 Constructor<br>(Iron Rod)""]
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(60 units/min)""-->ReinforcedIronPlate
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(5 units/min)""-->ReinforcedIronPlate_Item
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(15 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronRod
//        //";
//        //            string expectedWithImagesResult = @"flowchart LR
//        //    ReinforcedIronPlate[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/AssemblerMk1_256.png' style='max-width:100px' alt='Assembler'></span><br> x1 Assembler<br>(Reinforced Iron Plate)</div>""]
//        //    ReinforcedIronPlate_Item([""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Items/ReinforcedIronPlate_256.png' style='max-width:100px' alt='Reinforced Iron Plate'></span><br> x5 Reinforced Iron Plate</div>""])
//        //    IronPlate[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1.5 Constructor<br>(Iron Plate)</div>""]
//        //    IronIngot[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/SmelterMk1_256.png' style='max-width:100px' alt='Smelter'></span><br> x2 Smelter<br>(Iron Ingot)</div>""]
//        //    IronOre[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/MinerMk1_256.png' style='max-width:100px' alt='Mining Machine Mk1'></span><br> x1 Mining Machine Mk1<br>(Iron Ore)</div>""]
//        //    Screw[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1.5 Constructor<br>(Screw)</div>""]
//        //    IronRod[""<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/ConstructorMk1_256.png' style='max-width:100px' alt='Constructor'></span><br> x1 Constructor<br>(Iron Rod)</div>""]
//        //    IronPlate--""Iron Plate<br>(30 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(60 units/min)""-->ReinforcedIronPlate
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(5 units/min)""-->ReinforcedIronPlate_Item
//        //    IronIngot--""Iron Ingot<br>(45 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(60 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(15 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(15 units/min)""-->IronRod
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //                mermaidWithImagesResult = graph.ToMermaidString(true);
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(44.0M, result.PowerConsumption);
//        //            Assert.AreEqual(6, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //            Assert.IsNotNull(mermaidWithImagesResult);
//        //            Assert.AreEqual(expectedWithImagesResult, mermaidWithImagesResult);
//        //        }


//        //        [TestMethod]
//        //        public void SteelIngotsProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Steel Ingot";
//        //            decimal quantity = 1025;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
//        //    SteelIngot_Item([1025 Steel Ingot])
//        //    IronOre[""x17.1 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Coal[""x17.1 Mining Machine Mk1<br>(Coal)""]
//        //    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
//        //    SteelIngot--""Steel Ingot<br>(1025 units/min)""-->SteelIngot_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(535.28M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void HeavyModularFrameProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Heavy Modular Frame";
//        //            decimal quantity = 10;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    HeavyModularFrame[""x5 Manufacturer<br>(Heavy Modular Frame)""]
//        //    HeavyModularFrame_Item([10 Heavy Modular Frame])
//        //    ModularFrame[""x25 Assembler<br>(Modular Frame)""]
//        //    ReinforcedIronPlate[""x15 Assembler<br>(Reinforced Iron Plate)""]
//        //    IronPlate[""x22.5 Constructor<br>(Iron Plate)""]
//        //    IronIngot[""x48.4 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x41.3 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x47.5 Constructor<br>(Screw)""]
//        //    IronRod[""x51.7 Constructor<br>(Iron Rod)""]
//        //    SteelPipe[""x7.5 Constructor<br>(Steel Pipe)""]
//        //    SteelIngot[""x22.8 Foundry<br>(Steel Ingot)""]
//        //    Coal[""x17.1 Mining Machine Mk1<br>(Coal)""]
//        //    EncasedIndustrialBeam[""x8.4 Assembler<br>(Encased Industrial Beam)""]
//        //    SteelBeam[""x13.4 Constructor<br>(Steel Beam)""]
//        //    Concrete[""x16.7 Constructor<br>(Concrete)""]
//        //    Limestone[""x12.5 Mining Machine Mk1<br>(Limestone)""]
//        //    ModularFrame--""Modular Frame<br>(50 units/min)""-->HeavyModularFrame
//        //    SteelPipe--""Steel Pipe<br>(150 units/min)""-->HeavyModularFrame
//        //    EncasedIndustrialBeam--""Encased Industrial Beam<br>(50 units/min)""-->HeavyModularFrame
//        //    Screw--""Screw<br>(1000 units/min)""-->HeavyModularFrame
//        //    HeavyModularFrame--""Heavy Modular Frame<br>(10 units/min)""-->HeavyModularFrame_Item
//        //    ReinforcedIronPlate--""Reinforced Iron Plate<br>(75 units/min)""-->ModularFrame
//        //    IronRod--""Iron Rod<br>(300 units/min)""-->ModularFrame
//        //    IronPlate--""Iron Plate<br>(450 units/min)""-->ReinforcedIronPlate
//        //    Screw--""Screw<br>(900 units/min)""-->ReinforcedIronPlate
//        //    IronIngot--""Iron Ingot<br>(675 units/min)""-->IronPlate
//        //    IronOre--""Iron Ore<br>(1450 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(475 units/min)""-->Screw
//        //    IronIngot--""Iron Ingot<br>(775 units/min)""-->IronRod
//        //    SteelIngot--""Steel Ingot<br>(225 units/min)""-->SteelPipe
//        //    IronOre--""Iron Ore<br>(1025 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(1025 units/min)""-->SteelIngot
//        //    SteelBeam--""Steel Beam<br>(200.0 units/min)""-->EncasedIndustrialBeam
//        //    Concrete--""Concrete<br>(250.0 units/min)""-->EncasedIndustrialBeam
//        //    SteelIngot--""Steel Ingot<br>(800 units/min)""-->SteelBeam
//        //    Limestone--""Limestone<br>(750.0 units/min)""-->Concrete
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(2548.61M, result.PowerConsumption);
//        //            Assert.AreEqual(15, results.Count);
//        //            Assert.AreEqual(1025, results[9].Dependencies["Iron Ore"]);
//        //            Assert.AreEqual(1025, results[9].Dependencies["Coal"]);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }


//        //        [TestMethod]
//        //        public void MotorProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Motor";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    Motor[""x1 Assembler<br>(Motor)""]
//        //    Motor_Item([5 Motor])
//        //    Rotor[""x2.5 Assembler<br>(Rotor)""]
//        //    IronRod[""x7.5 Constructor<br>(Iron Rod)""]
//        //    IronIngot[""x3.8 Smelter<br>(Iron Ingot)""]
//        //    IronOre[""x2.7 Mining Machine Mk1<br>(Iron Ore)""]
//        //    Screw[""x6.3 Constructor<br>(Screw)""]
//        //    Stator[""x2 Assembler<br>(Stator)""]
//        //    SteelPipe[""x1.5 Constructor<br>(Steel Pipe)""]
//        //    SteelIngot[""x1 Foundry<br>(Steel Ingot)""]
//        //    Coal[""x0.8 Mining Machine Mk1<br>(Coal)""]
//        //    Wire[""x2.7 Constructor<br>(Wire)""]
//        //    CopperIngot[""x1.4 Smelter<br>(Copper Ingot)""]
//        //    CopperOre[""x0.7 Mining Machine Mk1<br>(Copper Ore)""]
//        //    Rotor--""Rotor<br>(10 units/min)""-->Motor
//        //    Stator--""Stator<br>(10 units/min)""-->Motor
//        //    Motor--""Motor<br>(5 units/min)""-->Motor_Item
//        //    IronRod--""Iron Rod<br>(50 units/min)""-->Rotor
//        //    Screw--""Screw<br>(250 units/min)""-->Rotor
//        //    IronIngot--""Iron Ingot<br>(112.5 units/min)""-->IronRod
//        //    IronOre--""Iron Ore<br>(112.5 units/min)""-->IronIngot
//        //    IronRod--""Iron Rod<br>(62.5 units/min)""-->Screw
//        //    SteelPipe--""Steel Pipe<br>(30 units/min)""-->Stator
//        //    Wire--""Wire<br>(80 units/min)""-->Stator
//        //    SteelIngot--""Steel Ingot<br>(45 units/min)""-->SteelPipe
//        //    IronOre--""Iron Ore<br>(45 units/min)""-->SteelIngot
//        //    Coal--""Coal<br>(45 units/min)""-->SteelIngot
//        //    CopperIngot--""Copper Ingot<br>(40 units/min)""-->Wire
//        //    CopperOre--""Copper Ore<br>(40.0 units/min)""-->CopperIngot
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(210.71M, result.PowerConsumption);
//        //            Assert.AreEqual(13, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void PlasticProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Plastic";
//        //            decimal quantity = 20;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    Plastic[""x1 Refinery<br>(Plastic)""]
//        //    Plastic_Item([20 Plastic])
//        //    HeavyOilResidue_Item([10 Heavy Oil Residue])
//        //    CrudeOil[""x0.3 Oil Extractor<br>(Crude Oil)""]
//        //    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
//        //    Plastic--""Plastic<br>(20 units/min)""-->Plastic_Item
//        //    Plastic--""Heavy Oil Residue<br>(10 units/min)""-->HeavyOilResidue_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(40M, result.PowerConsumption);
//        //            Assert.AreEqual(3, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void CircuitBoardProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Circuit Board";
//        //            decimal quantity = 5;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    CircuitBoard[""x0.7 Assembler<br>(Circuit Board)""]
//        //    CircuitBoard_Item([5 Circuit Board])
//        //    CopperSheet[""x1 Constructor<br>(Copper Sheet)""]
//        //    CopperIngot[""x0.7 Smelter<br>(Copper Ingot)""]
//        //    CopperOre[""x0.4 Mining Machine Mk1<br>(Copper Ore)""]
//        //    Plastic[""x1 Refinery<br>(Plastic)""]
//        //    HeavyOilResidue_Item([10 Heavy Oil Residue])
//        //    CrudeOil[""x0.3 Oil Extractor<br>(Crude Oil)""]
//        //    CopperSheet--""Copper Sheet<br>(10 units/min)""-->CircuitBoard
//        //    Plastic--""Plastic<br>(20.1 units/min)""-->CircuitBoard
//        //    CircuitBoard--""Circuit Board<br>(5 units/min)""-->CircuitBoard_Item
//        //    CopperIngot--""Copper Ingot<br>(20 units/min)""-->CopperSheet
//        //    CopperOre--""Copper Ore<br>(20.1 units/min)""-->CopperIngot
//        //    CrudeOil--""Crude Oil<br>(30 units/min)""-->Plastic
//        //    Plastic--""Heavy Oil Residue<br>(10 units/min)""-->HeavyOilResidue_Item
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(58.33M, result.PowerConsumption);
//        //            Assert.AreEqual(7, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }

//        //        [TestMethod]
//        //        public void AluminumIngotProductionTest()
//        //        {
//        //            //Arrange
//        //            SatisfactoryProduction graph = new();
//        //            string itemName = "Aluminum Ingot";
//        //            decimal quantity = 10;
//        //            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
//        //            ProductionCalculation? result = null;
//        //            List<ProductionItem> results = new();
//        //            string mermaidResult = "";
//        //            string expectedResult = @"flowchart LR
//        //    AluminumIngot[""x0.2 Foundry<br>(Aluminum Ingot)""]
//        //    AluminumIngot_Item([10 Aluminum Ingot])
//        //    AluminumScrap[""x0.1 Assembler<br>(Aluminum Scrap)""]
//        //    Water[""x0.2 Water Extractor<br>(Water)""]
//        //    AluminaSolution[""x0.1 Refinery<br>(Alumina Solution)""]
//        //    Silica[""x0.5 Constructor<br>(Silica)""]
//        //    Bauxite[""x0.2 Mining Machine Mk1<br>(Bauxite)""]
//        //    Coal[""x0.1 Mining Machine Mk1<br>(Coal)""]
//        //    RawQuartz[""x0.2 Assembler<br>(Raw Quartz)""]
//        //    AluminumScrap--""Aluminum Scrap<br>(15.1 units/min)""-->AluminumIngot
//        //    Silica--""Silica<br>(12.6 units/min)""-->AluminumIngot
//        //    AluminumIngot--""Aluminum Ingot<br>(10 units/min)""-->AluminumIngot_Item
//        //    AluminaSolution--""Alumina Solution<br>(10.1 units/min)""-->AluminumScrap
//        //    Coal--""Coal<br>(5.1 units/min)""-->AluminumScrap
//        //    Bauxite--""Bauxite<br>(10.1 units/min)""-->AluminaSolution
//        //    Water--""Water<br>(15.1 units/min)""-->AluminaSolution
//        //    RawQuartz--""Raw Quartz<br>(30.1 units/min)""-->Silica
//        //";

//        //            //Act
//        //            if (itemGoal != null)
//        //            {
//        //                result = graph.BuildProductionPlan(itemGoal);
//        //                results = result.ProductionItems;
//        //                mermaidResult = graph.ToMermaidString();
//        //            }

//        //            //Assert
//        //            Assert.IsNotNull(itemGoal);
//        //            Assert.IsNotNull(result);
//        //            Assert.AreEqual(12.75M, result.PowerConsumption);
//        //            Assert.AreEqual(8, results.Count);
//        //            Assert.IsNotNull(mermaidResult);
//        //            Assert.AreEqual(expectedResult, mermaidResult);
//        //        }
//    }
//}
