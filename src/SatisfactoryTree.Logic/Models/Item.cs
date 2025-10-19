namespace SatisfactoryTree.Logic.Models
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string ItemImagePath
        {
            get
            {
                string imageName = DisplayName.Replace(" ", "");
                return $"images/parts/{imageName}_256.png";
            }
        }
        public List<Item> Ingredients { get; set; } = new();
        public string ByProductName { get; set; } = string.Empty;
        public string ByProductDisplayName { get; set; } = string.Empty;
        public double ByProductQuantity { get; set; }
        public string ByProductImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(ByProductDisplayName))
                {
                    return "";
                }
                string imageName = ByProductDisplayName.Replace(" ", "");
                return $"images/parts/{imageName}_256.png";
            }
        }
        public string Building { get; set; } = string.Empty;
        public string BuildingDisplayName { get; set; } = string.Empty;
        public string BuildingImagePath { get; set; } = string.Empty;
        public double BuildingQuantity { get; set; }
        public double BuildingPowerUsage { get; set; }

        public int Counter { get; set; }
        public List<ItemIngredient> MissingIngredients { get; set; } = new();
        public bool HasMissingIngredients
        {
            get
            {
                return MissingIngredients.Any();
            }
        }

        public Recipe? Recipe { get; set; }
        public bool IsRedundant { get; set; } = false;
        public int Order { get; set; }
    }
}
