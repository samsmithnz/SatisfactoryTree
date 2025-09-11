using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Extraction
{
    public class ProcessedResult
    {
        public List<RawPart> Parts = new();
        public List<RawRecipe> Recipes = new();
        public List<Building> Buildings = new();
    }
}
