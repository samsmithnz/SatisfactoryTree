using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier3
    {
        //Level 3
        public static Item Coal()
        {
            return new Item(3, "Coal",
                "Coal.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Coal", 60 }
                        },
                        "Mining Machine Mk1")
                }
            };
        }

        public static Item SteelIngot()
        {
            return new Item(3, "Steel Ingot",
                "Steel_Ingot.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Iron Ore", 45 },
                            { "Coal", 45 }
                        },
                        new()
                        {
                            { "Steel Ingot", 45 }
                        },
                        "Foundry")
                }
            };
        }

        public static Item SteelBeam()
        {
            return new Item(3, "Steel Beam",
                "Steel_Beam.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Ingot", 60 }
                        },
                        new()
                        {
                            { "Steel Beam", 15 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item SteelPipe()
        {
            return new Item(3, "Steel Pipe",
                "Steel_Pipe.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Steel Ingot", 30 }
                        },
                        new()
                        {
                            { "Steel Pipe", 20 }
                        },
                        "Constructor")
                }
            };
        }

        public static Item VersatileFramework()
        {
            return new Item(3, "Versatile Framework",
                "Versatile_Framework.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Modular Frame", 2.5m },
                            { "Steel Beam", 30 }
                        },
                        new()
                        {
                            { "Versatile Framework", 5 }
                        },
                        "Assembler")
                }
            };
        }


        public static Item Water()
        {
            return new Item(3, "Water",
                "Water.webp",
                ItemType.Item,
                ResearchType.Tier3)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Water", 120 }
                        },
                        "Water Extractor")
                }
            };
        }


    }
}
