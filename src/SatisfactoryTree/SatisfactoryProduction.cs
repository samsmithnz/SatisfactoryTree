//using MermaidDotNet;
//using SatisfactoryTree.DataAccess;
//using SatisfactoryTree.Models;

//namespace SatisfactoryTree
//{
//    public class SatisfactoryProduction
//    {
//        public List<NewItem> Items { get; set; }
//        public List<NewRecipe> Recipes { get; set; }
//        public List<NewBuilding> Buildings { get; set; }
//        public List<ProductionItem> ProductionItems { get; set; }
//        public decimal PowerConsumption { get; set; }

//        public SatisfactoryProduction()
//        {
//            NewContent content = FileContent.LoadJsonContent();
//            Items = content.Items;
//            Recipes = content.Recipes;
//            Buildings = content.Buildings;
//            ProductionItems = [];
//        }

//        public ProductionCalculation NewBuildProductionPlan(NewTargetItem newTargetItem)
//        {
//            if (newTargetItem != null)
//            {
//                ProcessOutputItem(newTargetItem);

//                //Search for items that are not dependencies to identify outputs
//                List<string> dependencies = [];
//                foreach (ProductionItem item in ProductionItems)
//                {
//                    foreach (KeyValuePair<string, decimal> dependent in item.Dependencies)
//                    {
//                        if (!dependencies.Any(p => p == dependent.Key))
//                        {
//                            dependencies.Add(dependent.Key);
//                        }
//                    }
//                }
//                //Mark items that are not dependencies
//                foreach (ProductionItem item in ProductionItems)
//                {
//                    if (item != null && item.Item != null)
//                    {
//                        if (!dependencies.Any(p => p == item.Item?.Name))
//                        {
//                            item.OutputItem = true;
//                        }
//                    }
//                }
//            }
//            ProductionCalculation productionCalculation = new()
//            {
//                ProductionItems = ProductionItems,
//                PowerConsumption = PowerConsumption
//            };
//            return productionCalculation;


//            //ProductionItems = [];
//            //ProductionItem productionItem = new(newTargetItem.ItemName, newTargetItem.ItemQuantity);
//            //NewRecipe? recipe = FindRecipe(newTargetItem.ItemName);
//            //if (recipe != null)
//            //{
//            //    decimal ratePerMinute = 60M / recipe.ManufactoringDuration;
//            //    decimal buildingQuantityRequired = newTargetItem.ItemQuantity / ratePerMinute;
//            //    productionItem.BuildingQuantityRequired = buildingQuantityRequired;
//            //}
//            //ProductionCalculation productionCalculation = new()
//            //{
//            //    ProductionItems = ProductionItems,
//            //    PowerConsumption = 7.5M
//            //};
//            //ProductionItems.Add(productionItem);

//            //return productionCalculation;
//        }

//        //Taking an output item, find the inputs required to produce it
//        private bool ProcessOutputItem(NewTargetItem targetItem)
//        {
//            List<KeyValuePair<string, decimal>> inputs = [];
//            ProductionItem? currentItemMatch = null;
//            if (targetItem != null)
//            {
//                //Process this item
//                ProductionItem productionItem = new(targetItem.ItemName, targetItem.ItemQuantity);
//                NewRecipe? recipe = FindRecipe(targetItem.ItemName);
//                if (recipe != null)
//                {
//                    decimal ratePerMinute = 60M / recipe.ManufactoringDuration;
//                    decimal buildingQuantityRequired = targetItem.ItemQuantity / ratePerMinute;
//                    productionItem.BuildingQuantityRequired = buildingQuantityRequired;
//                }
//                //targetItem.BuildingQuantityRequired = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];
//                //if (targetItem != null && targetItem.Item != null && targetItem.Item.Recipes.Count > 0 && targetItem.Item.Recipes[0].Building != null)
//                //{
//                //    Building? building = FindBuilding(targetItem.Item.Recipes[0].Building);
//                //    if (building != null)
//                //    {
//                //        PowerConsumption += building.PowerConsumption * targetItem.BuildingQuantityRequired;
//                //    }
//                //}
//                //Check if this item is already in the production list, update it instead of adding a new one
//                if (ProductionItems.Any(p => p.Item?.Name == targetItem.Item.Name))
//                {
//                    currentItemMatch = ProductionItems.FirstOrDefault(p => p.Item?.Name == targetItem.Item.Name);
//                    if (currentItemMatch != null)
//                    {
//                        currentItemMatch.Quantity += targetItem.Quantity;
//                        currentItemMatch.BuildingQuantityRequired += targetItem.BuildingQuantityRequired;
//                    }
//                }
//                else
//                {
//                    ProductionItems.Add(targetItem);
//                }
//                decimal itemOutputRatio = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];

