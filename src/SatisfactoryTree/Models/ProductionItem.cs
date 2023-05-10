namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item? item, decimal quantity)
        {
            Item = item;
            Quantity = quantity;
            Dependencies = new();
        }
        public Item? Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal BuildingQuantityRequired { get; set; }
        public Dictionary<string, decimal> Dependencies { get; set; }
        public bool OutputItem { get; set; }
    }
}
