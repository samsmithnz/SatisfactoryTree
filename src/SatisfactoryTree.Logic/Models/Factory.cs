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
        
        /// <summary>
        /// Tracks custom recipe selections for component parts.
        /// Key is the part name, value is the recipe name.
        /// When a user changes a component part's recipe, it's stored here so it persists across recalculations.
        /// </summary>
        public Dictionary<string, string> ComponentPartRecipeOverrides { get; set; }

        public Factory(int id, string name)
        {
            Id = id;
            Name = name;
            ImportedParts = new();
            ExportedParts = new();
            ComponentParts = new();
            Surplus = new();
            UserDefinedExports = new();
            ComponentPartRecipeOverrides = new();
        }
    }
}
