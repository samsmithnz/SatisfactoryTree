using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using System;
using System.Linq;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionDependencyTests
    {
        [TestMethod]
        public void ProductionGoal_AddDependentGoal_WorksCorrectly()
        {
            // Arrange
            var parentGoal = new ProductionGoal("Iron Plate", 100);
            var dependentGoal = new ProductionGoal("Iron Ore", 150);

            // Act
            parentGoal.AddDependentGoal(dependentGoal);

            // Assert
            Assert.AreEqual(1, parentGoal.DependentGoals.Count);
            Assert.AreEqual("Iron Ore", parentGoal.DependentGoals[0].ItemName);
            Assert.AreEqual(parentGoal.Id, dependentGoal.ParentGoalId);
        }

        [TestMethod]
        public void ProductionGoal_RemoveDependentGoal_WorksCorrectly()
        {
            // Arrange
            var parentGoal = new ProductionGoal("Iron Plate", 100);
            var dependentGoal = new ProductionGoal("Iron Ore", 150);
            parentGoal.AddDependentGoal(dependentGoal);

            // Act
            parentGoal.RemoveDependentGoal(dependentGoal.Id);

            // Assert
            Assert.AreEqual(0, parentGoal.DependentGoals.Count);
        }

        [TestMethod]
        public void ProductionGoal_GetAllDependencies_ReturnsRecursiveDependencies()
        {
            // Arrange
            var goal1 = new ProductionGoal("Computer", 10);
            var goal2 = new ProductionGoal("Circuit Board", 20);
            var goal3 = new ProductionGoal("Copper Wire", 30);
            var goal4 = new ProductionGoal("Plastic", 15);

            goal1.AddDependentGoal(goal2);
            goal1.AddDependentGoal(goal4);
            goal2.AddDependentGoal(goal3);

            // Act
            var allDependencies = goal1.GetAllDependencies();

            // Assert
            Assert.AreEqual(3, allDependencies.Count);
            Assert.IsTrue(allDependencies.Any(g => g.ItemName == "Circuit Board"));
            Assert.IsTrue(allDependencies.Any(g => g.ItemName == "Copper Wire"));
            Assert.IsTrue(allDependencies.Any(g => g.ItemName == "Plastic"));
        }

        [TestMethod]
        public void ProductionPlanningService_GetTopLevelGoals_ReturnsOnlyParentGoals()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var factory = service.GetFactory("default")!;

            var goal1 = factory.AddProductionGoal("Computer", 10);
            var goal2 = new ProductionGoal("Circuit Board", 20);
            var goal3 = factory.AddProductionGoal("Steel Beam", 30);

            goal1.AddDependentGoal(goal2);

            // Act
            var topLevelGoals = service.GetTopLevelGoals();

            // Assert
            Assert.AreEqual(2, topLevelGoals.Count);
            Assert.IsTrue(topLevelGoals.Any(g => g.ItemName == "Computer"));
            Assert.IsTrue(topLevelGoals.Any(g => g.ItemName == "Steel Beam"));
            Assert.IsFalse(topLevelGoals.Any(g => g.ItemName == "Circuit Board"));
        }

        [TestMethod]
        public void ProductionPlanningService_GetAllGoalsIncludingDependencies_ReturnsAllGoals()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var factory = service.GetFactory("default")!;

            var goal1 = factory.AddProductionGoal("Computer", 10);
            var goal2 = new ProductionGoal("Circuit Board", 20);
            var goal3 = factory.AddProductionGoal("Steel Beam", 30);

            goal1.AddDependentGoal(goal2);

            // Act
            var allGoals = service.GetAllGoalsIncludingDependencies();

            // Assert
            Assert.AreEqual(3, allGoals.Count);
            Assert.IsTrue(allGoals.Any(g => g.ItemName == "Computer"));
            Assert.IsTrue(allGoals.Any(g => g.ItemName == "Circuit Board"));
            Assert.IsTrue(allGoals.Any(g => g.ItemName == "Steel Beam"));
        }

        [TestMethod]
        public void ProductionGoal_ProduceInternallyFlag_DefaultsToTrue()
        {
            // Arrange & Act
            var goal = new ProductionGoal("Iron Plate", 100);

            // Assert
            Assert.IsTrue(goal.ProduceInternally);
        }

        [TestMethod]
        public void ProductionGoal_RecipeClassName_CanBeSetAndRetrieved()
        {
            // Arrange
            var goal = new ProductionGoal("Iron Plate", 100);

            // Act
            goal.RecipeClassName = "Recipe_IronPlate_C";

            // Assert
            Assert.AreEqual("Recipe_IronPlate_C", goal.RecipeClassName);
        }

        [TestMethod]
        public void ProductionPlanningService_ToggleGoalProductionMethod_UpdatesProduceInternally()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);

            // Act
            service.ToggleGoalProductionMethod(goal.Id, false);

            // Assert
            Assert.IsFalse(goal.ProduceInternally);
        }

        [TestMethod]
        public void ProductionPlanningService_ToggleGoalProductionMethod_ClearsDependenciesWhenImporting()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);
            var dependentGoal = new ProductionGoal("Iron Ore", 150);
            goal.AddDependentGoal(dependentGoal);

            // Act
            service.ToggleGoalProductionMethod(goal.Id, false);

            // Assert
            Assert.AreEqual(0, goal.DependentGoals.Count);
        }
    }
}