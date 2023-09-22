using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public class AllItemGroups
    {
        List<ItemGroup> ItemGroups { get; set; }
        public AllItemGroups()
        {
            List<ItemGroup> itemGroups = new();


            ItemGroups = itemGroups;

        }
    }
}
