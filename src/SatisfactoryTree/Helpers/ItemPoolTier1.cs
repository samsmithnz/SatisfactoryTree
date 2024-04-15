using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier1
    {
        public static Item IronIngot()
        {
            return new Item(1, "Iron Ingot",
                "IronIngot_256.png",
                ItemType.Production,
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

        public static Item CopperIngot()
        {
            return new Item(1, "Copper Ingot",
                "CopperIngot_256.png",
                ItemType.Production,
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
        

        public static Item Concrete()
        {
            return new Item(1, "Concrete",
                "Concrete_256.png",
                ItemType.Production,
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

        public static Item Biomass()
        {
            return new Item(1, "Biomass",
                "Biomass256.png",
                ItemType.Production,
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

        public static Item SteelIngot()
        {
            return new Item(2, "Steel Ingot",
                "SteelIngot_256.png",
                ItemType.Production,
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
                        "Foundry")
                }
            };
        }


        public static Item Plastic()
        {
            return new Item(1, "Plastic",
                "Plastic_256.png",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Crude Oil", 30 }
                        },
                        new()
                        {
                            { "Plastic", 20 },
                            { "Heavy Oil Residue", 10 }
                        },
                        "Refinery",
                        true,
                        "Plastic")
                }
            };
        }

        public static Item Rubber()
        {
            return new Item(1, "Rubber",
                "Rubber_256.png",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Crude Oil", 30 }
                        },
                        new()
                        {
                            { "Rubber", 20 },
                            { "Heavy Oil Residue", 20 }
                        },
                        "Refinery",
                        true,
                        "Rubber")
                }
            };
        }
        public static Item CateriumIngot()
        {
            return new Item(1, "Caterium Ingot",
                "CateriumIngot_256.png",
                ItemType.Production,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Caterium Ore", 45 }
                        },
                        new()
                        {
                            { "Caterium Ingot", 15 }
                        },
                        "Smelter")
                }
            };
        }

        public static Item AluminaSolution()
        {
            return new Item(1, "Alumina Solution",
                "AluminaSolution_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Bauxite", 120 },
                            { "Water", 180 }
                        },
                        new()
                        {
                            { "Alumina Solution", 120 },
                            { "Silica", 50 }
                        },
                        "Refinery",
                        true,
                        "Alumina Solution")
                }
            };
        }

        public static Item QuartzCrystal()
        {
            return new Item(1, "Quartz Crystal",
                "QuartzCrystal_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Raw Quartz", 37.5M }
                        },
                        new()
                        {
                            { "Quartz Crystal", 22.5M }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Silica()
        {
            return new Item(1, "Silica",
                "Silica_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Raw Quartz", 22.5M }
                        },
                        new()
                        {
                            { "Silica", 37.5M }
                        },
                        "Constructor")
                }
            };
        }

        public static Item SulfuricAcid()
        {
            return new Item(1, "Sulfuric Acid",
                "SulfuricAcid_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Sulfur", 50 },
                            { "Water", 50 }
                        },
                        new()
                        {
                            { "Sulfuric Acid", 50 }
                        },
                        "Refinery")
                }
            };
        }


    }
}
