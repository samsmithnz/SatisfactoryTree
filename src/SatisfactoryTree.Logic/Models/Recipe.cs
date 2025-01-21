using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Models
{
    public class Recipe
    {
        public Recipe(string partId, string recipeId, string recipeName, List<Ingredient> ingredients, double quantity)
        {
            PartId = partId;
            RecipeId = recipeId;
            RecipeName = recipeName;
            Ingredients = ingredients;
            Quantity = quantity;
        }
        public string PartId { get; set; }
        public string RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public double Quantity { get; set; }
    }
}
