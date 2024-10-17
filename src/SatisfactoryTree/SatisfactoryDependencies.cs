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
            //TODO
            //Items = AllItems.GetAllItems();
            //ItemGroups = AllItemGroups.GetAllItemGroups();
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
                    Random rnd = new();
                    //process the item dependencies
                    foreach (KeyValuePair<string, decimal> input in item.Recipes[0].Inputs)
                    {
                        item.Level = rnd.Next(1, 9);
                        flowchart.Links.Add(
                            new MermaidDotNet.Models.Link(input.Key.Replace(" ", ""), 
                            node.Text.Replace(" ", ""),
                            null,
                            GetLinkStyle(item.Level)));
                    }
                }

            }

            return flowchart;

        }

        private static string GetLinkStyle(int level)
        {
            switch (level)
            {
                case 0:
                case 1:
                    return "stroke:black";
                case 2:
                    return "stroke:blue";
                case 3:
                    return "stroke:green";
                case 4:
                    return "stroke:orange";
                case 5:
                    return "stroke:red";
                case 6:
                    return "stroke:purple";
                //case 7:
                //    return "stroke:yellow";
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return "stroke:brown";
                default:
                    return "DEFAULT";
            }
        }
    }
}
