namespace SatisfactoryTree.Models
{
    public class ProductionItem
    {
        public ProductionItem(Item item, decimal quantity)
        {
            Item = item;
            Quantity = quantity;
            Parents = new();
        }
        public Item Item { get; set; }
        public decimal Quantity { get; set; }
        public List<ProductionItem> Parents { get; set; }
    }
}
