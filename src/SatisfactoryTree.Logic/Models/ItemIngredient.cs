namespace SatisfactoryTree.Logic.Models
{
    public class ItemIngredient
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string? IngredientImagePart
        {
            get
            {
                string imageName = DisplayName.Replace(" ", "");
                return $"images/parts/{imageName}_256.png";
            }
        }
    }
}
