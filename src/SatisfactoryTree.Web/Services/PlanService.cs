using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services
{
    public class PlanService
    {
        private readonly ILocalStorageService _localStorage;
        private const string PLAN_STORAGE_KEY = "satisfactory-factory-plan";
        
        private Plan? _plan;
        
        public event Action? PlanChanged;

        public PlanService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public Plan? Plan 
        { 
            get => _plan; 
            set 
            { 
                _plan = value; 
                PlanChanged?.Invoke();
                _ = SavePlanAsync(); // Fire and forget
            } 
        }

        public bool HasPlan => _plan != null && _plan.Factories.Any();

        public async Task<Plan?> LoadPlanAsync()
        {
            try
            {
                var savedPlan = await _localStorage.GetItemAsync<Plan>(PLAN_STORAGE_KEY);
                if (savedPlan != null)
                {
                    _plan = savedPlan;
                    PlanChanged?.Invoke();
                }
                return savedPlan;
            }
            catch (Exception)
            {
                // If loading fails, return null to use default plan
                return null;
            }
        }

        public async Task SavePlanAsync()
        {
            if (_plan != null)
            {
                await _localStorage.SetItemAsync(PLAN_STORAGE_KEY, _plan);
            }
        }

        public async Task ClearSavedPlanAsync()
        {
            await _localStorage.RemoveItemAsync(PLAN_STORAGE_KEY);
        }
    }
}