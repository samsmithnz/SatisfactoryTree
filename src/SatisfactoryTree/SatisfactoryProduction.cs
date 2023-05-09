using MermaidDotNet;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;

namespace SatisfactoryTree
{
    public class SatisfactoryProduction
    {
        public List<Item> Items { get; set; }
        public List<ProductionItem> ProductionItems { get; set; }
        public Queue<KeyValuePair<string, decimal>> InputQueue { get; set; }

        public SatisfactoryProduction()
        {
            Items = AllItems.GetAllItems();
            ProductionItems = new();
            InputQueue = new();
        }

        public List<ProductionItem> BuildSatisfactoryProductionPlan(ProductionItem itemGoal)
        {
            ProductionItems = new();
            InputQueue = new();
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
                //Check if this item is already in the production list
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
                if (item.Item.Name == "Steel Ingot")
                {
                    int i = 0;
                }
                foreach (KeyValuePair<string, decimal> input in item.Item.Recipes[0].Inputs)
                {
                    Item? inputItem = FindItem(input.Key);
                    if (inputItem != null)
                    {
                        decimal outputQuantity = item.Item.Recipes[0].Outputs[item.Item.Name];
                        decimal inputQuantity = input.Value;
                        decimal ratio = item.Quantity / outputQuantity;
                        decimal newQuantity = inputQuantity * ratio;
                        item.Dependencies.Add(input.Key, newQuantity);
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
                        ProductionItem newProductionItem = new(inputItem, newQuantity)
                        {
                            BuildingQuantityRequired = ratio
                        };
                        ProcessOutputItem(newProductionItem);
                    }
                }
            }
            return true;
        }

        private List<ProductionItem> GetChildren(string itemName, decimal quantity)
        {
            List<ProductionItem> results = new();
            Item? item = FindItem(itemName);
            if (item != null && item.Recipes.Count > 0 && item.Recipes[0].Inputs.Count > 0)
            {
                //Look at each input and the quantity needed to make the item 
                foreach (KeyValuePair<string, decimal> recipeInput in item.Recipes[0].Inputs)
                {
                    //get the input item
                    ProductionItem? inputItem = new(FindItem(recipeInput.Key), recipeInput.Value);
                    if (inputItem != null && inputItem.Item != null)
                    {
                        inputItem.Quantity = recipeInput.Value / inputItem.Item.Recipes[0].ThroughPutPerMinute;
                        //Add the input item to the results
                        results.Add(inputItem);
                        //Then get the children of the input item
                        results.AddRange(GetChildren(inputItem.Item.Name, inputItem.Item.Recipes[0].ThroughPutPerMinute * quantity));
                    }
                }
            }
            return results;
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
                        links.Add(
                        new MermaidDotNet.Models.Link(
                                itemInput.Key.Replace(" ", ""),
                                item.Item.Name.Replace(" ", ""),
                                '"' + itemInput.Key + "<br>(" + itemInput.Value.ToString("0") + " units/min)" + '"')
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
