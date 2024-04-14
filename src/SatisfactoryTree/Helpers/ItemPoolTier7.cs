using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier7
    {

        public static Item AssemblyDirectorSystem()
        {
            return new Item(7, "Assembly Director System",
                "Assembly_Director_System_256.png",
                ItemType.Production,
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

        public static Item TurboMotor()
        {
            return new Item(7, "Turbo Motor",
                "Turbo_Motor_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Cooling System", 7.5M },
                            { "Radio Control Unit", 3.75M },
                            { "Motor", 7.5M },
                            { "Rubber", 45 }
                        },
                        new()
                        {
                            { "Turbo Motor", 1.88M }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item NonfissileUranium()
        {
            return new Item(7, "Non-fissile Uranium",
                "Non-fissile_Uranium_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium Waste", 37.5M },
                            { "Silica", 25 },
                            { "Nitric Acid", 15 },
                            { "Sulfuric Acid", 15 }
                        },
                        new()
                        {
                            { "Non-fissile Uranium", 50 },
                            { "Water", 15 }
                        },
                        "Blender",
                        true,
                        "Non-fissile Uranium")
                }
            };
        }

        public static Item PressureConversionCube()
        {
            return new Item(7, "Pressure Conversion Cube",
                "Pressure_Conversion_Cube_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Fused Modular Frame", 1 },
                            { "Radio Control Unit", 2 }
                        },
                        new()
                        {
                            { "Pressure Conversion Cube", 1 }
                        },
                        "Assembler")
                }
            };
        }


    }
}
