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
            }
            return ProductionItems;
        }

        //Taking an output item, find the inputs required to produce it
        private bool ProcessOutputItem(ProductionItem item)
        {
            ProductionItem? match = null;
            if (item != null && item.Item != null)
            {
                item.BuildingQuantityRequired = item.Quantity / item.Item.Recipes[0].Outputs[item.Item.Name];
                //Check if this item is already in the production list, undate it instead of adding a new one
                if (ProductionItems.Any(p => p.Item?.Name == item.Item.Name))
                {
                    match = ProductionItems.FirstOrDefault(p => p.Item?.Name == item.Item.Name);
                    if (match != null)
                    {
                        match.Quantity += item.Quantity;
                        match.BuildingQuantityRequired += item.BuildingQuantityRequired;
                    }
                }
                else
                {
                    ProductionItems.Add(item);
                }
                foreach (KeyValuePair<string, decimal> input in item.Item.Recipes[0].Inputs)
                {
                    Item? inputItem = FindItem(input.Key);
                    if (inputItem != null)
                    {
                        decimal outputQuantity = item.Item.Recipes[0].Outputs[item.Item.Name];
                        decimal inputQuantity = input.Value;
                        decimal ratio = item.Quantity / outputQuantity;
                        decimal inputQuantityWithRatio = inputQuantity * ratio;
                        item.Dependencies.Add(input.Key, inputQuantityWithRatio);                        
                        ProductionItem newProductionItem = new(inputItem, inputQuantityWithRatio)
                        {
                            BuildingQuantityRequired = ratio
                        };
                        ProcessOutputItem(newProductionItem);
                    }
                }
                //If this item already exists in the production list, update the quantity for it
                if (match != null)
                {
                    foreach (KeyValuePair<string, decimal> dependency in item.Dependencies)
                    {
                        if (match.Dependencies.ContainsKey(dependency.Key))
                        {
                            match.Dependencies[dependency.Key] += dependency.Value;
                        }
                        else
                        {
                            match.Dependencies.Add(dependency.Key, dependency.Value);
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

        public string GetMermaidString(ProductionItem? productionItem = null)
        {
            string direction = "LR";
            List<MermaidDotNet.Models.Node> nodes = new();
            foreach (ProductionItem item in ProductionItems)
            {
                if (item != null && item.Item != null)
                {
                    nodes.Add(new(item.Item.Name.Replace(" ", ""), '"' + "x" + RoundUpAndFormat(item.BuildingQuantityRequired) + " " + item.Item.Recipes[0].ManufactoringBuilding + "<br>(" + item.Item.Name + ")" + '"'));
                }
            }
            if (productionItem != null && productionItem.Item != null)
            {
                string finalItemQuantity = productionItem.Quantity.ToString("0.0");
                if ((int)productionItem.Quantity == productionItem.Quantity)
                {
                    finalItemQuantity = productionItem.Quantity.ToString("0");
                }
                nodes.Add(new(productionItem.Item?.Name.Replace(" ", "") + "_Item", finalItemQuantity + " " + productionItem.Item?.Name));
            }
            List<MermaidDotNet.Models.Link> links = new();
            foreach (ProductionItem item in ProductionItems)
            {
                foreach (KeyValuePair<string, decimal> itemInput in item.Dependencies)
                {
                    if (item != null && item.Item != null)
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
                }
            }
            if (productionItem != null && productionItem.Item != null &&
                ProductionItems != null && ProductionItems.Count > 0 &&
                ProductionItems[0] != null && ProductionItems[0].Item != null &&
                ProductionItems[0].Item?.Name != null)
            {
                string? source;
                string? destination;
                source = ProductionItems[0].Item?.Name.Replace(" ", "");
                destination = productionItem.Item.Name.Replace(" ", "") + "_Item";
                if (source != null && destination != null)
                {
                    links.Add(new MermaidDotNet.Models.Link(
                                        source,
                                        destination,
                                        '"' + productionItem.Item.Name + "<br>(" + productionItem.Quantity.ToString("0") + " units/min)" + '"'));
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