//                //Process each output (that isn't the target item)
//                foreach (KeyValuePair<string, decimal> output in targetItem.Item.Recipes[0].Outputs)
//                {
//                    //Check for additional outputs
//                    if (output.Key != targetItem.Item.Name)
//                    {
//                        //Process this item
//                        Item? outputItem = FindItem(output.Key);
//                        decimal outputQuantityWithRatio = output.Value * itemOutputRatio;
//                        ProductionItem newProductionItem = new(outputItem, outputQuantityWithRatio)
//                        {
//                            BuildingQuantityRequired = itemOutputRatio
//                        };
//                        if (newProductionItem != null && newProductionItem.Item != null)
//                        {
//                            foreach (KeyValuePair<string, decimal> input in newProductionItem.Item.Recipes[0].Inputs)
//                            {
//                                newProductionItem.Dependencies.Add(input.Key, input.Value);
//                            }
//                            ProductionItems.Add(newProductionItem);
//                            //Commented out this - because we are already adding this input below V
//                            //inputs.AddRange(newProductionItem.Item.Recipes[0].Inputs);
//                        }
//                    }
//                }
//                inputs.AddRange(targetItem.Item.Recipes[0].Inputs);

//                //Process each input
//                foreach (KeyValuePair<string, decimal> input in inputs)
//                {
//                    Item? inputItem = FindItem(input.Key);
//                    if (inputItem != null)
//                    {
//                        decimal inputQuantityWithRatio = input.Value * itemOutputRatio;
//                        if (targetItem.Dependencies.Any(p => p.Key == input.Key))
//                        {
//                            KeyValuePair<string, decimal>? currentInputMatch = targetItem.Dependencies.FirstOrDefault(p => p.Key == input.Key);
//                            if (currentInputMatch != null)
//                            {
//                                decimal value = ((KeyValuePair<string, decimal>)currentInputMatch).Value;
//                                currentInputMatch = new KeyValuePair<string, decimal>(input.Key, value + targetItem.Quantity);
//                            }
//                        }
//                        else
//                        {
//                            targetItem.Dependencies.Add(input.Key, inputQuantityWithRatio);
//                        }
//                        ProductionItem newProductionItem = new(inputItem, inputQuantityWithRatio)
//                        {
//                            BuildingQuantityRequired = itemOutputRatio
//                        };
//                        ProcessOutputItem(newProductionItem);
//                    }
//                }
//                //If this item already exists in the production list, update the quantity for it
//                if (currentItemMatch != null)
//                {
//                    foreach (KeyValuePair<string, decimal> dependency in targetItem.Dependencies)
//                    {
//                        if (currentItemMatch.Dependencies.ContainsKey(dependency.Key))
//                        {
//                            currentItemMatch.Dependencies[dependency.Key] += dependency.Value;
//                        }
//                        else
//                        {
//                            currentItemMatch.Dependencies.Add(dependency.Key, dependency.Value);
//                        }
//                    }
//                }
//            }
//            return true;
//        }

//        public NewItem? FindItem(string itemName)
//        {
//            foreach (NewItem item in Items)
//            {
//                if (item.DisplayName == itemName)
//                {
//                    return item;
//                }
//            }
//            return null;
//        }

