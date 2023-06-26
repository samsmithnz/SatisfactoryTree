using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier7
    {
        //Level 7
        public static Item Bauxite()
        {
            return new Item(7, "Bauxite",
                "Bauxite.webp",
                ItemType.Item,
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

        public static Item AluminaSolution()
        {
            return new Item(7, "Alumina Solution",
                "Alumina_Solution.webp",
                ItemType.Item,
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

        public static Item AluminumScrap()
        {
            return new Item(7, "Aluminum Scrap",
                "Aluminum_Scrap.webp",
                ItemType.Item,
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

        public static Item AluminumIngot()
        {
            return new Item(7, "Aluminum Ingot",
                "Aluminum_Ingot.webp",
                ItemType.Item,
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

        public static Item AlcladAluminumSheet()
        {
            return new Item(7, "Alclad Aluminum Sheet",
                "Alclad_Aluminum_Sheet.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Aluminum Ingot", 30 },
                            { "Copper Ingot", 10 }
                        },
                        new()
                        {
                            { "Alclad Aluminum Sheet", 30 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item AluminumCasing()
        {
            return new Item(7, "Aluminum Casing",
                "Aluminum_Casing.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Aluminum Ingot", 90 }
                        },
                        new()
                        {
                            { "Aluminum Casing", 60 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item RawQuartz()
        {
            return new Item(7, "Raw Quartz",
                "Raw_Quartz.webp",
                ItemType.Item,
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

        public static Item QuartzCrystal()
        {
            return new Item(7, "Quartz Crystal",
                "Quartz_Crystal.webp",
                ItemType.Item,
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
            return new Item(7, "Silica",
                "Silica.webp",
                ItemType.Item,
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

        public static Item CrystalOscillator()
        {
            return new Item(7, "Crystal Oscillator",
                "Crystal_Oscillator.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Quartz Crystal", 18 },
                            { "Cable", 14 },
                            { "Reinforced Iron Plate", 2.5M }
                        },
                        new()
                        {
                            { "Crystal Oscillator", 1 }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item RadioControlUnit()
        {
            return new Item(7, "Radio Control Unit",
                "Radio_Control_Unit.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Aluminum Casing", 40 },
                            { "Crystal Oscillator", 1.25M },
                            { "Computer", 1.25M }
                        },
                        new()
                        {
                            { "Radio Control Unit", 2.5M }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item Sulfur()
        {
            return new Item(7, "Sulfur",
                "Sulfur.webp",
                ItemType.Item,
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

        public static Item SulfuricAcid()
        {
            return new Item(7, "Sulfuric Acid",
                "Sulfuric_Acid.webp",
                ItemType.Item,
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

        public static Item Battery()
        {
            return new Item(7, "Battery",
                "Battery.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Sulfuric Acid", 60 },
                            { "Alumina Solution", 40 },
                            { "Aluminum Casing", 20 }
                        },
                        new()
                        {
                            { "Battery", 20 },
                            { "Water", 40 }
                        },
                        "Blender",
                        true,
                        "Battery")
                }
            };
        }

        public static Item AILimiter()
        {
            return new Item(7, "AI Limiter",
                "AI_Limiter.webp",
                ItemType.Item,
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

        public static Item Supercomputer()
        {
            return new Item(7, "Supercomputer",
                "Supercomputer.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Computer", 3.75M },
                            { "AI Limiter", 3.75M },
                            { "High-Speed Connector", 5.63M },
                            { "Plastic", 52.5M }
                        },
                        new()
                        {
                            { "Supercomputer", 1.88M }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item AssemblyDirectorSystem()
        {
            return new Item(7, "Assembly Director System",
                "Assembly_Director_System.webp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Adaptive Control Unit", 1.5M },
                            { "Supercomputer", 0.75M }
                        },
                        new()
                        {
                            { "Assembly Director System", 0.75M }
                        },
                        "Assembler")
                }
            };
        }



    }
}
