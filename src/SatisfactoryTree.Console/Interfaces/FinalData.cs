namespace SatisfactoryTree.Console.Interfaces
{
    public class FinalData
    {
        public Dictionary<string, Double> Buildings { get; set; }
        public PartDataInterface Items { get; set; }
        public PartDataInterface Items2 { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<PowerGenerationRecipe> PowerGenerationRecipes { get; set; }

        public FinalData(
            Dictionary<string, Double> buildings,
            PartDataInterface items,
            PartDataInterface items2,
            List<Recipe> recipes,
            List<PowerGenerationRecipe> powerGenerationRecipes)
        {
            Buildings = buildings;
            Items = items;
            Items2 = items2;
            Recipes = recipes;
            PowerGenerationRecipes = powerGenerationRecipes;
        }
    }
}
