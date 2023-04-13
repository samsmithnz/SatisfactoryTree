using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public class BuildingPool
    {
        public static Item MiningMachineMk1()
        {
            return new Item(1, "Iron Ore",
                "Iron_Ore.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Iron Ore", 1 }
                        },
                        ManufactoringBuildingType.MiningMachine)
                }
            };
        }
    }
}
