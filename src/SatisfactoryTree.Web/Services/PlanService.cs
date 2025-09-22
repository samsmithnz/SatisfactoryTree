using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services
{
    public class PlanService
    {
        private Plan? _plan;
        private FactoryCatalog? _factoryCatalog;
        
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

        public FactoryCatalog? FactoryCatalog
        {
            get => _factoryCatalog;
            set => _factoryCatalog = value;
        }

        public bool HasPlan => _plan != null && _plan.Factories.Any();

        public void NotifyPlanChanged()
        {
            PlanChanged?.Invoke();
        }
    }
}