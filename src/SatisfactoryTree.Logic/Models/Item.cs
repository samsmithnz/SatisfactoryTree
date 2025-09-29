namespace SatisfactoryTree.Logic.Models
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<Item> Ingredients { get; set; } = new();
        public string Building { get; set; } = string.Empty;
        public double BuildingQuantity { get; set; }
        public double BuildingPowerUsage { get; set; }

        public int Counter { get; set; }
        public List<string> MissingIngredients { get; set; } = new();
        public bool HasMissingIngredients => MissingIngredients.Any();
    }
}
