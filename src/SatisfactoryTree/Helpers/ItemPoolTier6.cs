using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier6
    {
        //Level 6
        public static Item CateriumOre()
        {
            return new Item(6, "Caterium Ore",
                "Caterium_Ore.webp",
                ItemType.Item,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Caterium Ore", 60 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item CateriumIngot()
        {
            return new Item(6, "Caterium Ingot",
                "Caterium_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Caterium Ore", 45 }
                        },
                        new()
                        {
                            { "Caterium Ingot", 15 }
                        },
                        BuildingType.Smelter)
                }
            };
        }

        public static Item Quickwire()
        {
            return new Item(6, "Quickwire",
                "Quickwire.webp",
                ItemType.Item,
                ResearchType.Tier6)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Caterium Ingot", 12 }
                        },
                        new()
                        {
                            { "Quickwire", 60 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item HighSpeedConnector()
        {
            return new Item(6, "High-Speed Connector",
                "High-Speed_Connector.webp",
                ItemType.Item,
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
                        BuildingType.Manufacturer)
                }
            };
        }


    }
}
