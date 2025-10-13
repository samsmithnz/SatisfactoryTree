using SatisfactoryTree.Logic.Calculations;

namespace SatisfactoryTree.Logic.Models
{
    public class Factory2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Ingredients { get; set; }
        public Dictionary<int, ImportedItem> ImportedParts { get; set; }
        private FactoryCatalog _factoryCatalog;

        public Factory2(int id, string name, FactoryCatalog factoryCatalog)
        {
            Id = id;
            Name = name;
            Ingredients = new();
            ImportedParts = new();
            _factoryCatalog = factoryCatalog;
        }

        public void AddIngredient(string name, double quantity)
        {
            Item item = new()
            {
                Name = name,
                Quantity = quantity
            };

            List<Recipe> recipes = Lookups.GetRecipes(_factoryCatalog, name);
            if (recipes.Count > 0 && recipes[0].Products.Count > 0)
            {
                double ingredientRatio = recipes[0].Products[0].amount / quantity;
                foreach (Ingredient ingredient in recipes[0].Ingredients)
                {
                    double ingredientAmount = ingredient.perMin * ingredientRatio;
                    item.Ingredients.Add(new()
                    {
                        Name = ingredient.part,
                        Quantity = ingredientAmount
                    });
                }
            }
        }
    }
}
