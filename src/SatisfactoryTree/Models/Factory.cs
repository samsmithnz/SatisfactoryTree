namespace SatisfactoryTree.Models
{
    public class Factory
    {
        public Factory(string id, string name = "")
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? $"Factory {id}" : name;
            Storage = new Storage(id);
            ProductionGoals = new List<ProductionGoal>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Storage Storage { get; set; }
        public List<ProductionGoal> ProductionGoals { get; set; }

        public ProductionGoal AddProductionGoal(string itemName, decimal targetQuantity)
        {
            var goal = new ProductionGoal(itemName, targetQuantity, Id);
            ProductionGoals.Add(goal);
            return goal;
        }

        public void RemoveProductionGoal(string goalId)
        {
            ProductionGoals.RemoveAll(g => g.Id == goalId);
        }

        public void RemoveProductionGoal(ProductionGoal goal)
        {
            ProductionGoals.Remove(goal);
        }

        public List<ProductionGoal> GetActiveGoals()
        {
            return ProductionGoals.Where(g => !g.IsCompleted).ToList();
        }

        public List<ProductionGoal> GetCompletedGoals()
        {
            return ProductionGoals.Where(g => g.IsCompleted).ToList();
        }

        public void ProcessProduction(string itemName, decimal quantity)
        {
            // Update active goals for this item
            var activeGoals = GetActiveGoals().Where(g => g.ItemName == itemName).ToList();
            decimal remainingQuantity = quantity;

            foreach (var goal in activeGoals)
            {
                if (remainingQuantity <= 0) break;

                var neededQuantity = goal.GetRemainingQuantity();
                var quantityToAllocate = Math.Min(neededQuantity, remainingQuantity);

                goal.UpdateProgress(quantityToAllocate);
                remainingQuantity -= quantityToAllocate;
            }

            // Send remaining quantity to storage
            if (remainingQuantity > 0)
            {
                Storage.AddItem(itemName, remainingQuantity);
            }
        }
    }
}