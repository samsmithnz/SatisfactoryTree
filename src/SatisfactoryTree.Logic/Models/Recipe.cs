namespace SatisfactoryTree.Logic.Models
{
    public class Recipe
    {
        public string id { get; set; } = string.Empty;
        public string displayName { get; set; } = string.Empty;
        public List<Ingredient> ingredients { get; set; } = new List<Ingredient>();
        public List<Product> products { get; set; } = new List<Product>();
        public Building building { get; set; } = new Building();
        public bool isAlternate { get; set; }
        public bool isFicsmas { get; set; }
        public bool usesSAMOre { get; set; }
    }
}
