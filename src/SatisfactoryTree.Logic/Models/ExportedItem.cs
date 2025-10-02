namespace SatisfactoryTree.Logic.Models
{
    public class ExportedItem
    {
        public Item Item { get; set; } = new();
        public double PartQuantityExported { get; set; } = 0;
        
        /// <summary>
        /// Indicates whether this exported item was automatically added to resolve missing ingredients,
        /// rather than being explicitly added by the user. Auto-added items are hidden from the 
        /// "Exported parts" UI section but still drive production calculations.
        /// </summary>
        public bool IsAutoAdded { get; set; } = false;

        public ExportedItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Item = item;
        }
    }
}
