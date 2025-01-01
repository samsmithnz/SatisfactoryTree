using SatisfactoryTree.Helpers;

namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item? item, decimal quantity)
        {
            Item = item;
            //TODO
            //if (item != null)
            //{
            //    Building = AllBuildings.FindBuilding(item.Recipes[0].Building);
            //}
            Quantity = quantity;
            Dependencies = [];
        }
        public ProductionItem(string itemName, decimal quantity)
        {
            ItemName = itemName;
            Quantity = quantity;
        }
        public Item? Item { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal BuildingQuantityRequired { get; set; }
        public Building? Building { get; set; }
        public Dictionary<string, decimal> Dependencies { get; set; }
        public bool OutputItem { get; set; }
        public string Name
        {
            get
            {
                return Item?.Name ?? "Unknown";
            }
        }
        //public override string ToString()
        //{
        //    if (Building != null)
        //    {
        //        return $"{Name} x{Quantity} ({Building.Name} x{BuildingQuantityRequired})";
        //    }
        //    else
        //    {
        //        return $"{Name} x{Quantity}";
        //    }
        //}
    }
}
