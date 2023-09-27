using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class DependencyGraphTests
    {
        [TestMethod]
        public void DependencyTest()
        {
            //Arrange
            SatisfactoryDependencies satisfactoryDependencies = new();

            string expectedResult = @"flowchart LR
    IronOre[""x1.5 Mining Machine Mk1<br>(Iron Ore)""]
    IronOre_Item([90 Iron Ore])
    IronOre--""Iron Ore<br>(90 units/min)""-->IronOre_Item
";

            //Act
            MermaidDotNet.Flowchart flowchart = satisfactoryDependencies.BuildDependencyPlan();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(flowchart.SubGraphs);
            Assert.AreEqual(9, flowchart.SubGraphs.Count);
            string mermaidResult = flowchart.CalculateFlowchart();
            //Assert.IsNotNull(result);
            //Assert.AreEqual(7.5M, result.PowerConsumption);
            //Assert.AreEqual(1, results.Count);
            //Assert.IsNotNull(results[0].Item);
            //Assert.AreEqual(90, results[0].Quantity);
            //Assert.AreEqual(1.5M, results[0].BuildingQuantityRequired);
            //Assert.IsNotNull(mermaidResult);
            //Assert.AreEqual(expectedResult, mermaidResult);
        }


    }
}
