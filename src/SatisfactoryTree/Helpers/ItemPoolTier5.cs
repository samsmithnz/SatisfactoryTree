using SatisfactoryTree.Models;


namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier5
    {
                public static Item VersatileFramework()
        {
            return new Item(5, "Versatile Framework",
                "Versatile_Framework.webp",
                ItemType.Production,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Modular Frame", 2.5m },
                            { "Steel Beam", 30 }
                        },
                        new()
                        {
                            { "Versatile Framework", 5 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item HeavyModularFrame()
        {
            return new Item(5, "Heavy Modular Frame",
                "Heavy_Modular_Frame.webp",
                ItemType.Production,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Modular Frame", 10 },
                            { "Steel Pipe", 30 },
                            { "Encased Industrial Beam", 10 },
                            { "Screw", 200 }
                        },
                        new()
                        {
                            { "Heavy Modular Frame", 2 }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item ModularEngine()
        {
            return new Item(5, "Modular Engine",
                "Modular_Engine.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Motor", 2 },
                            { "Rubber", 15 },
                            { "Smart Plating", 2 },
                        },
                        new()
                        {
                            { "Modular Engine", 1 }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item Battery()
        {
            return new Item(5, "Battery",
                "Battery.webp",
                ItemType.Production,
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
        public static Item RadioControlUnit()
        {
            return new Item(5, "Radio Control Unit",
                "Radio_Control_Unit.webp",
                ItemType.Production,
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

        public static Item Supercomputer()
        {
            return new Item(5, "Supercomputer",
                "Supercomputer.webp",
                ItemType.Production,
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

        public static Item UraniumFuelRod()
        {
            return new Item(5, "Uranium Fuel Rod",
                "Uranium_Fuel_Rod.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Encased Uranium Cell", 20 },
                            { "Encased Industrial Beam", 1.2M },
                            { "Electromagnetic Control Rod", 2 }
                        },
                        new()
                        {
                            { "Uranium Fuel Rod", 0.4M }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item HeatSink()
        {
            return new Item(5, "Heat Sink",
                "Heat_Sink.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Alclad Aluminum Sheet", 37.5M },
                            { "Copper Sheet", 3 }
                        },
                        new()
                        {
                            { "Heat Sink", 7.5M }
                        },
                        "Assembler")
                }
            };
        }

    }
}
