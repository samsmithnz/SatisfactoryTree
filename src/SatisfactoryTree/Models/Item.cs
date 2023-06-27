namespace SatisfactoryTree.Models
{
    public class Item
    {

        //Stacksize
        //SinkValue

        public Item(int level,
            string name,
            string image,
            ItemType itemType = ItemType.Production,
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
        Production = 1,
        PowerGeneration = 2
    }

    public enum ResearchType
    {
        Tier1, //Note, Tier one includes 'the beginning', tier 0, and tier 1, as no resources are added in tier 1, and there are only a few in the beginning and tier 0
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7,
        Tier8
    }
}
