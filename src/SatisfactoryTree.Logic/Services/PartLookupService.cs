using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Services;

// Pure, testable helper for part lookups and display name resolution.
public class PartLookupService
{
    // Ensure catalog.PartsLookup is populated from catalog.Parts when needed.
    public void EnsurePartsLookup(FactoryCatalog catalog)
    {
        if (catalog == null)
        {
            throw new ArgumentNullException(nameof(catalog));
        }

        if ((catalog.PartsLookup == null || catalog.PartsLookup.Count == 0)
            && catalog.Parts != null && catalog.Parts.Count > 0)
        {
            catalog.PartsLookup = catalog.Parts
                .Select(kvp => new LookupItem(kvp.Key, kvp.Value?.Name ?? kvp.Key))
                .OrderBy(l => l.Name)
                .ToList();
        }
    }

    // Build a quick dictionary for id->display name
    public Dictionary<string, string> BuildDisplayLookup(FactoryCatalog catalog)
    {
        if (catalog == null)
        {
            throw new ArgumentNullException(nameof(catalog));
        }
        if (catalog.PartsLookup == null)
        {
            return new Dictionary<string, string>();
        }

        return catalog.PartsLookup
            .GroupBy(p => p.Id)
            .ToDictionary(g => g.Key, g => g.First().Name);
    }

    public string GetPartDisplayName(FactoryCatalog catalog, string partId, Dictionary<string, string>? cache = null)
    {
        if (string.IsNullOrWhiteSpace(partId))
        {
            return partId;
        }

        if (cache != null && cache.TryGetValue(partId, out var value))
        {
            return value;
        }

        LookupItem? lookup = catalog.PartsLookup?.FirstOrDefault(p => p.Id == partId);
        return lookup?.Name ?? partId;
    }

    public LookupItem? GetCurrentLookup(Item item, FactoryCatalog catalog)
    {
        if (item == null)
        {
            return null;
        }
        return catalog.PartsLookup?.FirstOrDefault(p => p.Id == item.Name);
    }
}