//        public NewRecipe? FindRecipe(string itemName)
//        {
//            foreach (NewRecipe item in Recipes)
//            {
//                if (item.DisplayName == itemName && item.IsAlternateRecipe == false)
//                {
//                    return item;
//                }
//            }
//            return null;
//        }

//        public NewBuilding FindBuilding(string buildingName)
//        {
//            foreach (NewBuilding building in Buildings)
//            {
//                if (building.Name == buildingName)
//                {
//                    return building;
//                }
//            }
//            return null;
//        }

//        ////Find an item by name
//        //public Item? FindItem(string itemName)
//        //{
//        //    Item? result = null;
//        //    if (Items != null && Items.Count > 0)
//        //    {
//        //        foreach (Item item in Items)
//        //        {
//        //            if (item.Name == itemName)
//        //            {
//        //                result = item;
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    return result;
//        //}

//        ////Find a building by name
//        //public Building? FindBuilding(string buildingName)
//        //{
//        //    Building? result = null;
//        //    if (Buildings != null && Buildings.Count > 0)
//        //    {
//        //        foreach (Building item in Buildings)
//        //        {
//        //            if (item.Name == buildingName)
//        //            {
//        //                result = item;
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    return result;
//        //}

//        ////Build a production plan for a given target item
//        //public ProductionCalculation BuildProductionPlan(ProductionItem itemGoal)
//        //{
//        //    ProductionItems = [];
//        //    if (itemGoal != null && itemGoal.Item != null)
//        //    {
//        //        ProcessOutputItem(itemGoal);

//        //        //Search for items that are not dependencies to identify outputs
//        //        List<string> dependencies = [];
//        //        foreach (ProductionItem item in ProductionItems)
//        //        {
//        //            foreach (KeyValuePair<string, decimal> dependent in item.Dependencies)
//        //            {
//        //                if (!dependencies.Any(p => p == dependent.Key))
//        //                {
//        //                    dependencies.Add(dependent.Key);
//        //                }
//        //            }
//        //        }
//        //        //Mark items that are not dependencies
//        //        foreach (ProductionItem item in ProductionItems)
//        //        {
//        //            if (item != null && item.Item != null)
//        //            {
//        //                if (!dependencies.Any(p => p == item.Item?.Name))
//        //                {
//        //                    item.OutputItem = true;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    ProductionCalculation productionCalculation = new()
//        //    {
//        //        ProductionItems = ProductionItems,
//        //        PowerConsumption = PowerConsumption
//        //    };
//        //    return productionCalculation;
//        //}

//        ////Taking an output item, find the inputs required to produce it
//        //private bool ProcessOutputItem(ProductionItem targetItem)
//        //{
//        //    List<KeyValuePair<string, decimal>> inputs = [];
//        //    ProductionItem? currentItemMatch = null;
//        //    if (targetItem != null && targetItem.Item != null)
//        //    {
//        //        //Process this item
//        //        targetItem.BuildingQuantityRequired = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];
//        //        if (targetItem != null && targetItem.Item != null && targetItem.Item.Recipes.Count > 0 && targetItem.Item.Recipes[0].Building != null)
//        //        {
//        //            Building? building = FindBuilding(targetItem.Item.Recipes[0].Building);
//        //            if (building != null)
//        //            {
//        //                PowerConsumption += building.PowerConsumption * targetItem.BuildingQuantityRequired;
//        //            }
//        //        }
//        //        //Check if this item is already in the production list, undate it instead of adding a new one
//        //        if (ProductionItems.Any(p => p.Item?.Name == targetItem.Item.Name))
//        //        {
//        //            currentItemMatch = ProductionItems.FirstOrDefault(p => p.Item?.Name == targetItem.Item.Name);
//        //            if (currentItemMatch != null)
//        //            {
//        //                currentItemMatch.Quantity += targetItem.Quantity;
//        //                currentItemMatch.BuildingQuantityRequired += targetItem.BuildingQuantityRequired;
//        //            }
//        //        }
//        //        else
//        //        {
//        //            ProductionItems.Add(targetItem);
//        //        }
//        //        decimal itemOutputRatio = targetItem.Quantity / targetItem.Item.Recipes[0].Outputs[targetItem.Item.Name];

