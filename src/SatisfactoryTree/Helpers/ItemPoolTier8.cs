using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier8
    {
        public static Item ThermalPropulsionRocket()
        {
            return new Item(8, "Thermal Propulsion Rocket",
                "Thermal_Propulsion_Rocket.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Modular Engine", 2.5M },
                            { "Turbo Motor", 1 },
                            { "Cooling System", 3 },
                            { "Fused Modular Frame", 1 }
                        },
                        new()
                        {
                            { "Thermal Propulsion Rocket", 1 }
                        },
                        "Manufacturer")
                }
            };
        }

        public static Item PlutoniumPellet()
        {
            return new Item(8, "Plutonium Pellet",
                "Plutonium_Pellet.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Non-fissile Uranium", 100 },
                            { "Uranium Waste", 25 }
                        },
                        new()
                        {
                            { "Plutonium Pellet", 30 }
                        },
                        "Assembler")
                }
            };
        }

        public static Item NuclearPasta()
        {
            return new Item(8, "Nuclear Pasta",
                "Nuclear_Pasta.webp",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Powder", 100 },
                            { "Pressure Conversion Cube", 0.5M }
                        },
                        new()
                        {
                            { "Nuclear Pasta", 0.5M }
                        },
                        "Particle Accelerator")
                }
            };
        }

    }
}
