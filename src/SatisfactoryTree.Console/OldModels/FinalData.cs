namespace SatisfactoryTree.Console.OldModels
{
    public class FinalData
    {
        public Dictionary<string, double> buildings { get; set; }
        public PartDataInterface items { get; set; }
        public List<Recipe> recipes { get; set; }
        public List<PowerGenerationRecipe> powerGenerationRecipes { get; set; }
        public List<NewRecipe> newRecipes { get; set; }

        public FinalData(
            Dictionary<string, Double> buildings,
            PartDataInterface items,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes,
            List<NewRecipe> newRecipes)
        {
            this.buildings = buildings;
            this.items = items;
            this.recipes = recipes;
            this.powerGenerationRecipes = powerGenerationRecipes;
            this.newRecipes = newRecipes;
        }
    }
}
