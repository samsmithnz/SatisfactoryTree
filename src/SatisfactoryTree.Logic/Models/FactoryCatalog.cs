using SatisfactoryTree.Logic.Extraction;

namespace SatisfactoryTree.Logic.Models
{
    public class FactoryCatalog
    {
        public Dictionary<string, double> Buildings { get; set; }
        public Dictionary<string, Part> Parts { get; set; }
        public Dictionary<string, RawResource> RawResources { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<PowerGenerationRecipe> PowerGenerationRecipes { get; set; }

        // Parameterless constructor for JSON deserialization
        public FactoryCatalog()
        {
            Buildings = new Dictionary<string, double>();
            Parts = new Dictionary<string, Part>();
            RawResources = new Dictionary<string, RawResource>();
            Recipes = new List<Recipe>();
            PowerGenerationRecipes = new List<PowerGenerationRecipe>();
        }

        public FactoryCatalog(
            Dictionary<string, Double> buildings,
            RawPartsAndRawMaterials items,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes)
        {
            this.Buildings = buildings;
            this.Parts = items.Parts;
            this.RawResources = items.RawResources;
            this.Recipes = recipes;
            this.PowerGenerationRecipes = powerGenerationRecipes;
        }
    }
}
