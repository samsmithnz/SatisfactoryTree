using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTierB
    {
        public static Item PlutoniumWaste()
        {
            return new Item(11, "Plutonium Waste",
                "Plutonium_Waste_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Plutonium Fuel Rod", 0.1M },
                            { "Water", 240 }
                        },
                        new()
                        {
                            { "Plutonium Waste", 1 }
                        },
                        "Nuclear Power Plant")
                }
            };
        }



    }
}
