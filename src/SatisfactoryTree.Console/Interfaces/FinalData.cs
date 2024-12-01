namespace SatisfactoryTree.Console.Interfaces
{
    public class FinalData
    {
        public Dictionary<string, Double> Buildings { get; set; }
        public PartDataInterface Items { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<PowerGenerationRecipe> PowerGenerationRecipes { get; set; }

        public FinalData(
            Dictionary<string, Double> buildings,
            PartDataInterface items,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes)
        {
            Buildings = buildings;
            Items = items;
            Recipes = recipes;
            PowerGenerationRecipes = powerGenerationRecipes;
        }
    }
}
