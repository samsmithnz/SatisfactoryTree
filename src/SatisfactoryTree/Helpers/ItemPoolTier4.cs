using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;
//using MethodType = SatisfactoryTree.Models.ManufactoringMethodType;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier4
    {
        //Level 4
        public static Item EncasedIndustrialBeam()
        {
            return new Item(4, "Encased Industrial Beam",
                "Encased_Industrial_Beam.webp",
                ItemType.Item,
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
                        BuildingType.Assembler)
                }
            };
        }

        public static Item Stator()
        {
            return new Item(4, "Stator",
                "Stator.webp",
                ItemType.Item,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(12,
                        5,
                        new()
                        {
                            { "Steel Pipe", 3 },
                            { "Wire", 8 }
                        },
                        new()
                        {
                            { "Stator", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item Motor()
        {
            return new Item(4, "Motor",
                "Motor.webp",
                ItemType.Item,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(12,
                        5,
                        new()
                        {
                            { "Rotor", 2 },
                            { "Stator", 2 }
                        },
                        new()
                        {
                            { "Motor", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item AutomatedWiring()
        {
            return new Item(4, "Automated Wiring",
                "Automated_Wiring.webp",
                ItemType.Item,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(24,
                        2.5m,
                        new()
                        {
                            { "Stator", 2.5m },
                            { "Cable", 20 }
                        },
                        new()
                        {
                            { "Automated Wiring", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item HeavyModularFrame()
        {
            return new Item(4, "Heavy Modular Frame",
                "Heavy_Modular_Frame.webp",
                ItemType.Item,
                ResearchType.Tier4)
            {
                Recipes =
                {
                    new Recipe(30,
                        2,
                        new()
                        {
                            { "Modular Frame", 5 },
                            { "Steel Pipe", 15 },
                            { "Encased Industrial Beam", 5 },
                            { "Screw", 100 }
                        },
                        new()
                        {
                            { "Heavy Modular Frame", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }





    }
}
