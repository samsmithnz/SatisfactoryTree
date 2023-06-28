using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class PowerTests
    {
        [TestMethod]
        public void SolidBioFuelPowerGenerationTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Solid Biofuel Power";
            decimal quantity = 60;
            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
            ProductionCalculation result;
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    SolidBiofuelPower[""x2 Biomass Burner<br>(Solid Biofuel Power)""]
    SolidBiofuelPower_Item([60 Solid Biofuel Power])
    SolidBiofuel[""x0.2 Constructor<br>(Solid Biofuel)""]
    Biomass[""x0.3 Constructor<br>(Biomass)""]
    Leaves[""x0.6 <br>(Leaves)""]
    SolidBiofuel--""Solid Biofuel<br>(8 units/min)""-->SolidBiofuelPower
    SolidBiofuelPower--""Solid Biofuel Power<br>(60 units/min)""-->SolidBiofuelPower_Item
    Biomass--""Biomass<br>(16.0 units/min)""-->SolidBiofuel
    Leaves--""Leaves<br>(32.0 units/min)""-->Biomass
";

            //Act
            if (itemGoal != null)
            {
                result = graph.BuildProductionPlan(itemGoal);
                results = result.ProductionItems;
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(itemGoal);
            Assert.AreEqual(4, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(60, results[0].Quantity);
            Assert.AreEqual(2, results[0].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }

        [TestMethod]
        public void CoalPowerGenerationTest()
        {
            //Arrange
            SatisfactoryProduction graph = new();
            string itemName = "Coal Power";
            decimal quantity = 150;
            ProductionItem? itemGoal = new(graph.FindItem(itemName), quantity);
            ProductionCalculation result = null;
            List<ProductionItem> results = new();
            string mermaidResult = "";
            string expectedResult = @"flowchart LR
    CoalPower[""x2 Coal Generator<br>(Coal Power)""]
    CoalPower_Item([150 Coal Power])
    Coal[""x0.5 Mining Machine Mk1<br>(Coal)""]
    Water[""x0.8 Water Extractor<br>(Water)""]
    Coal--""Coal<br>(30 units/min)""-->CoalPower
    Water--""Water<br>(90 units/min)""-->CoalPower
    CoalPower--""Coal Power<br>(150 units/min)""-->CoalPower_Item
";

            //Act
            if (itemGoal != null)
            {
                result = graph.BuildProductionPlan(itemGoal);
                results = result.ProductionItems;
                mermaidResult = graph.ToMermaidString();
            }

            //Assert
            Assert.IsNotNull(itemGoal);
            Assert.IsNotNull(result);
            Assert.AreEqual(17.5M, result.PowerConsumption);
            Assert.AreEqual(3, results.Count);
            Assert.IsNotNull(results[0].Item);
            Assert.AreEqual(150, results[0].Quantity);
            Assert.AreEqual(2, results[0].BuildingQuantityRequired);
            Assert.IsNotNull(mermaidResult);
            Assert.AreEqual(expectedResult, mermaidResult);
        }

    }
}
