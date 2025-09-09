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

        // Parameterless constructor for JSON deserialization
        public ExtractedData()
        {
            buildings = new Dictionary<string, double>();
            parts = new Dictionary<string, Part>();
            rawResources = new Dictionary<string, RawResource>();
            recipes = new List<Recipe>();
            powerGenerationRecipes = new List<PowerGenerationRecipe>();
        }

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
