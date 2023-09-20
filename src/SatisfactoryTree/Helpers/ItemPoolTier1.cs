using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier1
    {
        public static Item IronIngot()
        {
            return new Item(1, "Iron Ingot",
                "Iron_Ingot.webp",
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
                "Copper_Ingot.webp",
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
                "Concrete.webp",
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
                "Steel_Ingot.webp",
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
                "Plastic.webp",
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
                "Rubber.webp",
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
                "Caterium_Ingot.webp",
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


    }
}
