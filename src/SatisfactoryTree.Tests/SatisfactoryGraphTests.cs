using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree;
using SatisfactoryTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class SatisfactoryGraphTests
    {
        [TestMethod]
        public void SatisfactoryGraph_Constructor_WithEmptyFilter_LoadsAllItems()
        {
            // Act
            SatisfactoryGraph graph = new();
            
            // Assert
            Assert.IsNotNull(graph.Items);
            Assert.IsTrue(graph.Items.Count > 0);
            Assert.IsTrue(graph.Items.Any(i => i.Name == "Iron Ore"));
            Assert.IsTrue(graph.Items.Any(i => i.Name == "Iron Ingot"));
        }

        [TestMethod]
        public void SatisfactoryGraph_Constructor_WithValidFilter_FiltersToSpecificItem()
        {
            // Act
            SatisfactoryGraph graph = new("Iron Ingot", ResearchType.Tier2);
            
            // Assert
            Assert.IsNotNull(graph.Items);
            Assert.IsTrue(graph.Items.Count > 0);
            Assert.IsTrue(graph.Items.Any(i => i.Name == "Iron Ingot"));
            // Should have the target item plus its dependencies
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SatisfactoryGraph_Constructor_WithInvalidFilter_ThrowsException()
        {
            // Act
            SatisfactoryGraph graph = new("NonExistentItem", ResearchType.Tier2);
            
            // Should throw exception because item not found
        }

        [TestMethod]
        public void SatisfactoryGraph_Constructor_WithResearchTypeFilter_FiltersCorrectly()
        {
            // Act
            SatisfactoryGraph graph = new("", ResearchType.Tier1);
            
            // Assert
            Assert.IsNotNull(graph.Items);
            // All items should be Tier1 or lower
            foreach (Item item in graph.Items)
            {
                Assert.IsTrue(item.ResearchType <= ResearchType.Tier1);
            }
        }

        [TestMethod]
        public void SatisfactoryGraph_Items_PropertyExists()
        {
            // Arrange & Act
            Type graphType = typeof(SatisfactoryGraph);
            var itemsProperty = graphType.GetProperty("Items");
            
            // Assert
            Assert.IsNotNull(itemsProperty);
            Assert.AreEqual(typeof(List<Item>), itemsProperty.PropertyType);
        }

        [TestMethod]
        public void SatisfactoryGraph_WithIronOre_ContainsExpectedProperties()
        {
            // Act
            SatisfactoryGraph graph = new();
            Item? ironOre = graph.Items.FirstOrDefault(i => i.Name == "Iron Ore");
            
            // Assert
            Assert.IsNotNull(ironOre);
            Assert.AreEqual("Iron Ore", ironOre.Name);
            Assert.AreEqual("IronOre_256.png", ironOre.Image);
            Assert.AreEqual(0, ironOre.Level);
            Assert.AreEqual(ItemType.Production, ironOre.ItemType);
            Assert.AreEqual(ResearchType.Tier1, ironOre.ResearchType);
        }
    }
}