using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier2
    {

        public static Item IronPlate()
        {
            return new Item(2, "Iron Plate",
                "IronPlate_256.png",
                ItemType.Production,
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
            return new Item(2, "Iron Rod",
                "IronRod_256.png",
                ItemType.Production,
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

        //Special level 2 item since screws are always made with iron rods
        public static Item Screw()
        {
            return new Item(2, "Screw",
                "Screw_256.png",
                ItemType.Production,
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

        public static Item Wire()
        {
            return new Item(2, "Wire",
                "Wire_256.png",
                ItemType.Production,
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

        //Special level 2 item since cable is always made with wire.
        public static Item Cable()
        {
            return new Item(2, "Cable",
                "Cable_256.png",
                ItemType.Production,
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

        public static Item CopperSheet()
        {
            return new Item(2, "Copper Sheet",
                "CopperSheet_256.png",
                ItemType.Production,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Ingot", 20 }
                        },
                        new()
                        {
                            { "Copper Sheet", 10 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item SolidBiofuel()
        {
            return new Item(1, "Solid Biofuel",
                "SolidBiofuel_256.png",
                ItemType.Production,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Biomass", 120 }
                        },
                        new()
                        {
                            { "Solid Biofuel", 60 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item SteelBeam()
        {
            return new Item(2, "Steel Beam",
                "SteelBeam_256.png",
                ItemType.Production,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Ingot", 60 }
                        },
                        new()
                        {
                            { "Steel Beam", 15 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item SteelPipe()
        {
            return new Item(2, "Steel Pipe",
                "SteelPipe_256.png",
                ItemType.Production,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Ingot", 30 }
                        },
                        new()
                        {
                            { "Steel Pipe", 20 }
                        },
                        "Constructor")
                }
            };
        }
        public static Item HeavyOilResidue()
        {
            return new Item(2, "Heavy Oil Residue",
                "HeavyOilResidue_256.png",
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
                        "Plastic"),
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
        public static Item Quickwire()
        {
            return new Item(2, "Quickwire",
                "Quickwire_256.png",
                ItemType.Production,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Caterium Ingot", 12 }
                        },
                        new()
                        {
                            { "Quickwire", 60 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item AluminumScrap()
        {
            return new Item(2, "Aluminum Scrap",
                "AluminumScrap_256.png",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Alumina Solution", 240 },
                            { "Coal", 120 }
                        },
                        new()
                        {
                            { "Aluminum Scrap", 360 },
                            { "Water", 120 }
                        },
                        "Assembler",
                        true,
                        "Aluminum Scrap")
                }
            };
        }

        public static Item EncasedUraniumCell()
        {
            return new Item(2, "Encased Uranium Cell",
                "EncasedUraniumCell_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium", 50 },
                            { "Concrete", 15 },
                            { "Sulfuric Acid", 40 }
                        },
                        new()
                        {
                            { "Encased Uranium Cell", 25 },
                            { "Sulfuric Acid", 10 }
                        },
                        "Blender",
                        true,
                        "Encased Uranium Cell")
                }
            };
        }
        public static Item CopperPowder()
        {
            return new Item(2, "Copper Powder",
                "CopperPowder_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Ingot", 300 }
                        },
                        new()
                        {
                            { "Copper Powder", 50 }
                        },
                        "Constructor")
                }
            };
        }

    }
}
