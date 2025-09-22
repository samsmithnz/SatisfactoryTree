using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services
{
    public class PlanService
    {
        private Plan? _plan;
        
        public event Action? PlanChanged;

        public Plan? Plan 
        { 
            get => _plan; 
            set 
            { 
                _plan = value; 
                PlanChanged?.Invoke(); 
            } 
        }

        public bool HasPlan => _plan != null && _plan.Factories.Any();

        public void AddFactory()
        {
            if (_plan == null)
            {
                _plan = new Plan();
            }

            // Find the next available ID
            int nextId = _plan.Factories.Any() ? _plan.Factories.Max(f => f.Id) + 1 : 1;
            
            // Create a new factory with the default name pattern
            string factoryName = $"Factory {nextId}";
            var newFactory = new Factory(nextId, factoryName);
            
            _plan.Factories.Add(newFactory);
            PlanChanged?.Invoke();
        }
    }
}