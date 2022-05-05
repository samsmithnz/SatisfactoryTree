namespace DSPTree.Models
{
    public class Item
    {

        //STacksize
        //SInkValue

        public Item(int level,
            string name,
            string image,
            ItemType itemType = ItemType.Item,
            ResearchType researchType = ResearchType.Tier1)
        {
            Level = level;
            Name = name;
            Image = image;
            Recipes = new();
            ItemType = itemType;
            ResearchType = researchType;
        }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ItemType ItemType { get; set; }
        public ResearchType ResearchType { get; set; }
        public List<Recipe> Recipes { get; set; }
    }

    public enum ItemType
    {
        All = 0,
        Item = 1,
        Building = 2
    }

    public enum ResearchType
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7
    }
}
