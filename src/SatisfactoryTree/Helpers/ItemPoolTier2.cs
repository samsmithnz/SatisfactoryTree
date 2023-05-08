using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;
//using MethodType = SatisfactoryTree.Models.ManufactoringMethodType;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier2
    {
        //Level 2
        public static Item CopperSheet()
        {
            return new Item(2, "Copper Sheet",
                "Copper_Sheet.webp",
                ItemType.Item,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Copper Ingot", 20 }
                        },
                        new()
                        {
                            { "Copper Sheet", 10 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item Rotor()
        {
            return new Item(2, "Rotor",
                "Rotor.webp",
                ItemType.Item,
                ResearchType.Tier2)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Iron Rod", 20 },
                            { "Screw", 100 }
                        },
                        new()
                        {
                            { "Rotor", 4 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item ModularFrame()
        {
            return new Item(2, "Modular Frame",
                "Modular_Frame.webp",
                ItemType.Item,
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
                        BuildingType.Assembler)
                }
            };
        }

        public static Item SmartPlating()
        {
            return new Item(2, "Smart Plating",
                "Smart_Plating.webp",
                ItemType.Item,
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
                        BuildingType.Assembler)
                }
            };
        }

    }
}
