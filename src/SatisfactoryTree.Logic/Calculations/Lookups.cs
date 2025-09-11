using SatisfactoryTree.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Calculations
{
    public class Lookups
    {

        public static List<Part> GetParts(FactoryCatalog factoryCatalog)
        {
            List<Part> parts = new();
            foreach (var item in factoryCatalog.Parts)
            {
                parts.Add(item.Value);
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
