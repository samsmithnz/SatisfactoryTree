using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services
{
    public class PlanService
    {
        private Plan? _plan;
        private FactoryCatalog? _factoryCatalog;
        private int? _lastAddedFactoryId; // track last added factory

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

        public int? LastAddedFactoryId => _lastAddedFactoryId;

        public void ClearLastAddedFactory() => _lastAddedFactoryId = null;

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
            Factory newFactory = new(nextId, factoryName);

            _plan.Factories.Add(newFactory);

            _lastAddedFactoryId = newFactory.Id; // mark for scrolling

            // Notify listeners so the new factory renders
            PlanChanged?.Invoke();
        }

        public void AddExportedPartToFactory(int factoryId, string itemName, double quantity)
        {
            if (_plan == null || _factoryCatalog == null)
            { 
                return;
            }

            Factory factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            { 
                return;
            }

            // Check if this exported part already exists
            ExportedItem existingExport = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == itemName);
            if (existingExport != null)
            {
                // Update existing quantity
                existingExport.Item.Quantity = quantity;
            }
            else
            {
                // Add new exported part
                factory.ExportedParts.Add(new(new Item { Name = itemName, Quantity = quantity }));
            }

            // Recalculate the entire plan
            RefreshPlanCalculations();
        }

        public void RemoveExportedPartFromFactory(int factoryId, string itemName)
        {
            if (_plan == null)
            { 
                return;
            }

            Factory factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            { 
                return;
            }

            ExportedItem exportToRemove = factory.ExportedParts.FirstOrDefault(e => e.Item.Name == itemName);
            if (exportToRemove != null)
            {
                factory.ExportedParts.Remove(exportToRemove);

                // Recalculate the entire plan
                RefreshPlanCalculations();
            }
        }

        public void AddImportedPartToFactory(int factoryId, int sourceFactoryId, string sourceFactoryName, string itemName, double quantity)
        {
            if (_plan == null)
            { 
                return;
            }

            Factory factory = _plan.Factories.FirstOrDefault(f => f.Id == factoryId);
            if (factory == null)
            { 
                return;
            }

            // Add or update imported part
            ImportedItem importedItem = new(sourceFactoryId, sourceFactoryName, new Item { Name = itemName, Quantity = quantity });
            factory.ImportedParts[sourceFactoryId] = importedItem;

            // Recalculate the entire plan
            RefreshPlanCalculations();
        }

        public void RefreshPlanCalculations()
        {
            if (_plan == null || _factoryCatalog == null)
            { 
                return;
            }

            try
            {
                // Recalculate the plan
                _plan.UpdatePlanCalculations(_factoryCatalog);

                // Notify UI that plan has changed
                PlanChanged?.Invoke();
            }
            catch (Exception ex)
            {
                // Log error or handle it appropriately
                Console.WriteLine($"Error updating plan calculations: {ex.Message}");
            }
        }

        public void NotifyPlanChanged()
        {
            PlanChanged?.Invoke();
        }
    }
}