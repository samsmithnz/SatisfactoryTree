namespace SatisfactoryTree.Models
{
    public class ItemGroup
    {
        public int Dependencies { get; set; }
        public string Name { get; set; }
        public int ResearchTier { get; set; }

        public ItemGroup(int dependencies, string name, int researchTier)
        {
            Dependencies = dependencies;
            Name = name;
            ResearchTier = researchTier;
        }
    }
}
