using SatisfactoryTree.Models;


namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier5
    {


        public static Item HeavyModularFrame()
        {
            return new Item(5, "Heavy Modular Frame",
                "Heavy_Modular_Frame.webp",
                ItemType.Production,
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
                        "Manufacturer")
                }
            };
        }

        public static Item VersatileFramework()
        {
            return new Item(5, "Versatile Framework",
                "Versatile_Framework.webp",
                ItemType.Production,
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

        public static Item ModularEngine()
        {
            return new Item(5, "Modular Engine",
                "Modular_Engine.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
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
                        "Manufacturer")
                }
            };
        }

    }
}
