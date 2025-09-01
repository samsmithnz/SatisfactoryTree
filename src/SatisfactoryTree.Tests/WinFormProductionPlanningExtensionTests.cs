using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Models;
using SatisfactoryTree.Services;
using System.Linq;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class WinFormProductionPlanningExtensionTests
    {
        [TestMethod]
        public void ProductionGoal_WithRecipeClassName_StoresCorrectly()
        {
            // Arrange
            var service = new ProductionPlanningService();

            // Act
            var goal = service.AddProductionGoal("Iron Plate", 100);
            goal.RecipeClassName = "Recipe_IronPlate_C";

            // Assert
            Assert.AreEqual("Recipe_IronPlate_C", goal.RecipeClassName);
        }

        [TestMethod]
        public void ProductionGoal_ProduceInternallyFlag_CanBeToggled()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);

            // Act
            goal.ProduceInternally = false;

            // Assert
            Assert.IsFalse(goal.ProduceInternally);
        }

        [TestMethod]
        public void ProductionGoal_DependentGoals_CanBeManaged()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var mainGoal = service.AddProductionGoal("Iron Plate", 100);
            var dependency = new ProductionGoal("Iron Ore", 150);

            // Act
            mainGoal.AddDependentGoal(dependency);

            // Assert
            Assert.AreEqual(1, mainGoal.DependentGoals.Count);
            Assert.AreEqual("Iron Ore", mainGoal.DependentGoals[0].ItemName);
            Assert.AreEqual(mainGoal.Id, dependency.ParentGoalId);
        }

        [TestMethod]
        public void ProductionGoal_GetAllDependencies_WorksRecursively()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var mainGoal = service.AddProductionGoal("Computer", 10);
            var dependency1 = new ProductionGoal("Circuit Board", 20);
            var dependency2 = new ProductionGoal("Copper Wire", 40);
            
            // Act - Create a chain: Computer -> Circuit Board -> Copper Wire
            mainGoal.AddDependentGoal(dependency1);
            dependency1.AddDependentGoal(dependency2);

            var allDeps = mainGoal.GetAllDependencies();

            // Assert
            Assert.AreEqual(2, allDeps.Count);
            Assert.IsTrue(allDeps.Any(d => d.ItemName == "Circuit Board"));
            Assert.IsTrue(allDeps.Any(d => d.ItemName == "Copper Wire"));
        }

        [TestMethod]
        public void ProductionPlanningService_GetAllGoalsIncludingDependencies_ReturnsAllLevels()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var factory = service.GetFactory("default")!;
            
            var mainGoal = factory.AddProductionGoal("Computer", 10);
            var dependency = new ProductionGoal("Circuit Board", 20);
            mainGoal.AddDependentGoal(dependency);

            // Act
            var allGoals = service.GetAllGoalsIncludingDependencies("default");

            // Assert
            Assert.AreEqual(2, allGoals.Count);
            Assert.IsTrue(allGoals.Any(g => g.ItemName == "Computer"));
            Assert.IsTrue(allGoals.Any(g => g.ItemName == "Circuit Board"));
        }

        [TestMethod] 
        public void ProductionPlanningService_ToggleGoalProductionMethod_ClearsDependencies()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);
            var dependency = new ProductionGoal("Iron Ore", 150);
            goal.AddDependentGoal(dependency);

            // Act
            service.ToggleGoalProductionMethod(goal.Id, false); // Switch to import

            // Assert
            Assert.IsFalse(goal.ProduceInternally);
            Assert.AreEqual(0, goal.DependentGoals.Count);
        }
    }
}