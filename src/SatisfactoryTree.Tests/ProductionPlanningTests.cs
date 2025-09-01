using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using System;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionPlanningTests
    {
        [TestMethod]
        public void ProductionGoal_InitializesCorrectly()
        {
            // Arrange
            string itemName = "Iron Plate";
            decimal targetQuantity = 100;
            string factoryId = "test-factory";

            // Act
            var goal = new ProductionGoal(itemName, targetQuantity, factoryId);

            // Assert
            Assert.AreEqual(itemName, goal.ItemName);
            Assert.AreEqual(targetQuantity, goal.TargetQuantity);
            Assert.AreEqual(0, goal.CurrentQuantity);
            Assert.AreEqual(factoryId, goal.FactoryId);
            Assert.IsFalse(goal.IsCompleted);
            Assert.AreEqual(0, goal.ProgressPercentage);
            Assert.AreEqual(100, goal.GetRemainingQuantity());
        }

        [TestMethod]
        public void ProductionGoal_UpdateProgress_WorksCorrectly()
        {
            // Arrange
            var goal = new ProductionGoal("Iron Plate", 100);

            // Act
            goal.UpdateProgress(30);

            // Assert
            Assert.AreEqual(30, goal.CurrentQuantity);
            Assert.AreEqual(30, goal.ProgressPercentage);
            Assert.AreEqual(70, goal.GetRemainingQuantity());
            Assert.IsFalse(goal.IsCompleted);
        }

        [TestMethod]
        public void ProductionGoal_CompletesWhenTargetReached()
        {
            // Arrange
            var goal = new ProductionGoal("Iron Plate", 100);

            // Act
            goal.UpdateProgress(100);

            // Assert
            Assert.AreEqual(100, goal.CurrentQuantity);
            Assert.AreEqual(100, goal.ProgressPercentage);
            Assert.AreEqual(0, goal.GetRemainingQuantity());
            Assert.IsTrue(goal.IsCompleted);
            Assert.IsNotNull(goal.CompletedDate);
        }

        [TestMethod]
        public void ProductionGoal_DoesNotExceedTarget()
        {
            // Arrange
            var goal = new ProductionGoal("Iron Plate", 100);

            // Act
            goal.UpdateProgress(150);

            // Assert
            Assert.AreEqual(100, goal.CurrentQuantity);
            Assert.AreEqual(100, goal.ProgressPercentage);
            Assert.AreEqual(0, goal.GetRemainingQuantity());
            Assert.IsTrue(goal.IsCompleted);
        }

        [TestMethod]
        public void Storage_AddAndRemoveItems_WorksCorrectly()
        {
            // Arrange
            var storage = new Storage("test-factory");

            // Act & Assert
            storage.AddItem("Iron Ore", 100);
            Assert.AreEqual(100, storage.GetItemQuantity("Iron Ore"));

            storage.AddItem("Iron Ore", 50);
            Assert.AreEqual(150, storage.GetItemQuantity("Iron Ore"));

            Assert.IsTrue(storage.RemoveItem("Iron Ore", 75));
            Assert.AreEqual(75, storage.GetItemQuantity("Iron Ore"));

            Assert.IsFalse(storage.RemoveItem("Iron Ore", 100));
            Assert.AreEqual(75, storage.GetItemQuantity("Iron Ore"));
        }

        [TestMethod]
        public void Factory_ProcessProduction_UpdatesGoalsAndStorage()
        {
            // Arrange
            var factory = new Factory("test-factory");
            var goal1 = factory.AddProductionGoal("Iron Plate", 50);
            var goal2 = factory.AddProductionGoal("Iron Plate", 30);

            // Act
            factory.ProcessProduction("Iron Plate", 100);

            // Assert
            // First goal should be completed (50)
            Assert.IsTrue(goal1.IsCompleted);
            Assert.AreEqual(50, goal1.CurrentQuantity);

            // Second goal should be completed (30)
            Assert.IsTrue(goal2.IsCompleted);
            Assert.AreEqual(30, goal2.CurrentQuantity);

            // Remaining 20 should go to storage
            Assert.AreEqual(20, factory.Storage.GetItemQuantity("Iron Plate"));
        }

        [TestMethod]
        public void Factory_ProcessProduction_PartialGoalCompletion()
        {
            // Arrange
            var factory = new Factory("test-factory");
            var goal = factory.AddProductionGoal("Iron Plate", 100);

            // Act
            factory.ProcessProduction("Iron Plate", 60);

            // Assert
            Assert.IsFalse(goal.IsCompleted);
            Assert.AreEqual(60, goal.CurrentQuantity);
            Assert.AreEqual(0, factory.Storage.GetItemQuantity("Iron Plate"));
        }

        [TestMethod]
        public void ProductionPlanningService_InitializesWithDefaultFactory()
        {
            // Arrange & Act
            var service = new ProductionPlanningService();

            // Assert
            Assert.AreEqual(1, service.Factories.Count);
            Assert.IsNotNull(service.GetFactory("default"));
            Assert.AreEqual("Main Factory", service.GetFactory("default")!.Name);
        }

        [TestMethod]
        public void ProductionPlanningService_ManageMultipleFactories()
        {
            // Arrange
            var service = new ProductionPlanningService();

            // Act
            var factory1 = service.AddFactory("factory1", "Production Line 1");
            var factory2 = service.AddFactory("factory2", "Production Line 2");

            // Assert
            Assert.AreEqual(3, service.Factories.Count); // including default
            Assert.IsNotNull(service.GetFactory("factory1"));
            Assert.IsNotNull(service.GetFactory("factory2"));
            Assert.AreEqual("Production Line 1", factory1.Name);
            Assert.AreEqual("Production Line 2", factory2.Name);
        }

        [TestMethod]
        public void ProductionPlanningService_TransferItemsBetweenFactories()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var factory1 = service.AddFactory("factory1");
            var factory2 = service.AddFactory("factory2");

            factory1.Storage.AddItem("Iron Ore", 100);

            // Act
            service.TransferItemsBetweenFactories("factory1", "factory2", "Iron Ore", 60);

            // Assert
            Assert.AreEqual(40, factory1.Storage.GetItemQuantity("Iron Ore"));
            Assert.AreEqual(60, factory2.Storage.GetItemQuantity("Iron Ore"));
        }

        [TestMethod]
        public void ProductionPlanningService_GetTotalStorage()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var factory1 = service.AddFactory("factory1");
            var factory2 = service.AddFactory("factory2");

            service.GetFactory("default")!.Storage.AddItem("Iron Ore", 50);
            factory1.Storage.AddItem("Iron Ore", 30);
            factory1.Storage.AddItem("Copper Ore", 40);
            factory2.Storage.AddItem("Iron Ore", 20);

            // Act
            var totalStorage = service.GetTotalStorage();

            // Assert
            Assert.AreEqual(100, totalStorage["Iron Ore"]); // 50 + 30 + 20
            Assert.AreEqual(40, totalStorage["Copper Ore"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProductionPlanningService_CannotRemoveDefaultFactory()
        {
            // Arrange
            var service = new ProductionPlanningService();

            // Act
            service.RemoveFactory("default");
        }

        [TestMethod]
        public void ProductionPlanningService_EndToEndScenario()
        {
            // Arrange
            var service = new ProductionPlanningService();
            
            // Act - Create production goals
            var goal1 = service.AddProductionGoal("Iron Plate", 100);
            var goal2 = service.AddProductionGoal("Copper Wire", 200);
            
            // Process some production
            service.ProcessProduction("Iron Plate", 60);
            service.ProcessProduction("Copper Wire", 200);
            service.ProcessProduction("Iron Plate", 50); // This should complete goal1 and add 10 to storage

            // Assert
            Assert.IsTrue(goal1.IsCompleted);
            Assert.AreEqual(100, goal1.CurrentQuantity);
            
            Assert.IsTrue(goal2.IsCompleted);
            Assert.AreEqual(200, goal2.CurrentQuantity);

            var defaultFactory = service.GetFactory("default")!;
            Assert.AreEqual(10, defaultFactory.Storage.GetItemQuantity("Iron Plate"));

            var activeGoals = service.GetAllActiveGoals();
            var completedGoals = service.GetAllCompletedGoals();
            
            Assert.AreEqual(0, activeGoals.Count);
            Assert.AreEqual(2, completedGoals.Count);
        }
    }
}