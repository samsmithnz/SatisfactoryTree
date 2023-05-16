using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;

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
                        BuildingType.MiningMachine)
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
                        BuildingType.Refinery,
                        ManufactoringMethodType.Manufactured,
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
                        BuildingType.Assembler,
                        ManufactoringMethodType.Manufactured,
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
                        BuildingType.Foundry)
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
                        BuildingType.Assembler)
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
                        BuildingType.Constructor)
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
                        BuildingType.Assembler)
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
                        BuildingType.Assembler)
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
                        BuildingType.Constructor)
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
                    new Recipe(120,
                        1,
                        new()
                        {
                            { "Quartz Crystal", 36 },
                            { "Cable", 28 },
                            { "Reinforced Iron Plate", 5 }
                        },
                        new()
                        {
                            { "Crystal Oscillator", 2 }
                        },
                        BuildingType.Manufacturer)
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
                    new Recipe(48,
                        2.5m,
                        new()
                        {
                            { "Aluminum Casing", 32 },
                            { "Crystal Oscillator", 1.25m },
                            { "Computer", 1 }
                        },
                        new()
                        {
                            { "Radio Control Unit", 2 }
                        },
                        BuildingType.Manufacturer)
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
                    new Recipe(1,
                        60, // Assuming normal node, MK1 miner
                        new(),
                        new()
                        {
                            { "Sulfur", 1 }
                        },
                        BuildingType.MiningMachine)
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
                    new Recipe(6,
                        50,
                        new()
                        {
                            { "Sulfur", 5 },
                            { "Water", 5 }
                        },
                        new()
                        {
                            { "Sulfuric Acid", 5 }
                        },
                        BuildingType.Refinery)
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
                    new Recipe(3,
                        20,
                        new()
                        {
                            { "Sulfuric Acid", 2.5m },
                            { "Alumina Solution", 2 },
                            { "Aluminum Casing", 1 }
                        },
                        new()
                        {
                            { "Battery", 1 },
                            { "Water", 1.5m }
                        },
                        BuildingType.Blender)
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
                    new Recipe(12,
                        5,
                        new()
                        {
                            { "Copper Sheet", 5 },
                            { "Quickwire", 20 }
                        },
                        new()
                        {
                            { "AI Limiter", 1 }
                        },
                        BuildingType.Assembler)
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
                    new Recipe(32,
                        1.875m,
                        new()
                        {
                            { "Computer", 2 },
                            { "AI Limiter", 2 },
                            { "High-Speed Connector", 3 },
                            { "Plastic", 28 }
                        },
                        new()
                        {
                            { "Supercomputer", 1 }
                        },
                        BuildingType.Manufacturer)
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
                    new Recipe(80,
                        0.75m,
                        new()
                        {
                            { "Adaptive Control Unit", 1.5m },
                            { "Supercomputer", 0.75m }
                        },
                        new()
                        {
                            { "Assembly Director System", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        //public static Item CircuitBoard()
        //{
        //    return new Item(7, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier7)
        //    {
        //        Recipes =
        //        {
        //            new Recipe(99,
        //                99,
        //                new()
        //                {
        //                    { "CopperSheet", 99 },
        //                    { "Plastic", 99 }
        //                },
        //                new()
        //                {
        //                    { "CircuitBoard", 99 }
        //                },
        //                BuildingType.Assembler)
        //        }
        //    };
        //}



    }
}
