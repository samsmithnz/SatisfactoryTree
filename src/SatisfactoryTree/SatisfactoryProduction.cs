using MermaidDotNet;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;

namespace SatisfactoryTree
{
    public class SatisfactoryProduction
    {
        public List<Item> Items { get; set; }
        public List<ProductionItem> ProductionItems { get; set; }

        public SatisfactoryProduction()
        {
            Items = AllItems.GetAllItems();
            ProductionItems = new();
        }

        public List<ProductionItem> BuildSatisfactoryProductionPlan(ProductionItem itemGoal)
        {
            ProductionItems = new();
            if (itemGoal != null && itemGoal.Item != null)
            {
                ProcessOutputItem(itemGoal);
                //Identify which items aren't dependencies for other items
                List<string> dependencies = new();
                foreach (ProductionItem item in ProductionItems)
                {
                    foreach (KeyValuePair<string, decimal> dependent in item.Dependencies)
                    {
                        if (!dependencies.Any(p => p == dependent.Key))
                        {
                            dependencies.Add(dependent.Key);
                        }
                    }
                }
                //Mark items that are not dependencies
                foreach (ProductionItem item in ProductionItems)
                {
                    if (item != null && item.Item != null)
                    {
                        if (!dependencies.Any(p => p == item.Item?.Name))
                        {
                            item.OutputItem = true;
                        }
                    }
                }
            }
            return ProductionItems;
        }

        //Taking an output item, find the inputs required to produce it
        private bool ProcessOutputItem(ProductionItem targetItem)
        {
            List<KeyValuePair<string, decimal>> inputs = new();
            ProductionItem? currentItemMatch = null;
            if (targetItem != null && targetItem.Item != null)
            {
                //Process this item
                targetItem.BuildingQuantityRequired = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];
                //Check if this item is already in the production list, undate it instead of adding a new one
                if (ProductionItems.Any(p => p.Item?.Name == targetItem.Item.Name))
                {
                    currentItemMatch = ProductionItems.FirstOrDefault(p => p.Item?.Name == targetItem.Item.Name);
                    if (currentItemMatch != null)
                    {
                        currentItemMatch.Quantity += targetItem.Quantity;
                        currentItemMatch.BuildingQuantityRequired += targetItem.BuildingQuantityRequired;
                    }
                }
                else
                {
                    ProductionItems.Add(targetItem);
                }
                decimal itemOutputRatio = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];

                //Process each output (that isn't the target item)
                foreach (KeyValuePair<string, decimal> output in targetItem.Item.Recipes[0].Outputs)
                {
                    //Check for additional outputs
                    if (output.Key != targetItem.Item.Name)
                    {
                        //Process this item
                        Item? outputItem = FindItem(output.Key);
                        decimal outputQuantityWithRatio = output.Value * itemOutputRatio;
                        ProductionItem newProductionItem = new(outputItem, outputQuantityWithRatio)
                        {
                            BuildingQuantityRequired = itemOutputRatio
                        };
                        if (newProductionItem != null && newProductionItem.Item != null)
                        {
                            ProductionItems.Add(newProductionItem);
                            inputs.AddRange(newProductionItem.Item.Recipes[0].Inputs);
                        }
                    }
                }
                inputs.AddRange(targetItem.Item.Recipes[0].Inputs);

                //Process each input
                foreach (KeyValuePair<string, decimal> input in inputs)
                {
                    Item? inputItem = FindItem(input.Key);
                    if (inputItem != null)
                    {
                        decimal inputQuantityWithRatio = input.Value * itemOutputRatio;
                        targetItem.Dependencies.Add(input.Key, inputQuantityWithRatio);
                        ProductionItem newProductionItem = new(inputItem, inputQuantityWithRatio)
                        {
                            BuildingQuantityRequired = itemOutputRatio
                        };
                        ProcessOutputItem(newProductionItem);
                    }
                }
                //If this item already exists in the production list, update the quantity for it
                if (currentItemMatch != null)
                {
                    foreach (KeyValuePair<string, decimal> dependency in targetItem.Dependencies)
                    {
                        if (currentItemMatch.Dependencies.ContainsKey(dependency.Key))
                        {
                            currentItemMatch.Dependencies[dependency.Key] += dependency.Value;
                        }
                        else
                        {
                            currentItemMatch.Dependencies.Add(dependency.Key, dependency.Value);
                        }
                    }
                }
            }
            return true;
        }

        public Item? FindItem(string itemName)
        {
            Item? result = null;
            if (Items != null && Items.Count > 0)
            {
                foreach (Item item in Items)
                {
                    if (item.Name == itemName)
                    {
                        result = item;
                        break;
                    }
                }
            }
            return result;
        }

        public string ToMermaidString()
        {
            string direction = "LR";
            List<MermaidDotNet.Models.Node> nodes = new();
            foreach (ProductionItem item in ProductionItems)
            {
                if (item != null && item.Item != null)
                {
                    nodes.Add(new(item.Item.Name.Replace(" ", ""), '"' + "x" + RoundUpAndFormat(item.BuildingQuantityRequired) + " " + item.Item.Recipes[0].ManufactoringBuilding + "<br>(" + item.Item.Name + ")" + '"'));
                    if (item.OutputItem == true)
                    {
                        string finalItemQuantity = item.Quantity.ToString("0.0");
                        if ((int)item.Quantity == item.Quantity)
                        {
                            finalItemQuantity = item.Quantity.ToString("0");
                        }
                        nodes.Add(new(item.Item?.Name.Replace(" ", "") + "_Item", finalItemQuantity + " " + item.Item?.Name, MermaidDotNet.Models.Node.eShape.Stadium));
                    }
                }
            }
            List<MermaidDotNet.Models.Link> links = new();
            foreach (ProductionItem item in ProductionItems)
            {
                if (item != null && item.Item != null)
                {
                    foreach (KeyValuePair<string, decimal> itemInput in item.Dependencies)
                    {
                        string itemQuantity = itemInput.Value.ToString("0.0");
                        if ((int)itemInput.Value == itemInput.Value)
                        {
                            itemQuantity = itemInput.Value.ToString("0");
                        }
                        links.Add(
                        new MermaidDotNet.Models.Link(
                                itemInput.Key.Replace(" ", ""),
                                item.Item.Name.Replace(" ", ""),
                                '"' + itemInput.Key + "<br>(" + itemQuantity + " units/min)" + '"')
                            );
                    }
                    if (item.OutputItem == true)
                    {
                        //if (ProductionItems != null && ProductionItems.Count > 0 &&
                        //        ProductionItems[0] != null && ProductionItems[0].Item != null &&
                        //        ProductionItems[0].Item?.Name != null)
                        //{
                        string? source;
                        string? destination;
                        source = item.Item.Name.Replace(" ", "");
                        destination = item.Item.Name.Replace(" ", "") + "_Item";
                        if (source != null && destination != null)
                        {
                            links.Add(new MermaidDotNet.Models.Link(
                                                source,
                                                destination,
                                                '"' + item.Item.Name + "<br>(" + item.Quantity.ToString("0") + " units/min)" + '"'));
                        }
                        //}
                    }
                }
            }
            Flowchart flowchart = new(direction, nodes, links);
            string result = flowchart.CalculateFlowchart();

            return result;
        }

        //Round up to the nearest decimal point - if one exists
        private static string RoundUpAndFormat(decimal value)
        {
            if ((int)value == value)
            {
                return (Math.Ceiling(value * 10) / 10).ToString("0");
            }
            else
            {
                return (Math.Ceiling(value * 10) / 10).ToString("0.0");
            }
        }

    }
}
