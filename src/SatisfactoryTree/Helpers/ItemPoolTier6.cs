using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier6
    {
        public static Item AdaptiveControlUnit()
        {
            return new Item(6, "Adaptive Control Unit",
                "Adaptive_Control_Unit.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Automated Wiring", 7.5M },
                            { "Circuit Board", 5 },
                            { "Heavy Modular Frame", 1 },
                            { "Computer", 1 }
                        },
                        new()
                        {
                            { "Adaptive Control Unit", 2 }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item MagneticFieldGenerator()
        {
            return new Item(6, "Magnetic Field Generator",
                "Magnetic_Field_Generator.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Versatile Framework", 2.5M },
                            { "Electromagnetic Control Rod", 1 },
                            { "Battery", 5 }
                        },
                        new()
                        {
                            { "Magnetic Field Generator", 1 }
                        },
                        "Manufacturer")
                }
            };
        }
        public static Item CoolingSystem()
        {
            return new Item(6, "Cooling System",
                "Cooling_System.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Heat Sink", 12 },
                            { "Rubber", 12 },
                            { "Water", 30 },
                            { "Nitrogen Gas", 150 }
                        },
                        new()
                        {
                            { "Cooling System", 6 }
                        },
                        "Blender")
                }
            };
        }

        public static Item FusedModularFrame()
        {
            return new Item(6, "Fused Modular Frame",
                "Fused_Modular_Frame.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Heavy Modular Frame", 1.5M },
                            { "Aluminum Casing", 75 },
                            { "Nitrogen Gas", 37.5M }
                        },
                        new()
                        {
                            { "Fused Modular Frame", 1.5M }
                        },
                        "Blender")
                }
            };
        }

        public static Item UraniumWaste()
        {
            return new Item(6, "Uranium Waste",
                "Uranium_Waste.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium Fuel Rod", 0.2M },
                            { "Water", 240 }
                        },
                        new()
                        {
                            { "Uranium Waste", 10 }
                        },
                        "Nuclear Power Plant")
                }
            };
        }

        public static Item NuclearPowerGeneration()
        {
            return new Item(6, "Nuclear Power",
                "LightningBolt.png",
                ItemType.PowerGeneration,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium Fuel Rod", 0.2M },
                            { "Water", 240 }
                        },
                        new()
                        {
                            { "Nuclear Power", 2500M }
                        },
                        "Nuclear Power Plant")
                }
            };
        }
    }
}
