namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public List<Part> Imports { get; set; }
        public List<Part> Surplus { get; set; }
        public List<Part> PartGoals { get; set; }
        public List<Part> Parts { get; set; }

        public Factory()
        {
            Imports = new();
            Surplus = new();
            PartGoals = new();
            Parts = new();
        }
    }
}
