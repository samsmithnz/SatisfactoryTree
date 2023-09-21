using SatisfactoryTree.Models;


namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier4
    {

        public static Item SmartPlating()
        {
            return new Item(4, "Smart Plating",
                "Smart_Plating.webp",
                ItemType.Production,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Reinforced Iron Plate", 2 },
                            { "Rotor", 2 }
                        },
                        new()
                        {
                            { "Smart Plating", 2 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item ModularFrame()
        {
            return new Item(4, "Modular Frame",
                "Modular_Frame.webp",
                ItemType.Production,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Reinforced Iron Plate", 3 },
                            { "Iron Rod", 12 }
                        },
                        new()
                        {
                            { "Modular Frame", 2 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Motor()
        {
            return new Item(4, "Motor",
                "Motor.webp",
                ItemType.Production,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Rotor", 10 },
                            { "Stator", 10 }
                        },
                        new()
                        {
                            { "Motor", 5 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item AutomatedWiring()
        {
            return new Item(4, "Automated Wiring",
                "Automated_Wiring.webp",
                ItemType.Production,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Stator", 2.5m },
                            { "Cable", 20 }
                        },
                        new()
                        {
                            { "Automated Wiring", 2.5M }
                        },
                        "Assembler")
                }
            };
        }

        public static Item Computer()
        {
            return new Item(4, "Computer",
                "Computer.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Circuit Board", 25 },
                            { "Cable", 22.5M },
                            { "Plastic", 45 },
                            { "Screw", 130 }
                        },
                        new()
                        {
                            { "Computer", 2.5M }
                        },
                        "Manufacturer")
                }
            };
        }
        public static Item HighSpeedConnector()
        {
            return new Item(4, "High-Speed Connector",
                "High-Speed_Connector.webp",
                ItemType.Production,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Quickwire", 210 },
                            { "Cable", 37.5M },
                            { "Circuit Board", 3.75M },
                        },
                        new()
                        {
                            { "High-Speed Connector", 3.75M }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item FuelPowerGeneration()
        {
            return new Item(4, "Fuel Power",
                "LightningBolt.png",
                ItemType.PowerGeneration,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Fuel", 12 }
                        },
                        new()
                        {
                            { "Fuel Power", 150M }
                        },
                        "Fuel Generator")
                }
            };
        }

        public static Item AlcladAluminumSheet()
        {
            return new Item(4, "Alclad Aluminum Sheet",
                "Alclad_Aluminum_Sheet.webp",
                ItemType.Production,
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
            return new Item(4, "Aluminum Casing",
                "Aluminum_Casing.webp",
                ItemType.Production,
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
        public static Item CrystalOscillator()
        {
            return new Item(4, "Crystal Oscillator",
                "Crystal_Oscillator.webp",
                ItemType.Production,
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
        public static Item ElectromagneticControlRod()
        {
            return new Item(4, "Electromagnetic Control Rod",
                "Electromagnetic_Control_Rod.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Stator", 6 },
                            { "AI Limiter", 4 }
                        },
                        new()
                        {
                            { "Electromagnetic Control Rod", 4 }
                        },
                        "Assembler")
                }
            };
        }

    }
}
