namespace SatisfactoryTree.Logic.Models
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<Item> Ingredients { get; set; } = new();
        public string Building { get; set; }
        public double BuildingQuantity { get; set; }

        public int Counter { get; set; }
    }
}
