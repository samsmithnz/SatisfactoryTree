using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;


namespace SatisfactoryTree.Helpers
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
                    new Recipe(
                        new()
                        {
                            { "Copper Sheet", 15 },
                            { "Plastic", 30 }
                        },
                        new()
                        {
                            { "Circuit Board", 7.5M }
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
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Crude Oil", 120 }
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
                    new Recipe(
                        new()
                        {
                            { "Crude Oil", 30 }
                        },
                        new()
                        {
                            { "Plastic", 20 },
                            { "Heavy Oil Residue", 10 }
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
                    new Recipe(
                        new()
                        {
                            { "Crude Oil", 30 }
                        },
                        new()
                        {
                            { "Rubber", 20 },
                            { "Heavy Oil Residue", 20 }
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

        public static Item Computer()
        {
            return new Item(5, "Computer",
                "Computer.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(24,
                        2.5m,
                        new()
                        {
                            { "Circuit Board", 10 },
                            { "Cable", 9 },
                            { "Plastic", 18 },
                            { "Screw", 52 }
                        },
                        new()
                        {
                            { "Computer", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item ModularEngine()
        {
            return new Item(5, "Modular Engine",
                "Modular_Engine.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(60,
                        1,
                        new()
                        {
                            { "Motor", 2 },
                            { "Rubber", 15 },
                            { "Smart Plating", 2 },
                        },
                        new()
                        {
                            { "Modular Engine", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item AdaptiveControlUnit()
        {
            return new Item(5, "Adaptive Control Unit",
                "Adaptive_Control_Unit.webp",
                ItemType.Item,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(120,
                        2,
                        new()
                        {
                            { "Automated Wiring", 15 },
                            { "Circuit Board", 10 },
                            { "Heavy Modular Frame", 2 },
                            { "Computer", 2 }
                        },
                        new()
                        {
                            { "Adaptive Control Unit", 2 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }


    }
}
