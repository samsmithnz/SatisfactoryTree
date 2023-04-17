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
                decimal quantity = itemGoal.Quantity;
                decimal total = itemGoal.Item.Recipes[0].ThroughPutPerMinute;
                decimal ratio = quantity / total;
                itemGoal.BuildingQuantityRequired = ratio;
                foreach (KeyValuePair<string, decimal> inputItem in itemGoal.Item.Recipes[0].Inputs)
                {
                    Item? itemInput = FindItem(inputItem.Key);
                    if (itemInput != null)
                    {
                        decimal inputThroughPutPerMinute = itemInput.Recipes[0].ThroughPutPerMinute;
                        itemGoal.Dependencies.Add(inputItem.Key, inputThroughPutPerMinute * ratio);
                        //Add each item to a queue to add to other dependencies
                        InputQueue.Enqueue(new(inputItem.Key, inputThroughPutPerMinute * ratio));
                    }
                }
                ProductionItems.Add(itemGoal);
            }

            while (InputQueue.Count > 0)
            {
                KeyValuePair<string, decimal> inputToProcess = InputQueue.Dequeue();
                //See if it exists in the production item list already
                ProductionItem? productionItem = ProductionItems.Where(i => i.Item?.Name == inputToProcess.Key).FirstOrDefault();
                if (productionItem != null)
                {
                    productionItem.Quantity += inputToProcess.Value;
                    //Now process these items in a queue recursively
                }
                else
                {
                    ProductionItems.Add(new(FindItem(inputToProcess.Key), inputToProcess.Value));
                }
            }


            ////Look at the recipe inputs, and get all of the item inputs that are needed to make the itemGoal
            //if (itemGoal != null && itemGoal.Item != null && itemGoal.Item.Recipes.Count > 0 && itemGoal.Item.Recipes[0].Inputs.Count > 0)
            //{
            //    productionPlan.AddRange(GetChildren(itemGoal.Item.Name, ratio));
            //}

            return ProductionItems;
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
                    ProductionItem? inputItem = new ProductionItem(FindItem(recipeInput.Key), recipeInput.Value);
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

    }
}
