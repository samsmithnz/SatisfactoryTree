using SatisfactoryTree.Logic.Extraction;

namespace SatisfactoryTree.Logic.Models
{
    public class FactoryCatalog
    {
        public Dictionary<string, double> Buildings { get; set; }
        public Dictionary<string, Part> Parts { get; set; }
        public Dictionary<string, RawResource> RawResources { get; set; }
        public List<LookupItem> PartsLookup { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<PowerGenerationRecipe> PowerGenerationRecipes { get; set; }

        // Parameterless constructor for JSON deserialization
        public FactoryCatalog()
        {
            Buildings = new();
            Parts = new();
            RawResources = new();
            PartsLookup = new();
            Recipes = new();
            PowerGenerationRecipes = new();
        }

        public FactoryCatalog(
            Dictionary<string, Double> buildings,
            RawPartsAndRawMaterials items,
            List<LookupItem> partsLookup,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes)
        {
            this.Buildings = buildings;
            this.Parts = items.Parts;
            this.RawResources = items.RawResources;
            this.PartsLookup = partsLookup;
            this.Recipes = recipes;
            this.PowerGenerationRecipes = powerGenerationRecipes;
        }
    }
}
