namespace SatisfactoryTree.Models
{
    public class ProductionCalculation
    {
        public ProductionCalculation()
        {
            ProductionItems = [];
        }

        public List<ProductionItem> ProductionItems { get; set; }

        private decimal _powerConsumption;
        public decimal PowerConsumption
        {
            get
            {
                return Math.Round(_powerConsumption, 2);
            }
            set
            {
                _powerConsumption = value;
            }
        }
    }
}