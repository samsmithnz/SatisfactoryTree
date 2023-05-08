using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;
//using MethodType = SatisfactoryTree.Models.ManufactoringMethodType;

namespace SatisfactoryTree.Helpers
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
                        60, // Assuming normal node, MK1 miner
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
                    new Recipe(
                        new()
                        {
                            { "Iron Ore", 45 },
                            { "Coal", 45 }
                        },
                        new()
                        {
                            { "Steel Ingot", 45 }
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
