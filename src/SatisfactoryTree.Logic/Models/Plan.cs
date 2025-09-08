namespace SatisfactoryTree.Logic.Models
{
    public class Plan
    {
        public List<Part> Parts { get; set; } = new List<Part>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public List<Building> Buildings { get; set; } = new List<Building>();
        public List<Factory> Factories { get; set; } = new List<Factory>();

    }   

}
