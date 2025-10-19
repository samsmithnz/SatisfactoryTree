using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Logic.Services;

namespace SatisfactoryTree.Logic.Calculations
{
    public class Lookups
    {

        private static Dictionary<string, string>? _partsDisplayLookup; // id -> display name cache

        public static List<LookupItem> GetParts(Dictionary<string, Part> parts)
        {
            List<LookupItem> items = new();
            foreach (KeyValuePair<string, Part> item in parts)
            {
                items.Add(new(item.Key, item.Value.Name));
            }
            //order the parts by name
            return items.OrderBy(p => p.Name).ToList();
        }

        public static List<Recipe> GetRecipes(FactoryCatalog factoryCatalog, string partName)
        {
            List<Recipe> recipes = new();
            foreach (Recipe item in factoryCatalog.Recipes)
            {
                // Check if any product in the recipe matches the partName
                if (item.Products.Any(p => p.part == partName))
                {
                    recipes.Add(item);
                }
            }
            //order the recipes by name
            return recipes.OrderBy(p => p.IsAlternate).ThenByDescending(p => p.DisplayName).ToList();
        }

        public static double GetBuildingPower(FactoryCatalog factoryCatalog, string building, double quantity)
        {
            double buildingPower = 0;
            foreach (KeyValuePair<string, double> item in factoryCatalog.Buildings)
            {
                if (building == item.Key)
                {
                    buildingPower = item.Value;
                    break;
                }
            }
            int wholeBuildingCount = (int)Math.Floor(quantity);
            double fractionalBuildingCount = quantity - wholeBuildingCount;
            double result = (buildingPower * wholeBuildingCount) + (buildingPower * Math.Pow(fractionalBuildingCount, 1.321928));
            result = (double)Math.Round((decimal)result, 3);
            return result;
        }

        public static string GetPartDisplayName(FactoryCatalog factoryCatalog, string partName)
        {
            if (string.IsNullOrWhiteSpace(partName))
            {
                return partName;
            }

            LookupItem? lookup = factoryCatalog.PartsLookup?.FirstOrDefault(p => p.Id == partName);
            return lookup?.Name ?? partName;
        }

        //// Build a quick dictionary for id->display name
        //public Dictionary<string, string> BuildDisplayLookup(FactoryCatalog catalog)
        //{
        //    if (catalog == null)
        //    {
        //        throw new ArgumentNullException(nameof(catalog));
        //    }
        //    if (catalog.PartsLookup == null)
        //    {
        //        return new Dictionary<string, string>();
        //    }

        //    return catalog.PartsLookup
        //        .GroupBy(p => p.Id)
        //        .ToDictionary(g => g.Key, g => g.First().Name);
        //}
    }
}
