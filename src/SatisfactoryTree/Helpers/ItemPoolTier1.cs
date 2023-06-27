using SatisfactoryTree.Models;

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

        public static Item IronIngot()
        {
            return new Item(1, "Iron Ingot",
                "Iron_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Iron Ore", 30 }
                        },
                        new()
                        {
                            { "Iron Ingot", 30 }
                        },
                        "Smelter")
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
                    new Recipe(
                        new()
                        {
                            { "Iron Ingot", 30 }
                        },
                        new()
                        {
                            { "Iron Plate", 20 }
                        },
                        "Constructor")
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
                    new Recipe(
                        new()
                        {
                            { "Iron Ingot", 15 }
                        },
                        new()
                        {
                            { "Iron Rod", 15 }
                        },
                        "Constructor")
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

        public static Item CopperIngot()
        {
            return new Item(1, "Copper Ingot",
                "Copper_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Ore", 30 }
                        },
                        new()
                        {
                            { "Copper Ingot",30 }
                        },
                        "Smelter")
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
                    new Recipe(
                        new()
                        {
                            { "Copper Ingot", 15 }
                        },
                        new()
                        {
                            { "Wire", 30 }
                        },
                        "Constructor")
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
                    new Recipe(
                        new()
                        {
                            { "Wire", 60 }
                        },
                        new()
                        {
                            { "Cable", 30 }
                        },
                        "Constructor")
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

        public static Item Concrete()
        {
            return new Item(1, "Concrete",
                "Concrete.webp",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Limestone", 45 }
                        },
                        new()
                        {
                            { "Concrete", 15 }
                        },
                        "Constructor")
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
                    new Recipe(
                        new()
                        {
                            { "Iron Rod", 10 }
                        },
                        new()
                        {
                            { "Screw", 40 }
                        },
                        "Constructor")
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
                    new Recipe(
                        new()
                        {
                            { "Iron Plate", 30 },
                            { "Screw", 60 }
                        },
                        new()
                        {
                            { "Reinforced Iron Plate", 5 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Wood()
        {
            return new Item(1, "Wood",
                "Wood_256.png",
                ItemType.Item,
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
            return new Item(1, "Leaves",
                "Leaves_256.png",
                ItemType.Item,
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
            return new Item(1, "Alien Protein",
                "AlienProtein_256.png",
                ItemType.Item,
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
            return new Item(1, "Mycelia",
                "Mycelia_256.png",
                ItemType.Item,
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

        public static Item Biomass()
        {
            return new Item(1, "Biomass",
                "Biomass256.png",
                ItemType.Item,
                ResearchType.Tier1)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Leaves", 120 }
                        },
                        new()
                        {
                            { "Biomass", 60 }
                        },
                        "Constructor"),
                    new Recipe(
                        new()
                        {
                            { "Wood", 60 }
                        },
                        new()
                        {
                            { "Biomass", 300 }
                        },
                        "Constructor"),
                    new Recipe(
                        new()
                        {
                            { "Alien Protein", 15 }
                        },
                        new()
                        {
                            { "Biomass", 1500 }
                        },
                        "Constructor"),
                    new Recipe(
                        new()
                        {
                            { "Mycelia", 15 }
                        },
                        new()
                        {
                            { "Biomass", 150 }
                        },
                        "Constructor")
                }
            };
        }


    }
}
