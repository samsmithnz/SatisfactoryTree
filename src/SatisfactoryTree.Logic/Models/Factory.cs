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
        
        /// <summary>
        /// Tracks the names of parts that were explicitly added by the user to export.
        /// Used to distinguish user-defined exports from auto-added missing ingredients in the UI.
        /// </summary>
        public HashSet<string> UserDefinedExports { get; set; }

        public Factory(int id, string name)
        {
            Id = id;
            Name = name;
            ImportedParts = new();
            ExportedParts = new();
            ComponentParts = new();
            Surplus = new();
            UserDefinedExports = new();
        }
    }
}
