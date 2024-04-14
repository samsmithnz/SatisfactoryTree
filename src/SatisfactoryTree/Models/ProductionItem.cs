using SatisfactoryTree.Helpers;

namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item? item, decimal quantity)
        {
            Item = item;
            if (item != null)
            {
                Building = AllBuildings.FindBuilding(item.Recipes[0].Building);
            }
            Quantity = quantity;
            Dependencies = [];
        }
        public Item? Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal BuildingQuantityRequired { get; set; }
        public Building? Building { get; set; }
        public Dictionary<string, decimal> Dependencies { get; set; }
        public bool OutputItem { get; set; }
        public string Name => Item?.Name ?? "Unknown";
        public override string ToString()
        {
            if (Building != null)
            {Building to
                return $"{Name} x{Quantity} ({Building.Name} x{BuildingQuantityRequired})";
            }
            else
            {
                return $"{Name} x{Quantity}";
            }
        }
    }
}
