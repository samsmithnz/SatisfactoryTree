using MermaidDotNet;
using MermaidDotNet.Models;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;

namespace SatisfactoryTree
{
    public class SatisfactoryDependencies
    {
        public List<Item> Items { get; set; }
        public List<ItemGroup> ItemGroups { get; set; }

        public SatisfactoryDependencies()
        {
            Items = AllItems.GetAllItems();
            ItemGroups = AllItemGroups.GetAllItemGroups();
        }

        //Build a production plan for a given target item
        public Flowchart BuildDependencyPlan()
        {

            int currentDependencyLevel = -1;
            List<SubGraph> subGraphs = new();
            SubGraph? currentSubGraph = null;
            for (int i = 0; i < ItemGroups.Count - 1; i++)
            {
                if (currentDependencyLevel != ItemGroups[i].Dependencies)
                {
                    if (currentSubGraph != null)
                    {
                        subGraphs.Add(currentSubGraph);
                    }
                    currentDependencyLevel = ItemGroups[i].Dependencies;
                    currentSubGraph = new(ItemGroups[i].Dependencies.ToString());
                }
                if (currentSubGraph != null)
                {
                    string name = ItemGroups[i].Name;
                    currentSubGraph.Nodes.Add(new MermaidDotNet.Models.Node(name.Replace(" ", ""), name));
                }
            }
            if (currentSubGraph != null)
            {
                subGraphs.Add(currentSubGraph);
            }

            Flowchart flowchart = new("LR", new(), new(), subGraphs);
            //Now calculate all of the links
            foreach (MermaidDotNet.Models.Node node in flowchart.NavigationNodes)
            {
                //Find item
                Item? item = Items.FirstOrDefault(p => p.Name == node.Text);
                if (item != null)
                {
                    //process the item dependencies
                    foreach (KeyValuePair<string, decimal> input in item.Recipes[0].Inputs)
                    {
                        flowchart.Links.Add(new MermaidDotNet.Models.Link(input.Key.Replace(" ", ""), node.Text.Replace(" ", "")));
                    }
                }

            }

            return flowchart;

        }
    }
}
