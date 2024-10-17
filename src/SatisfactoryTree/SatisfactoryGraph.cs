using SatisfactoryTree.Models;

namespace SatisfactoryTree
{
    public class SatisfactoryGraph
    {
        public List<Item> Items { get; set; }

        public SatisfactoryGraph(string filter = "",
            ResearchType researchType = ResearchType.Tier8)
            //bool includeBuildings = false,
            //bool showOnlyDirectDependencies = false)
        {
            Items = BuildSatisfactoryTree(filter,
                researchType);
                //includeBuildings,
                //showOnlyDirectDependencies);
        }

        private static List<Item> BuildSatisfactoryTree(string nameFilter,
            ResearchType researchType)
        {
            //TODO
            List<Item> items = null;// AllItems.GetAllItems();

            //Filter by science level
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].ResearchType > researchType)
                {
                    items.RemoveAt(i);
                }
            }

            //Filter by name
            if (string.IsNullOrEmpty(nameFilter) == false)
            {
                Item? filteredItem = FindItem(items, nameFilter);
                if (filteredItem == null)
                {
                    throw new Exception(nameFilter + " item not found");
                }
                else
                {
                    List<Item> filteredItems = new()
                    {
                        //Add the root - this is the final item
                        filteredItem
                    };

                    //Get all of the inputs leading up to it
                    filteredItems.AddRange(GetInputs(items, filteredItem.Recipes));

                    //Sort the items by level
                    filteredItems = filteredItems.OrderBy(b => b.Level).ToList();
                    items = filteredItems;
                }
            }

            ////If enabled, only show the direct inputs to product an item
            //if (showOnlyDirectDependencies == true)
            //{
            //    Dictionary<string, int> inputs = new();
            //    List<Item> filteredItems = new();
            //    foreach (Item? item in items)
            //    {
            //        if (item.ItemType != ItemType.Building)
            //        {
            //            //If the item is not a building, hide it's recipe
            //            item.Recipes = new List<Recipe>();
            //        }
            //        else
            //        {
            //            //If it is a building, log all of it's inputs
            //            foreach (Recipe? recipe in item.Recipes)
            //            {
            //                foreach (KeyValuePair<string, int> input in recipe.Inputs)
            //                {
            //                    if (inputs.ContainsKey(input.Key) == false)
            //                    {
            //                        inputs.Add(input.Key, 1);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    //Add each item to the filter.
            //    foreach (Item? item in items)
            //    {
            //        if (!filteredItems.Contains(item) &&
            //            (item.ItemType == ItemType.Building ||
            //            inputs.ContainsKey(item.Name)))
            //        {
            //            filteredItems.Add(item);
            //        }
            //    }

            //    items = filteredItems;
            //}

            //for (int i = 0; i < items.Count; i++)
            //{
            //    Item? item = items[i];
            //    if (item.Name == "Accumulator")
            //    {
            //        int j = i;
            //    }
            //}

            return items;
        }

        //Get recipe inputs
        private static List<Item> GetInputs(List<Item> items, List<Recipe> recipes)
        {
            List<Item> inputs = new();
            foreach (Recipe recipe in recipes)
            {
                foreach (KeyValuePair<string, decimal> item in recipe.Inputs)
                {
                    Item? inputItem = FindItem(items, item.Key);
                    if (inputItem != null && inputs.Contains(inputItem) == false)
                    {
                        inputs.Add(inputItem);
                        List<Item> newItems = GetInputs(items, inputItem.Recipes);
                        foreach (Item newItem in newItems)
                        {
                            if (newItem != null && inputs.Contains(newItem) == false)
                            {
                                inputs.Add(newItem);
                            }
                        }
                    }
                }
            }
            return inputs;
        }

        private static Item? FindItem(List<Item> items, string name)
        {
            return items.Where(i => i.Name == name).FirstOrDefault();
        }

    }
}
