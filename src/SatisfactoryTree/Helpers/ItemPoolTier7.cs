using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
{
    public static class ItemPoolTier7
    {
        //Level 7
        
        public static Item CircuitBoard()
        {
            return new Item(7, "CircuitBoard",
                "Circuit_Boardwebp",
                ItemType.Item,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(99,
                        99,
                        new()
                        {
                            { "CopperSheet", 99 },
                            { "Plastic", 99 }
                        },
                        new()
                        {
                            { "CircuitBoard", 99 }
                        },
                        BuildingType.Assembler)
                }
            };
        }



    }
}
