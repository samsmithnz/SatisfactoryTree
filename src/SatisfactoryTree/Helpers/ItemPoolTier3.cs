using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier3
    {
        //Level 3
        public static Item Coal()
        {
            return new Item(3, "Coal",
                "Coal.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Coal", 1 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item SteelIngot()
        {
            return new Item(3, "Steel Ingot",
                "Steel_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(4,
                        45,
                        new()
                        {
                            { "Iron Ore", 3 },
                            { "Coal", 3 }
                        },
                        new()
                        {
                            { "Steel Ingot", 3 }
                        },
                        BuildingType.Foundry)
                }
            };
        }

        public static Item SteelBeam()
        {
            return new Item(3, "Steel Beam",
                "Steel_Beam.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(4,
                        15,
                        new()
                        {
                            { "Steel Ingot", 4 }
                        },
                        new()
                        {
                            { "Steel Beam", 1 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item SteelPipe()
        {
            return new Item(3, "Steel Pipe",
                "Steel_Pipe.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Steel Ingot", 3 }
                        },
                        new()
                        {
                            { "Steel Pipe", 2 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item VersatileFramework()
        {
            return new Item(3, "Versatile Framework",
                "Versatile_Framework.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(24,
                        5,
                        new()
                        {
                            { "Modular Frame", 2.5m },
                            { "Steel Beam", 12 }
                        },
                        new()
                        {
                            { "Versatile Framework", 2 }
                        },
                        BuildingType.Assembler)
                }
            };
        }


        public static Item Water()
        {
            return new Item(3, "Water",
                "Water.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(1,
                        120,
                        new(),
                        new()
                        {
                            { "Water", 2 }
                        },
                        BuildingType.WaterExtractor)
                }
            };
        }


    }
}
