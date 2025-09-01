using SatisfactoryTree.Models;

namespace SatisfactoryTree.Services
{
    public class ProductionPlanningService
    {
        public ProductionPlanningService()
        {
            Factories = new Dictionary<string, Factory>();
            // Create default factory
            AddFactory("default", "Main Factory");
        }

        public Dictionary<string, Factory> Factories { get; set; }

        public Factory AddFactory(string id, string name = "")
        {
            if (Factories.ContainsKey(id))
            {
                throw new ArgumentException($"Factory with ID '{id}' already exists.");
            }

            var factory = new Factory(id, name);
            Factories[id] = factory;
            return factory;
        }

        public void RemoveFactory(string id)
        {
            if (id == "default")
            {
                throw new ArgumentException("Cannot remove the default factory.");
            }
            Factories.Remove(id);
        }

        public Factory? GetFactory(string id)
        {
            return Factories.ContainsKey(id) ? Factories[id] : null;
        }

        public List<Factory> GetAllFactories()
        {
            return Factories.Values.ToList();
        }

        public ProductionGoal AddProductionGoal(string itemName, decimal targetQuantity, string factoryId = "default")
        {
            var factory = GetFactory(factoryId);
            if (factory == null)
            {
                throw new ArgumentException($"Factory with ID '{factoryId}' not found.");
            }

            return factory.AddProductionGoal(itemName, targetQuantity);
        }

        public void ProcessProduction(string itemName, decimal quantity, string factoryId = "default")
        {
            var factory = GetFactory(factoryId);
            if (factory == null)
            {
                throw new ArgumentException($"Factory with ID '{factoryId}' not found.");
            }

            factory.ProcessProduction(itemName, quantity);
        }

        public List<ProductionGoal> GetAllActiveGoals()
        {
            return Factories.Values.SelectMany(f => f.GetActiveGoals()).ToList();
        }

        public List<ProductionGoal> GetAllCompletedGoals()
        {
            return Factories.Values.SelectMany(f => f.GetCompletedGoals()).ToList();
        }

        public List<ProductionGoal> GetTopLevelGoals(string factoryId = "default")
        {
            var factory = GetFactory(factoryId);
            if (factory == null) return new List<ProductionGoal>();

            return factory.GetActiveGoals().Where(g => string.IsNullOrEmpty(g.ParentGoalId)).ToList();
        }

        public List<ProductionGoal> GetAllGoalsIncludingDependencies(string factoryId = "default")
        {
            var factory = GetFactory(factoryId);
            if (factory == null) return new List<ProductionGoal>();

            var allGoals = new List<ProductionGoal>();
            var topLevelGoals = factory.GetActiveGoals().Where(g => string.IsNullOrEmpty(g.ParentGoalId));

            foreach (var goal in topLevelGoals)
            {
                allGoals.Add(goal);
                allGoals.AddRange(goal.GetAllDependencies());
            }

            return allGoals;
        }

        public void ToggleGoalProductionMethod(string goalId, bool produceInternally)
        {
            var goal = GetAllActiveGoals().FirstOrDefault(g => g.Id == goalId);
            if (goal != null)
            {
                goal.ProduceInternally = produceInternally;
                
                if (!produceInternally)
                {
                    // Clear dependencies if switching to import
                    goal.DependentGoals.Clear();
                }
            }
        }

        public void RemoveProductionGoal(string goalId, string factoryId = "default")
        {
            var factory = GetFactory(factoryId);
            if (factory == null) return;

            var goal = factory.GetActiveGoals().FirstOrDefault(g => g.Id == goalId);
            if (goal != null)
            {
                // Remove from parent if this is a dependent goal
                if (!string.IsNullOrEmpty(goal.ParentGoalId))
                {
                    var parentGoal = factory.GetActiveGoals().FirstOrDefault(g => g.Id == goal.ParentGoalId);
                    parentGoal?.RemoveDependentGoal(goalId);
                }

                // Remove from factory
                factory.RemoveProductionGoal(goal);
            }
        }

        public void TransferItemsBetweenFactories(string fromFactoryId, string toFactoryId, string itemName, decimal quantity)
        {
            var fromFactory = GetFactory(fromFactoryId);
            var toFactory = GetFactory(toFactoryId);

            if (fromFactory == null || toFactory == null)
            {
                throw new ArgumentException("One or both factories not found.");
            }

            if (fromFactory.Storage.RemoveItem(itemName, quantity))
            {
                toFactory.Storage.AddItem(itemName, quantity);
            }
            else
            {
                throw new InvalidOperationException($"Insufficient {itemName} in factory {fromFactoryId} storage.");
            }
        }

        public Dictionary<string, decimal> GetTotalStorage()
        {
            var totalStorage = new Dictionary<string, decimal>();

            foreach (var factory in Factories.Values)
            {
                foreach (var item in factory.Storage.GetAllItems())
                {
                    if (totalStorage.ContainsKey(item.Key))
                    {
                        totalStorage[item.Key] += item.Value;
                    }
                    else
                    {
                        totalStorage[item.Key] = item.Value;
                    }
                }
            }

            return totalStorage;
        }
    }
}