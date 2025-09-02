using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Models;
using SatisfactoryTree.Services;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class ProductionRowControlTests
    {
        [TestMethod]
        public void ProductionRowControl_ProductionGoalBinding_UpdatesCorrectly()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);
            goal.ProduceInternally = true;
            goal.CurrentQuantity = 25; // 25% progress

            // Act - This would normally be done with a UI control, but we can verify the data model
            var progressPercentage = goal.ProgressPercentage;

            // Assert
            Assert.AreEqual(25, progressPercentage);
            Assert.AreEqual("Iron Plate", goal.ItemName);
            Assert.AreEqual(100, goal.TargetQuantity);
            Assert.IsTrue(goal.ProduceInternally);
        }

        [TestMethod]
        public void ProductionRowControl_RecipeClassNameBinding_StoresCorrectly()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);

            // Act
            goal.RecipeClassName = "Recipe_IronPlate_C";

            // Assert
            Assert.AreEqual("Recipe_IronPlate_C", goal.RecipeClassName);
        }

        [TestMethod]
        public void ProductionRowControl_QuantityUpdate_CalculatesCorrectly()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);

            // Act
            goal.TargetQuantity = 250;

            // Assert
            Assert.AreEqual(250, goal.TargetQuantity);
        }

        [TestMethod]
        public void ProductionRowControl_ToggleProductionMethod_UpdatesStateCorrectly()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 100);
            goal.ProduceInternally = true;

            // Act
            service.ToggleGoalProductionMethod(goal.Id, false);

            // Assert
            Assert.IsFalse(goal.ProduceInternally);
        }

        [TestMethod]
        public void ProductionRowControl_ProgressCalculation_HandlesEdgeCases()
        {
            // Arrange
            var service = new ProductionPlanningService();
            var goal = service.AddProductionGoal("Iron Plate", 0); // Zero target

            // Act
            goal.CurrentQuantity = 10;
            var progressPercentage = goal.ProgressPercentage;

            // Assert
            Assert.AreEqual(0, progressPercentage); // Should handle division by zero
        }
    }
}