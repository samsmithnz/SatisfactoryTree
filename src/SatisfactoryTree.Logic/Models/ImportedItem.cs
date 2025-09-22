namespace SatisfactoryTree.Logic.Models
{
    public class ImportedItem
    {
        public int FactoryId { get; set; }
        public string FactoryName { get; set; } = string.Empty;
        public Item Item { get; set; } = new();
        public double PartQuantityImported { get; set; } = 0;

        public ImportedItem(int factoryId, string factoryName, Item item)
        {
            FactoryId = factoryId;
            FactoryName = factoryName;
            Item = item;
        }
    }
}
