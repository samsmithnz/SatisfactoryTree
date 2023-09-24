using MermaidDotNet;
using MermaidDotNet.Models;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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
                if (currentDependencyLevel != ItemGroups[0].Dependencies)
                {
                    if (currentSubGraph != null)
                    {
                        subGraphs.Add(currentSubGraph);
                    }
                    currentDependencyLevel = ItemGroups[0].Dependencies;
                    currentSubGraph = new(ItemGroups[0].Dependencies.ToString());
                }
                if (currentSubGraph != null)
                {
                    string name = ItemGroups[0].Name;
                    currentSubGraph.Nodes.Add(new MermaidDotNet.Models.Node(name.Replace(" ", ""), name));
                }
            }
            if (currentSubGraph != null)
            {
                subGraphs.Add(currentSubGraph);
            }

            Flowchart flowchart = new("LR", new(), new(), subGraphs);
            return flowchart;


            //ProductionItems = new ();
            //    if (itemGoal != null && itemGoal.Item != null)
            //    {
            //        ProcessOutputItem(itemGoal);

            //        //Search for items that are not dependencies to identify outputs
            //        List<string> dependencies = new();
            //        foreach (ProductionItem item in ProductionItems)
            //        {
            //            foreach (KeyValuePair<string, decimal> dependent in item.Dependencies)
            //            {
            //                if (!dependencies.Any(p => p == dependent.Key))
            //                {
            //                    dependencies.Add(dependent.Key);
            //                }
            //            }
            //        }
            //        //Mark items that are not dependencies
            //        foreach (ProductionItem item in ProductionItems)
            //        {
            //            if (item != null && item.Item != null)
            //            {
            //                if (!dependencies.Any(p => p == item.Item?.Name))
            //                {
            //                    item.OutputItem = true;
            //                }
            //            }
            //        }
            //    }
            //    ProductionCalculation productionCalculation = new()
            //    {
            //        ProductionItems = ProductionItems,
            //        PowerConsumption = PowerConsumption
            //    };
            //    return productionCalculation;
            //}



        }
    }
}
