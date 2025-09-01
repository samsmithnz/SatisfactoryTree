using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree;
using SatisfactoryTree.Models;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class SatisfactoryDependenciesTests
    {
        [TestMethod]
        public void SatisfactoryDependencies_Constructor_InitializesItems()
        {
            // Act
            SatisfactoryDependencies dependencies = new();

            // Assert
            Assert.IsNotNull(dependencies.Items);
            Assert.IsTrue(dependencies.Items.Count > 0);
            Assert.IsTrue(dependencies.Items.Any(i => i.Name == "Iron Ore"));
        }

        [TestMethod]
        public void SatisfactoryDependencies_Constructor_InitializesItemGroups()
        {
            // Act
            SatisfactoryDependencies dependencies = new();

            // Assert
            Assert.IsNotNull(dependencies.ItemGroups);
            // ItemGroups is initialized as empty list for now
        }

        [TestMethod]
        public void SatisfactoryDependencies_Items_Property_CanBeSetAndRetrieved()
        {
            // Arrange
            SatisfactoryDependencies dependencies = new();
            List<Item> testItems = new() { new Item(0, "Test Item", "test.png") };

            // Act
            dependencies.Items = testItems;

            // Assert
            Assert.AreEqual(testItems, dependencies.Items);
            Assert.AreEqual(1, dependencies.Items.Count);
            Assert.AreEqual("Test Item", dependencies.Items[0].Name);
        }

        [TestMethod]
        public void SatisfactoryDependencies_ItemGroups_Property_CanBeSetAndRetrieved()
        {
            // Arrange
            SatisfactoryDependencies dependencies = new();
            List<ItemGroup> testGroups = new();

            // Act
            dependencies.ItemGroups = testGroups;

            // Assert
            Assert.AreEqual(testGroups, dependencies.ItemGroups);
        }

        [TestMethod]
        public void SatisfactoryDependencies_BuildDependencyPlan_ReturnsFlowchart()
        {
            // Arrange
            SatisfactoryDependencies dependencies = new();

            // Act
            var flowchart = dependencies.BuildDependencyPlan();

            // Assert
            Assert.IsNotNull(flowchart);
            // The method should return a flowchart even with minimal data
        }
    }
}