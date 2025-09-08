using SatisfactoryTree.Logic.Extraction;

namespace SatisfactoryTree.Logic.Models
{
    public class ExtractedData
    {
        public Dictionary<string, double> buildings { get; set; }
        public Dictionary<string, Part> parts { get; set; }
        public Dictionary<string, RawResource> rawResources { get; set; }
        public List<Recipe> recipes { get; set; }
        public List<PowerGenerationRecipe> powerGenerationRecipes { get; set; }

        public ExtractedData(
            Dictionary<string, Double> buildings,
            RawPartsAndRawMaterials items,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes)
        {
            this.buildings = buildings;
            this.parts = items.Parts;
            this.rawResources = items.RawResources;
            this.recipes = recipes;
            this.powerGenerationRecipes = powerGenerationRecipes;
        }
    }
}
