using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier6
    {
        //Level 6
        public static Item CateriumOre()
        {
            return new Item(6, "Caterium Ore",
                "Caterium_Ore.webp",
                ItemType.Item,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Caterium Ore", 1 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item CateriumIngot()
        {
            return new Item(6, "Caterium Ingot",
                "Caterium_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(4,
                        15,
                        new()
                        {
                            { "Caterium Ingot", 3 }
                        },
                        new()
                        {
                            { "Caterium Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }


    }
}
