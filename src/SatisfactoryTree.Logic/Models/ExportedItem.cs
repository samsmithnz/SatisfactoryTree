namespace SatisfactoryTree.Logic.Models
{
    public class ExportedItem
    {
        public Item Item { get; set; } = new();
        public double PartQuantityExported { get; set; } = 0;

        public ExportedItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Item = item;
        }
    }
}
