namespace SatisfactoryTree.Logic.Models
{
    public class PowerGenerationRecipe
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<PowerIngredient> ingredients { get; set; }
        public PowerProduct byproduct { get; set; }
        public Building building { get; set; }
    }
}
