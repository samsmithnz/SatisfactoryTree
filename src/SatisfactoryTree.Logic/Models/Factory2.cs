using SatisfactoryTree.Logic.Calculations;

namespace SatisfactoryTree.Logic.Models
{
    public class Factory2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Item> Ingredients { get; set; }
        public Dictionary<int, ImportedItem> ImportedParts { get; set; }
        public FactoryCatalog FactoryCatalog;

        public Factory2(int id, string name, FactoryCatalog factoryCatalog)
        {
            Id = id;
            Name = name;
            Ingredients = new();
            ImportedParts = new();
            FactoryCatalog = factoryCatalog;
        }

        public void AddIngredient(string name, double quantity, Recipe? recipe)
        {
            Item item = new()
            {
                Name = name,
                Quantity = quantity,
                Recipe = recipe
            };

            //If no recipe was provided, get the default recipe for the part
            if (recipe == null)
            {
                List<Recipe> recipes = Lookups.GetRecipes(FactoryCatalog, name);
                recipe = recipes.FirstOrDefault();
            }
            if (recipe?.Products.Count > 0)
            {
                double ingredientRatio = recipe.Products[0].amount / quantity;
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    double ingredientAmount = ingredient.perMin * ingredientRatio;
                    item.Ingredients.Add(new()
                    {
                        Name = ingredient.part,
                        Quantity = ingredientAmount,
                        Recipe = recipe
                    });
                }
            }
            else
            {
                throw new Exception("Recipe is null: " + name + ", " + quantity);
            }
            Ingredients.Add(item);
        }
    }
}
