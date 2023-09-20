using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier7
    {

        public static Item AssemblyDirectorSystem()
        {
            return new Item(7, "Assembly Director System",
                "Assembly_Director_System.webp",
                ItemType.Production,
                ResearchType.Tier7)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Adaptive Control Unit", 1.5M },
                            { "Supercomputer", 0.75M }
                        },
                        new()
                        {
                            { "Assembly Director System", 0.75M }
                        },
                        "Assembler")
                }
            };
        }



    }
}
