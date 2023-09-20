using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier6
    {
        public static Item AdaptiveControlUnit()
        {
            return new Item(6, "Adaptive Control Unit",
                "Adaptive_Control_Unit.webp",
                ItemType.Production,
                ResearchType.Tier5)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Automated Wiring", 7.5M },
                            { "Circuit Board", 5 },
                            { "Heavy Modular Frame", 1 },
                            { "Computer", 1 }
                        },
                        new()
                        {
                            { "Adaptive Control Unit", 2 }
                        },
                        "Manufacturer")
                }
            };
        }
    }
}
