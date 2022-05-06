using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier5
    {
        //Level 5

        public static Item CircuitBoard()
        {
            return new Item(5, "Circuit Board",
                "Circuit_Board.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(8,
                        7.5m,
                        new()
                        {
                            { "Copper Sheet", 2 },
                            { "Plastic", 4 }
                        },
                        new()
                        {
                            { "Circuit Board", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item CrudeOil()
        {
            return new Item(5, "Crude Oil",
                "Crude_Oil.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(1,
                        120,
                        new(),
                        new()
                        {
                            { "Crude Oil", 2 }
                        },
                        BuildingType.OilExtractor)
                }
            };
        }

        public static Item Plastic()
        {
            return new Item(5, "Plastic",
                "Plastic.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Crude Oil", 3 }
                        },
                        new()
                        {
                            { "Plastic", 2 },
                            { "Heavy Oil Residue", 1 }
                        },
                        BuildingType.Refinery)
                }
            };
        }

        public static Item Rubber()
        {
            return new Item(5, "Rubber",
                "Rubber.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Crude Oil", 3 }
                        },
                        new()
                        {
                            { "Rubber", 2 },
                            { "Heavy Oil Residue", 2 }
                        },
                        BuildingType.Refinery)
                }
            };
        }

        public static Item HeavyOilResidue()
        {
            return new Item(5, "Heavy Oil Residue",
                "Heavy_Oil_Residue.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Crude Oil", 3 }
                        },
                        new()
                        {
                            { "Plastic", 2 },
                            { "Heavy Oil Residue", 1 }
                        },
                        BuildingType.Refinery),
                    new Recipe(6,
                        20,
                        new()
                        {
                            { "Crude Oil", 3 }
                        },
                        new()
                        {
                            { "Rubber", 2 },
                            { "Heavy Oil Residue", 2 }
                        },
                        BuildingType.Refinery)
                }
            };
        }

        public static Item Fuel()
        {
            return new Item(5, "Fuel",
                "Fuel.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    //new Recipe(6,
                    //    40,
                    //    new()
                    //    {
                    //        { "Crude Oil", 6 }
                    //    },
                    //    new()
                    //    {
                    //        { "Fuel", 4 },
                    //        { "Polymer Resin", 3 }
                    //    },
                    //    BuildingType.Refinery),
                    new Recipe(6,
                        40,
                        new()
                        {
                            { "Heavy Oil Residue", 6 }
                        },
                        new()
                        {
                            { "Fuel", 4 }
                        },
                        BuildingType.Refinery)
                }
            };
        }

        public static Item PetroleumCoke()
        {
            return new Item(5, "Petroleum Coke",
                "Petroleum_Coke.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(6,
                        120,
                        new()
                        {
                            { "Heavy Oil Residue", 4 }
                        },
                        new()
                        {
                            { "Petroleum Coke", 12 }
                        },
                        BuildingType.Refinery)
                }
            };
        }


    }
}
