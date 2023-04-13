using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier1
    {
        public static Item IronOre()
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
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item IronIngot()
        {
            return new Item(1, "Iron Ingot",
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
                            { "Iron Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item IronPlate()
        {
            return new Item(1, "Iron Plate",
                "Iron_Plate.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Iron Ingot", 3 }
                        },
                        new()
                        {
                            { "Iron Plate", 2 }
                        },
                        BuildingType.Constructor)
                }
            };
        }
        public static Item IronRod()
        {
            return new Item(1, "Iron Rod",
                "Iron_Rod.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(4,
                        15,
                        new()
                        {
                            { "Iron Ingot", 1 }
                        },
                        new()
                        {
                            { "Iron Rod", 1 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item CopperOre()
        {
            return new Item(1, "Copper Ore",
                "Copper_Ore.webp",
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
                            { "Copper Ore", 1 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item CopperIngot()
        {
            return new Item(1, "Copper Ingot",
                "Copper_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Copper Ore", 1 }
                        },
                        new()
                        {
                            { "Copper Ingot", 1 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item Wire()
        {
            return new Item(1, "Wire",
                "Wire.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(4,
                        30,
                        new()
                        {
                            { "Copper Ingot", 1 }
                        },
                        new()
                        {
                            { "Wire", 2 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item Cable()
        {
            return new Item(1, "Cable",
                "Cable.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(2,
                        30,
                        new()
                        {
                            { "Wire", 2 }
                        },
                        new()
                        {
                            { "Cable", 1 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item Limestone()
        {
            return new Item(1, "Limestone",
                "Limestone.webp",
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
                            { "Limestone", 1 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item Concrete()
        {
            return new Item(1, "Concrete",
                "Concrete.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(4,
                        15,
                        new()
                        {
                            { "Limestone", 3 }
                        },
                        new()
                        {
                            { "Concrete", 1 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item Screw()
        {
            return new Item(1, "Screw",
                "Screw.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(6,
                        40,
                        new()
                        {
                            { "Iron Rod", 1 }
                        },
                        new()
                        {
                            { "Screw", 10 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item ReinforcedIronPlate()
        {
            return new Item(1, "Reinforced Iron Plate",
                "Reinforced_Iron_Plate.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(12,
                        5,
                        new()
                        {
                            { "Iron Plate", 6 },
                            { "Screw", 12 }
                        },
                        new()
                        {
                            { "Reinforced Iron Plate", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }


    }
}
