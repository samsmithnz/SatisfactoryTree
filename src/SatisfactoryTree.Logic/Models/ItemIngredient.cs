namespace SatisfactoryTree.Logic.Models
{
    public class ItemIngredient
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string? IngredientImagePart { get; set; }
    }
}
