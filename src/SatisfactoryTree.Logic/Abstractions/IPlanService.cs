using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Abstractions;

public interface IPlanService
{
    event Action? PlanChanged;

    Plan? Plan { get; set; }
    FactoryCatalog? FactoryCatalog { get; set; }

    bool HasPlan { get; }
    int? LastAddedFactoryId { get; }

    void ClearLastAddedFactory();

    void AddFactory();

    void AddExportedPartToFactory(int factoryId, string itemName, double quantity);
    void RemoveExportedPartFromFactory(int factoryId, string itemName);

    void AddImportedPartToFactory(int factoryId, int sourceFactoryId, string sourceFactoryName, string itemName, double quantity);

    void AddAllMissingIngredients(int factoryId);
    List<string> GetMissingIngredients(int factoryId);

    void RefreshPlanCalculations();

    void NotifyPlanChanged();
}
