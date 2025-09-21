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
    }
}