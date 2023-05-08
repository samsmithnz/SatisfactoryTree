namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item? item, decimal quantity)
        //Building? building, decimal buildingQuantity)
        {
            Item = item;
            Quantity = quantity;
            //Building = building;
            //BuildingQuantity = buildingQuantity;
            Dependencies = new();
        }
        public Item? Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal BuildingQuantityRequired { get; set; }
        //public Building? Building { get; set; }
        //public decimal BuildingQuantity { get; set; }
        public Dictionary<string, decimal> Dependencies { get; set; }
    }
}
