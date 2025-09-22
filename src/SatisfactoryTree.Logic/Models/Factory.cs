namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, ImportedItem> ImportedParts { get; set; }
        public List<ExportedItem> ExportedParts { get; set; }
        public List<Item> ComponentParts { get; set; }
        public List<Item> Surplus { get; set; }

        public Factory(int id, string name)
        {
            Id = id;
            Name = name;
            ImportedParts = new();
            ExportedParts = new();
            ComponentParts = new();
            Surplus = new();
        }
    }
}
