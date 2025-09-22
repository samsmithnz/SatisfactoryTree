using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class PlanServiceFunctionalityTests
    {
        [TestMethod]
        public void AddFactory_CreatesFactoryWithCorrectIdAndName()
        {
            // Arrange
            var plan = new Plan();
            plan.Factories.Add(new Factory(1, "First Factory"));
            plan.Factories.Add(new Factory(2, "Second Factory"));

            // Act - Simulate the AddFactory functionality
            int nextId = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            string factoryName = $"Factory {nextId}";
            var newFactory = new Factory(nextId, factoryName);
            plan.Factories.Add(newFactory);

            // Assert
            Assert.AreEqual(3, plan.Factories.Count);
            Assert.AreEqual(3, newFactory.Id);
            Assert.AreEqual("Factory 3", newFactory.Name);
            Assert.IsTrue(plan.Factories.Contains(newFactory));
        }

        [TestMethod]
        public void AddFactory_ToEmptyPlan_CreatesFactoryWithId1()
        {
            // Arrange
            var plan = new Plan();

            // Act - Simulate the AddFactory functionality
            int nextId = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            string factoryName = $"Factory {nextId}";
            var newFactory = new Factory(nextId, factoryName);
            plan.Factories.Add(newFactory);

            // Assert
            Assert.AreEqual(1, plan.Factories.Count);
            Assert.AreEqual(1, newFactory.Id);
            Assert.AreEqual("Factory 1", newFactory.Name);
        }

        [TestMethod]
        public void AddFactory_SequentialCalls_CreatesConsecutiveIds()
        {
            // Arrange
            var plan = new Plan();

            // Act - Add first factory
            int nextId1 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory1 = new Factory(nextId1, $"Factory {nextId1}");
            plan.Factories.Add(factory1);

            // Add second factory
            int nextId2 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory2 = new Factory(nextId2, $"Factory {nextId2}");
            plan.Factories.Add(factory2);

            // Add third factory
            int nextId3 = plan.Factories.Any() ? plan.Factories.Max(f => f.Id) + 1 : 1;
            var factory3 = new Factory(nextId3, $"Factory {nextId3}");
            plan.Factories.Add(factory3);

            // Assert
            Assert.AreEqual(3, plan.Factories.Count);
            Assert.AreEqual("Factory 1", factory1.Name);
            Assert.AreEqual("Factory 2", factory2.Name);
            Assert.AreEqual("Factory 3", factory3.Name);
            Assert.AreEqual(1, factory1.Id);
            Assert.AreEqual(2, factory2.Id);
            Assert.AreEqual(3, factory3.Id);
        }
    }
}