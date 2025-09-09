namespace SatisfactoryTree.Logic.Models
{
    public class PowerGenerationRecipe
    {
        public string id { get; set; } = string.Empty;
        public string displayName { get; set; } = string.Empty;
        public List<PowerIngredient> ingredients { get; set; } = new List<PowerIngredient>();
        public PowerProduct byproduct { get; set; } = new PowerProduct();
        public Building building { get; set; } = new Building();
    }
}
