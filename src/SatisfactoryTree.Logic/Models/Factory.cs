namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public string Name { get; set; }
        public Dictionary<string, Item> ImportedParts { get; set; }
        public List<Item> TargetParts { get; set; }
        public List<Item> ComponentParts { get; set; }
        public List<Item> Surplus { get; set; }

        public Factory(string name)
        {
            Name = name;
            ImportedParts = new();
            TargetParts = new();
            ComponentParts = new();
            Surplus = new();
        }
    }
}