//        //        //Process each output (that isn't the target item)
//        //        foreach (KeyValuePair<string, decimal> output in targetItem.Item.Recipes[0].Outputs)
//        //        {
//        //            //Check for additional outputs
//        //            if (output.Key != targetItem.Item.Name)
//        //            {
//        //                //Process this item
//        //                Item? outputItem = FindItem(output.Key);
//        //                decimal outputQuantityWithRatio = output.Value * itemOutputRatio;
//        //                ProductionItem newProductionItem = new(outputItem, outputQuantityWithRatio)
//        //                {
//        //                    BuildingQuantityRequired = itemOutputRatio
//        //                };
//        //                if (newProductionItem != null && newProductionItem.Item != null)
//        //                {
//        //                    foreach (KeyValuePair<string, decimal> input in newProductionItem.Item.Recipes[0].Inputs)
//        //                    {
//        //                        newProductionItem.Dependencies.Add(input.Key, input.Value);
//        //                    }
//        //                    ProductionItems.Add(newProductionItem);
//        //                    //Commented out this - because we are already adding this input below V
//        //                    //inputs.AddRange(newProductionItem.Item.Recipes[0].Inputs);
//        //                }
//        //            }
//        //        }
//        //        inputs.AddRange(targetItem.Item.Recipes[0].Inputs);

//        //        //Process each input
//        //        foreach (KeyValuePair<string, decimal> input in inputs)
//        //        {
//        //            Item? inputItem = FindItem(input.Key);
//        //            if (inputItem != null)
//        //            {
//        //                decimal inputQuantityWithRatio = input.Value * itemOutputRatio;
//        //                if (targetItem.Dependencies.Any(p => p.Key == input.Key))
//        //                {
//        //                    KeyValuePair<string, decimal>? currentInputMatch = targetItem.Dependencies.FirstOrDefault(p => p.Key == input.Key);
//        //                    if (currentInputMatch != null)
//        //                    {
//        //                        decimal value = ((KeyValuePair<string, decimal>)currentInputMatch).Value;
//        //                        currentInputMatch = new KeyValuePair<string, decimal>(input.Key, value + targetItem.Quantity);
//        //                    }
//        //                }
//        //                else
//        //                {
//        //                    targetItem.Dependencies.Add(input.Key, inputQuantityWithRatio);
//        //                }
//        //                ProductionItem newProductionItem = new(inputItem, inputQuantityWithRatio)
//        //                {
//        //                    BuildingQuantityRequired = itemOutputRatio
//        //                };
//        //                ProcessOutputItem(newProductionItem);
//        //            }
//        //        }
//        //        //If this item already exists in the production list, update the quantity for it
//        //        if (currentItemMatch != null)
//        //        {
//        //            foreach (KeyValuePair<string, decimal> dependency in targetItem.Dependencies)
//        //            {
//        //                if (currentItemMatch.Dependencies.ContainsKey(dependency.Key))
//        //                {
//        //                    currentItemMatch.Dependencies[dependency.Key] += dependency.Value;
//        //                }
//        //                else
//        //                {
//        //                    currentItemMatch.Dependencies.Add(dependency.Key, dependency.Value);
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return true;
//        //}

//        ////Find an item by name
//        //public Item? FindItem(string itemName)
//        //{
//        //    Item? result = null;
//        //    if (Items != null && Items.Count > 0)
//        //    {
//        //        foreach (Item item in Items)
//        //        {
//        //            if (item.Name == itemName)
//        //            {
//        //                result = item;
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    return result;
//        //}

//        ////Find a building by name
//        //public Building? FindBuilding(string buildingName)
//        //{
//        //    Building? result = null;
//        //    if (Buildings != null && Buildings.Count > 0)
//        //    {
//        //        foreach (Building item in Buildings)
//        //        {
//        //            if (item.Name == buildingName)
//        //            {
//        //                result = item;
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    return result;
//        //}

