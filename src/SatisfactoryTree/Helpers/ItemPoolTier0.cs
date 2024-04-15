using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier0
    {
        public static Item IronOre()
        {
            return new Item(0, "Iron Ore",
                "IronOre_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Iron Ore", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }
        public static Item CopperOre()
        {
            return new Item(0, "Copper Ore",
                "CopperOre_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Copper Ore", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item Limestone()
        {
            return new Item(0, "Limestone",
                "Limestone_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Limestone", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item Wood()
        {
            return new Item(0, "Wood",
                "Wood_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Wood", 60 }
                        },
                        "")
                }
            };
        }

        public static Item Leaves()
        {
            return new Item(0, "Leaves",
                "Leaves_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Leaves", 60 }
                        },
                        "")
                }
            };
        }

        public static Item AlienProtein()
        {
            return new Item(0, "Alien Protein",
                "AlienProtein_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Alien Protein", 60 }
                        },
                        "")
                }
            };
        }

        public static Item Mycelia()
        {
            return new Item(0, "Mycelia",
                "Mycelia_256.png",
                ItemType.Production,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Mycelia", 60 }
                        },
                        "")
                }
            };
        }

        public static Item Coal()
        {
            return new Item(0, "Coal",
                "Coal_256.png",
                ItemType.Production,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Coal", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item Water()
        {
            return new Item(0, "Water",
                "Water_256.png",
                ItemType.Production,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Water", 120 }
                        },
                        "Water Extractor")
                }
            };
        }

        public static Item CrudeOil()
        {
            return new Item(0, "Crude Oil",
                "CrudeOil_256.png",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Crude Oil", 120 }
                        },
                        "Oil Extractor")
                }
            };
        }

        public static Item CateriumOre()
        {
            return new Item(0, "Caterium Ore",
                "CateriumOre_256.png",
                ItemType.Production,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Caterium Ore", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item Bauxite()
        {
            return new Item(0, "Bauxite",
                "Bauxite_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Bauxite", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item RawQuartz()
        {
            return new Item(0, "Raw Quartz",
                "RawQuartz_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Raw Quartz", 60 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Sulfur()
        {
            return new Item(0, "Sulfur",
                "Sulfur_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Sulfur", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }
        public static Item Uranium()
        {
            return new Item(0, "Uranium",
                "Uranium_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Uranium", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }
        public static Item NitrogenGas()
        {
            return new Item(0, "Nitrogen Gas",
                "NitrogenGas_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Nitrogen Gas", 120 }
                        },
                        "Resource Well Extractor")
                }
            };
        }

    }
}
