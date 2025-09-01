using SatisfactoryTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SatisfactoryTree.Tests;

[TestClass]
public class ResearchTests
{
    [TestMethod]
    public void OnBoardingTest()
    {
        //Arrange
        SatisfactoryGraph graph = new("", ResearchType.Tier2);

        //Act

        //Assert
        Assert.IsNotNull(graph);
        Assert.IsTrue(graph.Items.Count > 0);
    }
}