using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier8
    {
        //Level 8

        public static Item Uranium()
        {
            return new Item(8, "Uranium",
                "Uranium.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Uranium", 1 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item EncasedUraniumCell()
        {
            return new Item(8, "Encased Uranium Cell",
                "Encased_Uranium_Cell.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(12,
                        25,
                        new()
                        {
                            { "Uranium", 10 },
                            { "Concrete", 3 },
                            { "Sulfuric Acid", 8 }
                        },
                        new()
                        {
                            { "Encased Uranium Cell", 5 },
                            { "Sulfuric Acid", 2 }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item ElectromagneticControlRod()
        {
            return new Item(8, "Electromagnetic Control Rod",
                "Electromagnetic_Control_Rod.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(30,
                        4,
                        new()
                        {
                            { "Stator", 3 },
                            { "AI Limiter", 2 }
                        },
                        new()
                        {
                            { "Electromagnetic Control Rod", 2 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item UraniumFuelRod()
        {
            return new Item(8, "Uranium Fuel Rod",
                "Uranium_Fuel_Rod.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(150,
                        0.4m,
                        new()
                        {
                            { "Encased Uranium Cell", 50 },
                            { "Encased Industrial Beam", 3 },
                            { "Electromagnetic Control Rod", 5 }
                        },
                        new()
                        {
                            { "Uranium Fuel Rod", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item MagneticFieldGenerator()
        {
            return new Item(8, "Magnetic Field Generator",
                "Magnetic_Field_Generator.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(120,
                        1,
                        new()
                        {
                            { "Versatile Framework", 5 },
                            { "Electromagnetic Control Rod", 2 },
                            { "Battery", 10 }
                        },
                        new()
                        {
                            { "Magnetic Field Generator", 2 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item NitrogenGas()
        {
            return new Item(8, "Nitrogen Gas",
                "Nitrogen_Gas.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Nitrogen Gas", 1 }
                        },
                        BuildingType.ResourceWellExtractor)
                }
            };
        }

        public static Item HeatSink()
        {
            return new Item(8, "Heat Sink",
                "Heat_Sink.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(8,
                        7.5m,
                        new()
                        {
                            { "Alclad Aluminum Sheet", 5 },
                            { "Copper Sheet", 3 }
                        },
                        new()
                        {
                            { "Heat Sink", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item CoolingSystem()
        {
            return new Item(8, "Cooling System",
                "Cooling_System.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(10,
                        6,
                        new()
                        {
                            { "Heat Sink", 2 },
                            { "Rubber", 2 },
                            { "Water", 5 },
                            { "Nitrogen Gas", 25 }
                        },
                        new()
                        {
                            { "Cooling System", 4 }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item FusedModularFrame()
        {
            return new Item(8, "Fused Modular Frame",
                "Fused_Modular_Frame.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(40,
                        1.5m,
                        new()
                        {
                            { "Heavy Modular Frame", 1 },
                            { "Aluminum Casing", 50 },
                            { "Nitrogen Gas", 25 }
                        },
                        new()
                        {
                            { "Fused Modular Frame", 99 }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item CircuitBoard()
        {
            return new Item(8, "CircuitBoard",
                "Circuit_Boardwebp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(99,
                        99,
                        new()
                        {
                            { "CopperSheet", 99 },
                            { "Plastic", 99 }
                        },
                        new()
                        {
                            { "CircuitBoard", 99 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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

        //public static Item CircuitBoard()
        //{
        //    return new Item(8, "CircuitBoard",
        //        "Circuit_Boardwebp",
        //        ItemType.Item,
        //        ResearchType.Tier8)
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