//        //Create the mermaid string for the production plan
//        public string ToMermaidString(bool includeImages = false)
//        {
//            string direction = "LR";
//            List<MermaidDotNet.Models.Node> nodes = [];
//            foreach (ProductionItem item in ProductionItems)
//            {
//                if (item != null && item.Item != null)
//                {
//                    string recipeName = item.Item.Recipes[0].Name;
//                    string recipeBuildingName = item.Item.Recipes[0].Building;
//                    string recipeBuildingQuantity = RoundUpAndFormat(item.BuildingQuantityRequired);
//                    string recipeBuildingImage = "";
//                    //TODO
//                    //if (AllBuildings.FindBuilding(item.Item.Recipes[0].Building) != null)
//                    //{
//                    //    recipeBuildingImage = AllBuildings.FindBuilding(item.Item.Recipes[0].Building).Image;
//                    //}
//                    string recipeText = '"' + "x" + recipeBuildingQuantity + " " + recipeBuildingName + "<br>(" + recipeName + ")" + '"';
//                    if (includeImages)
//                    {
//                        recipeText = "\"<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/" + recipeBuildingImage + "' style='max-width:100px' alt='" + recipeBuildingName + "'></span><br> x" + recipeBuildingQuantity + " " + recipeBuildingName + "<br>(" + recipeName + ")</div>\"";
//                    }
//                    if (nodes.Find(p => p.Name == recipeName) == null)
//                    {
//                        nodes.Add(new(recipeName, recipeText));
//                    }
//                    // If it's the final output, add an extra node to show the output item
//                    if (item.OutputItem == true)
//                    {
//                        string finalNode = item.Item?.Name + "_Item";
//                        string finalNodeText = RoundUpAndFormat(item.Quantity) + " " + item.Item?.Name;
//                        if (includeImages)
//                        {
//                            finalNodeText = "\"<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Items/" + item.Item?.Image + "' style='max-width:100px' alt='" + item.Item?.Name + "'></span><br> x" + RoundUpAndFormat(item.Quantity) + " " + item.Item?.Name + "</div>\"";
//                        }
//                        nodes.Add(new(finalNode, finalNodeText, MermaidDotNet.Models.Node.ShapeType.Stadium));
//                    }
//                }
//            }
//            List<MermaidDotNet.Models.Link> links = [];
//            foreach (ProductionItem item in ProductionItems)
//            {
//                if (item != null && item.Item != null)
//                {
//                    foreach (KeyValuePair<string, decimal> itemInput in item.Dependencies)
//                    {
//                        string source = itemInput.Key;
//                        string destination = item.Item.Recipes[0].Name;
//                        string text = '"' + itemInput.Key + "<br>(" + RoundUpAndFormat(itemInput.Value) + " units/min)" + '"';
//                        MermaidDotNet.Models.Link link = new(source, destination, text);
//                        if (!links.Any(g => g.SourceNode == link.SourceNode &&
//                                            g.DestinationNode == link.DestinationNode &&
//                                            g.Text == link.Text))
//                        {
//                            links.Add(new MermaidDotNet.Models.Link(source, destination, text));
//                        }
//                    }
//                    if (item.OutputItem == true)
//                    {
//                        string linkSource = item.Item.Recipes[0].Name;
//                        string linkDestination = item.Item.Name + "_Item";
//                        string linkText = '"' + item.Item.Name + "<br>(" + RoundUpAndFormat(item.Quantity) + " units/min)" + '"';
//                        links.Add(new MermaidDotNet.Models.Link(
//                                            linkSource,
//                                            linkDestination,
//                                            linkText));
//                    }
//                }
//            }
//            Flowchart flowchart = new(direction, nodes, links);
//            return flowchart.CalculateFlowchart();
//        }

//        //Round up to the nearest decimal point - if one exists, otherwise just show a whole number
//        private static string RoundUpAndFormat(decimal value)
//        {
//            if ((int)value == value)
//            {
//                return (Math.Ceiling(value * 10) / 10).ToString("0");
//            }
//            else
//            {
//                return (Math.Ceiling(value * 10) / 10).ToString("0.0");
//            }
//        }

//    }
//}
