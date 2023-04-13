using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;
//using MethodType = SatisfactoryTree.Models.ManufactoringMethodType;

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
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Bauxite", 1 }
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
                    new Recipe(6,
                        120,
                        new()
                        {
                            { "Bauxite", 12 },
                            { "Water", 18 }
                        },
                        new()
                        {
                            { "Alumina Solution", 12 },
                            { "Silica", 5 }
                        },
                        BuildingType.Refinery)
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
                    new Recipe(1,
                        360,
                        new()
                        {
                            { "Alumina Solution", 4 },
                            { "Coal", 2 }
                        },
                        new()
                        {
                            { "Aluminum Scrap", 6 },
                            { "Water", 2 }
                        },
                        BuildingType.Assembler)
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
                    new Recipe(4,
                        60,
                        new()
                        {
                            { "Aluminum Scrap", 6 },
                            { "Silica", 5 }
                        },
                        new()
                        {
                            { "Aluminum Ingot", 4 }
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
                    new Recipe(6,
                        30,
                        new()
                        {
                            { "Aluminum Ingot", 3 },
                            { "Copper Ingot", 1 }
                        },
                        new()
                        {
                            { "Alclad Aluminum Sheet", 3 }
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
                    new Recipe(2,
                        60,
                        new()
                        {
                            { "Aluminum Ingot", 3 }
                        },
                        new()
                        {
                            { "Aluminum Casing", 2 }
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
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Raw Quartz", 1 }
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
                    new Recipe(8,
                        22.5m,
                        new()
                        {
                            { "Raw Quartz", 5 }
                        },
                        new()
                        {
                            { "Quartz Crystal", 3 }
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
                    new Recipe(8,
                        37.5m,
                        new()
                        {
                            { "Raw Quartz", 3 }
                        },
                        new()
                        {
                            { "Silica", 5 }
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
                        60,
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
