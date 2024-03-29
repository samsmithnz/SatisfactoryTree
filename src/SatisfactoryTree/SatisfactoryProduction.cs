﻿using MermaidDotNet;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;
using System.Text;

namespace SatisfactoryTree
{
    public class SatisfactoryProduction
    {
        public List<Item> Items { get; set; }
        public List<Building> Buildings { get; set; }
        public List<ProductionItem> ProductionItems { get; set; }
        public decimal PowerConsumption { get; set; }

        public SatisfactoryProduction()
        {
            Items = AllItems.GetAllItems();
            Buildings = AllBuildings.GetAllBuildings();
            ProductionItems = new();
        }

        //Build a production plan for a given target item
        public ProductionCalculation BuildProductionPlan(ProductionItem itemGoal)
        {
            ProductionItems = new();
            if (itemGoal != null && itemGoal.Item != null)
            {
                ProcessOutputItem(itemGoal);

                //Search for items that are not dependencies to identify outputs
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
            ProductionCalculation productionCalculation = new()
            {
                ProductionItems = ProductionItems,
                PowerConsumption = PowerConsumption
            };
            return productionCalculation;
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
                if (targetItem != null && targetItem.Item != null && targetItem.Item.Recipes.Count > 0 && targetItem.Item.Recipes[0].Building != null)
                {
                    Building? building = FindBuilding(targetItem.Item.Recipes[0].Building);
                    if (building != null)
                    {
                        PowerConsumption += building.PowerConsumption * targetItem.BuildingQuantityRequired;
                    }
                }
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
                            foreach (KeyValuePair<string, decimal> input in newProductionItem.Item.Recipes[0].Inputs)
                            {
                                newProductionItem.Dependencies.Add(input.Key, input.Value);
                            }
                            ProductionItems.Add(newProductionItem);
                            //Commented out this - because we are already adding this input below V
                            //inputs.AddRange(newProductionItem.Item.Recipes[0].Inputs);
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
                        if (targetItem.Dependencies.Any(p => p.Key == input.Key))
                        {
                            KeyValuePair<string, decimal>? currentInputMatch = targetItem.Dependencies.FirstOrDefault(p => p.Key == input.Key);
                            if (currentInputMatch != null)
                            {
                                decimal value = ((KeyValuePair<string, decimal>)currentInputMatch).Value;
                                currentInputMatch = new KeyValuePair<string, decimal>(input.Key, value + targetItem.Quantity);
                            }
                        }
                        else
                        {
                            targetItem.Dependencies.Add(input.Key, inputQuantityWithRatio);
                        }
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

        //Find an item by name
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

        //Find a building by name
        public Building? FindBuilding(string buildingName)
        {
            Building? result = null;
            if (Buildings != null && Buildings.Count > 0)
            {
                foreach (Building item in Buildings)
                {
                    if (item.Name == buildingName)
                    {
                        result = item;
                        break;
                    }
                }
            }
            return result;
        }

        //Create the mermaid string for the production plan
        public string ToMermaidString()
        {
            string direction = "LR";
            List<MermaidDotNet.Models.Node> nodes = new();
            foreach (ProductionItem item in ProductionItems)
            {
                if (item != null && item.Item != null)
                {
                    nodes.Add(new(item.Item.Recipes[0].Name.Replace(" ", ""), '"' + "x" + RoundUpAndFormat(item.BuildingQuantityRequired) + " " + item.Item.Recipes[0].Building + "<br>(" + item.Item.Recipes[0].Name + ")" + '"'));
                    if (item.OutputItem == true)
                    {
                        string finalItemQuantity = item.Quantity.ToString("0.0");
                        if ((int)item.Quantity == item.Quantity)
                        {
                            finalItemQuantity = item.Quantity.ToString("0");
                        }
                        nodes.Add(new(item.Item?.Name.Replace(" ", "") + "_Item", finalItemQuantity + " " + item.Item?.Name, MermaidDotNet.Models.Node.ShapeType.Stadium));
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
                        string source = itemInput.Key.Replace(" ", "");
                        string destination = item.Item.Recipes[0].Name.Replace(" ", "");
                        string text = '"' + itemInput.Key + "<br>(" + itemQuantity + " units/min)" + '"';
                        MermaidDotNet.Models.Link link = new(source, destination, text);
                        if (!links.Any(g => g.SourceNode == link.SourceNode &&
                                            g.DestinationNode == link.DestinationNode &&
                                            g.Text == link.Text))
                        {
                            links.Add(new MermaidDotNet.Models.Link(source, destination, text));
                        }
                    }
                    if (item.OutputItem == true)
                    {
                        string? source;
                        string? destination;
                        source = item.Item.Recipes[0].Name.Replace(" ", "");
                        destination = item.Item.Name.Replace(" ", "") + "_Item";
                        if (source != null && destination != null)
                        {
                            links.Add(new MermaidDotNet.Models.Link(
                                                source,
                                                destination,
                                                '"' + item.Item.Name + "<br>(" + item.Quantity.ToString("0") + " units/min)" + '"'));
                        }
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
