namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public List<Item> ImportedParts { get; set; }
        public List<Item> ExportedParts { get; set; }
        public List<Item> ComponentParts { get; set; }
        public List<Item> Surplus { get; set; }

        public Factory()
        {
            ImportedParts = new();
            ExportedParts = new();
            ComponentParts = new();
            Surplus = new();
        }
    }
}
