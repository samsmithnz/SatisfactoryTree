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

        //private bool AddItemsToInputs(Dictionary<string, decimal> inputs)
        //{
        //    foreach (KeyValuePair<string, decimal> inputItem in inputs)
        //    {
        //        Item? itemInput = FindItem(inputItem.Key);
        //        if (itemInput != null)
        //        {
        //            decimal inputThroughPutPerMinute = itemInput.Recipes[0].ThroughPutPerMinute;
        //            decimal adjustedInputThroughPutPerMinute = inputThroughPutPerMinute;// * ratio;
        //            if (adjustedInputThroughPutPerMinute > inputItem.Value)
        //            {
        //                adjustedInputThroughPutPerMinute = inputItem.Value;
        //            }
        //            //itemGoal.Dependencies.Add(inputItem.Key, adjustedInputThroughPutPerMinute);
        //            //Add each item to a queue to add to other dependencies
        //            InputQueue.Enqueue(new(inputItem.Key, adjustedInputThroughPutPerMinute));
        //        }
        //    }
        //    return true;
        //}

        //Taking an output item, find the inputs required to produce it
        private bool ProcessOutputItem(ProductionItem item)
        {
            ProductionItem? match = null;
            if (item != null && item.Item != null)
            {
                item.BuildingQuantityRequired = item.Quantity / item.Item.Recipes[0].Outputs[item.Item.Name];
                if (ProductionItems.Any(p => p.Item.Name == item.Item.Name))
                {
                    match = ProductionItems.FirstOrDefault(p => p.Item.Name == item.Item.Name);
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
                        //if (item.Quantity > outputQuantity)
                        //{
                        //    outputQuantity = item.Quantity;
                        //}
                        decimal inputQuantity = input.Value;
                        decimal ratio = item.Quantity / outputQuantity;
                        decimal newQuantity = inputQuantity * ratio;
                        item.Dependencies.Add(input.Key, newQuantity);
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
                nodes.Add(new(item.Item.Name.Replace(" ", ""), '"' + "x" + item.BuildingQuantityRequired + " " + item.Item.Recipes[0].ManufactoringBuilding + "<br>(" + item.Item.Name + ")" + '"'));
            }
            if (productionItem != null)
            {
                nodes.Add(new(productionItem.Item.Name.Replace(" ", "") + "_Item", productionItem.Quantity.ToString() + " " + productionItem.Item.Name));
            }
            List<MermaidDotNet.Models.Link> links = new();
            foreach (ProductionItem item in ProductionItems)
            {
                foreach (KeyValuePair<string, decimal> itemInput in item.Dependencies)
                {
                    links.Add(
                        new MermaidDotNet.Models.Link(
                                itemInput.Key.Replace(" ", ""),
                                item.Item.Name.Replace(" ", ""),
                                '"' + itemInput.Key + "<br>(" + itemInput.Value.ToString("0") + " units/min)" + '"')
                            );
                }
            }
            if (productionItem != null)
            {
                links.Add(new MermaidDotNet.Models.Link(
                                    ProductionItems[0].Item.Name.Replace(" ", ""),
                                    productionItem.Item.Name.Replace(" ", "") + "_Item",
                                    '"' + productionItem.Item.Name + "<br>(" + productionItem.Quantity.ToString("0") + " units/min)" + '"'));
            }
            Flowchart flowchart = new(direction, nodes, links);
            string result = flowchart.CalculateFlowchart();

            return result;
        }

    }
}
