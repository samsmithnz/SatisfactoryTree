namespace SatisfactoryTree.Models
{
    public class NewTargetItem
    {
        public NewTargetItem(string itemName, decimal itemQuantity)
        {
            ItemName = itemName;
            ItemQuantity = itemQuantity;
        }

        public string ItemName { get; set; }
        public decimal ItemQuantity { get; set; }
    }
}
