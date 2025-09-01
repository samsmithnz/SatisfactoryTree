namespace SatisfactoryTree.Models
{
    public class Storage
    {
        public Storage(string factoryId = "default")
        {
            FactoryId = factoryId;
            Items = new Dictionary<string, decimal>();
        }

        public string FactoryId { get; set; }
        public Dictionary<string, decimal> Items { get; set; }

        public void AddItem(string itemName, decimal quantity)
        {
            if (Items.ContainsKey(itemName))
            {
                Items[itemName] += quantity;
            }
            else
            {
                Items[itemName] = quantity;
            }
        }

        public bool RemoveItem(string itemName, decimal quantity)
        {
            if (Items.ContainsKey(itemName) && Items[itemName] >= quantity)
            {
                Items[itemName] -= quantity;
                if (Items[itemName] == 0)
                {
                    Items.Remove(itemName);
                }
                return true;
            }
            return false;
        }

        public decimal GetItemQuantity(string itemName)
        {
            return Items.ContainsKey(itemName) ? Items[itemName] : 0;
        }

        public List<KeyValuePair<string, decimal>> GetAllItems()
        {
            return Items.ToList();
        }
    }
}