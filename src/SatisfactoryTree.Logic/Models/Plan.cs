using SatisfactoryTree.Logic.Extraction;

namespace SatisfactoryTree.Logic.Models
{
    public class Plan
    {
        public List<Part> Parts { get; set; } = new();
        public List<RawRecipe> Recipes { get; set; } = new();
        public List<Building> Buildings { get; set; } = new();
        public List<Factory> Factories { get; set; } = new();

    }   

}
