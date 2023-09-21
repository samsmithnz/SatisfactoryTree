using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier3
    {
        public static Item ReinforcedIronPlate()
        {
            return new Item(3, "Reinforced Iron Plate",
                "Reinforced_Iron_Plate.webp",
                ItemType.Production,
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

        public static Item Rotor()
        {
            return new Item(3, "Rotor",
                "Rotor.webp",
                ItemType.Production,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Iron Rod", 20 },
                            { "Screw", 100 }
                        },
                        new()
                        {
                            { "Rotor", 4 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item EncasedIndustrialBeam()
        {
            return new Item(3, "Encased Industrial Beam",
                "Encased_Industrial_Beam.webp",
                ItemType.Production,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Beam", 24 },
                            { "Concrete", 30 }
                        },
                        new()
                        {
                            { "Encased Industrial Beam", 6 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Stator()
        {
            return new Item(3, "Stator",
                "Stator.webp",
                ItemType.Production,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Pipe", 15 },
                            { "Wire", 40 }
                        },
                        new()
                        {
                            { "Stator", 5 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item CoalPowerGeneration()
        {
            return new Item(3, "Coal Power",
                "LightningBolt.png",
                ItemType.PowerGeneration,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Coal", 15 },
                            { "Water", 45 }
                        },
                        new()
                        {
                            { "Coal Power" , 75 }
                        },
                        "Coal Generator")
                }
            };
        }

        public static Item SolidBiofuelPowerGeneration()
        {
            return new Item(3, "Solid Biofuel Power",
                "LightningBolt.png",
                ItemType.PowerGeneration,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Solid Biofuel", 4 }
                        },
                        new()
                        {
                            { "Solid Biofuel Power", 30M }
                        },
                        "Biomass Burner")
                }
            };
        }

        public static Item Fuel()
        {
            return new Item(3, "Fuel",
                "Fuel.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    //new Recipe(6,
                    //    40,
                    //    new()
                    //    {
                    //        { "Crude Oil", 6 }
                    //    },
                    //    new()
                    //    {
                    //        { "Fuel", 4 },
                    //        { "Polymer Resin", 3 }
                    //    },
                    //    "Refinery"),
                    new Recipe(
                        new()
                        {
                            { "Heavy Oil Residue", 60 }
                        },
                        new()
                        {
                            { "Fuel", 40 }
                        },
                        "Refinery")
                }
            };
        }

        public static Item PetroleumCoke()
        {
            return new Item(3, "Petroleum Coke",
                "Petroleum_Coke.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Heavy Oil Residue", 40 }
                        },
                        new()
                        {
                            { "Petroleum Coke", 120 }
                        },
                        "Refinery")
                }
            };
        }

        public static Item CircuitBoard()
        {
            return new Item(3, "Circuit Board",
                "Circuit_Board.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Sheet", 15 },
                            { "Plastic", 30 }
                        },
                        new()
                        {
                            { "Circuit Board", 7.5M }
                        },
                        "Assembler")
                }
            };
        }
        public static Item AluminumIngot()
        {
            return new Item(3, "Aluminum Ingot",
                "Aluminum_Ingot.webp",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Aluminum Scrap", 90 },
                            { "Silica", 75 }
                        },
                        new()
                        {
                            { "Aluminum Ingot", 60 }
                        },
                        "Foundry")
                }
            };
        }
        public static Item AILimiter()
        {
            return new Item(3, "AI Limiter",
                "AI_Limiter.webp",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Sheet", 25 },
                            { "Quickwire", 100 }
                        },
                        new()
                        {
                            { "AI Limiter", 5 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item NitricAcid()
        {
            return new Item(3, "Nitric Acid",
                "Nitric_Acid.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Nitrogen Gas", 120 },
                            { "Water", 30 },
                            { "Iron Plate", 10 }
                        },
                        new()
                        {
                            { "Nitric Acid", 30 }
                        },
                        "Blender")
                }
            };
        }


    }
}
