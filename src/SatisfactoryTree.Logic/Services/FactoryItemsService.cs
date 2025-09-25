using SatisfactoryTree.Logic.Abstractions;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Services;

// Thin orchestration over domain objects, independent of Blazor UI
public class FactoryItemsService
{
    private readonly IPlanService _planService;

    public FactoryItemsService(IPlanService planService)
    {
        _planService = planService;
    }

    public void AddExportedPart(Factory factory, string partId, double quantity)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        if (string.IsNullOrWhiteSpace(partId))
        {
            throw new ArgumentException("Part id required", nameof(partId));
        }

        _planService.AddExportedPartToFactory(factory.Id, partId, quantity);
    }

    public void RemoveExportedPart(Factory factory, string partId)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        if (string.IsNullOrWhiteSpace(partId))
        {
            return;
        }

        _planService.RemoveExportedPartFromFactory(factory.Id, partId);
    }
}
