namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public List<Item> ImportedParts { get; set; }
        public List<Item> TargetParts { get; set; }
        public List<Item> ComponentParts { get; set; }
        public List<Item> Surplus { get; set; }

        public Factory()
        {
            ImportedParts = new();
            TargetParts = new();
            ComponentParts = new();
            Surplus = new();
        }
    }
}
