using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTierA
    {
        public static Item PlutoniumFuelRod()
        {
            return new Item(10, "Plutonium Fuel Rod",
                "Plutonium_Fuel_Rod_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Encased Plutonium Cell", 7.5M },
                            { "Steel Beam", 4.5M },
                            { "Electromagnetic Control Rod", 1.5M },
                            { "Heat Sink", 2.5M }
                        },
                        new()
                        {
                            { "Plutonium Fuel Rod", 0.25M }
                        },
                        "Manufacturer")
                }
            };
        }


    }
}
