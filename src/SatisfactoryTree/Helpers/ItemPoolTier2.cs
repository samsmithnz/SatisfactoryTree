using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier2
    {
        //Level 2
        public static Item IronIngot()
        {
            return new Item(2, "Iron Ingot",
                "Iron_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Iron Ore", 1 }
                        },
                        new()
                        {
                            {"Iron Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item IronIngot()
        {
            return new Item(2, "Iron Ingot",
                "Iron_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Iron Ore", 1 }
                        },
                        new()
                        {
                            {"Iron Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item IronIngot()
        {
            return new Item(2, "Iron Ingot",
                "Iron_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Iron Ore", 1 }
                        },
                        new()
                        {
                            {"Iron Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item IronIngot()
        {
            return new Item(2, "Iron Ingot",
                "Iron_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Iron Ore", 1 }
                        },
                        new()
                        {
                            {"Iron Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

    }
}
