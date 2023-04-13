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
                    new Recipe(6,
                        10,
                        new()
                        {
                            { "Copper Ingot", 2 }
                        },
                        new()
                        {
                            { "Copper Sheet", 1 }
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
                    new Recipe(15,
                        4,
                        new()
                        {
                            { "Iron Rod", 5 },
                            { "Screw", 25 }
                        },
                        new()
                        {
                            { "Rotor", 1 }
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
                    new Recipe(60,
                        2,
                        new()
                        {
                            { "Reinforced Iron Plate", 3 },
                            { "Iron Rod", 12 }
                        },
                        new()
                        {
                            { "Modular Frame", 1 }
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
                    new Recipe(30,
                        2,
                        new()
                        {
                            { "Reinforced Iron Plate", 1 },
                            { "Rotor", 1 }
                        },
                        new()
                        {
                            { "Smart Plating", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

    }
}
