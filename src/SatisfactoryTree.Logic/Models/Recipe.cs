namespace SatisfactoryTree.Logic.Models
{
    public class Recipe
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Product> products { get; set; }
        public Building building { get; set; }
        public bool isAlternate { get; set; }
        public bool isFicsmas { get; set; }
        public bool usesSAMOre { get; set; }
    }
}
