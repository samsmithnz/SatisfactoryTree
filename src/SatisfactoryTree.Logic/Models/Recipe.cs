namespace SatisfactoryTree.Logic.Models
{
    public class Recipe
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public Building Building { get; set; } = new();
        public bool IsAlternate { get; set; }
        public bool IsFicsmas { get; set; }
        public bool UsesSAMOre { get; set; }
    }
}
