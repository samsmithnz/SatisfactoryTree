﻿using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier9
    {
        public static Item EncasedPlutoniumCell()
        {
            return new Item(9, "Encased Plutonium Cell",
                "EncasedPlutoniumCell_256.png",
                ItemType.Production,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Plutonium Pellet", 10 },
                            { "Concrete", 20 }
                        },
                        new()
                        {
                            { "Encased Plutonium Cell", 1 }
                        },
                        "Assembler")
                }
            };
        }

    }
}
