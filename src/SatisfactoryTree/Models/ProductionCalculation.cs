namespace SatisfactoryTree.Models
{
    public class ProductionCalculation
    {
        public ProductionCalculation()
        {
            ProductionItems = new();
        }

        public List<ProductionItem> ProductionItems { get; set; }
        public decimal PowerConsumption { get; set; }
    }
}