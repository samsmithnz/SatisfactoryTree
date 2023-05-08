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
                    new Recipe(
                        new()
                        {
                            { "Steel Pipe", 15 },
                            { "Wire", 40 }
                        },
                        new()
                        {
                            { "Stator", 5 }
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
                        BuildingType.Manufacturer)
                }
            };
        }

    }
}
