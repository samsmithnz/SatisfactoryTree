using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Calculations
{
    public class Lookups
    {

        public static List<LookupItem> GetParts(FactoryCatalog factoryCatalog)
        {
            List<LookupItem> parts = new();
            foreach (KeyValuePair<string, Part> item in factoryCatalog.Parts)
            {
                parts.Add(new(item.Key, item.Value.Name));
            }
            //order the parts by name
            return parts.OrderBy(p => p.Name).ToList();
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
            return recipes.OrderBy(p => p.IsAlternate).ThenByDescending(p=>p.DisplayName).ToList();
        }
    }
}
