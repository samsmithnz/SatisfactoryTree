using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic
{
    public class Calculator
    {

        //Using a target item, calculate the total number of items needed to produce the target item
        public Calculator() { }

        public List<Item> CalculateFactoryProduction(FactoryCatalog factoryCatalog, Factory factory)
        {
            List<Item> results = new();
            foreach (ExportedItem item in factory.ExportedParts)
            {
                results.AddRange(CalculateProduction(factoryCatalog, item.Item.Name, item.Item.Quantity, factory.ImportedParts));
            }
            return results;
        }

        public List<Item> ValidateFactorySetup(FactoryCatalog factoryCatalog, Factory factory)
        {
            List<Item> results = new();
            foreach (ExportedItem item in factory.ExportedParts)
            {
                results.AddRange(ValidateProductionSetup(factoryCatalog, item.Item.Name, item.Item.Quantity, factory.ImportedParts));
            }
            return results;
        }

        public List<Item> CalculateProduction(FactoryCatalog factoryCatalog, string partName, double quantity, Dictionary<int, ImportedItem> importedParts)
        {
            List<Item> results = new();
            int counter = 1;

            //Add the goal item
            Recipe? recipe = FindRecipe(factoryCatalog, partName);
            if (recipe == null)
            {
                return results;
            }

            double buildingRatio = quantity / recipe.Products[0].perMin;

            // Create a working copy of imported parts to track usage
            Dictionary<string, double> workingImportedParts = new();
            foreach (var import in importedParts.Values)
            {
                if (import?.Item != null && !string.IsNullOrEmpty(import.Item.Name))
                {
                    if (workingImportedParts.ContainsKey(import.Item.Name))
                    {
                        workingImportedParts[import.Item.Name] += import.Item.Quantity;
                    }
                    else
                    {
                        workingImportedParts[import.Item.Name] = import.Item.Quantity;
                    }
                }
            }

            List<Item> ingredients = GetIngredients(factoryCatalog, partName, quantity, counter, new Dictionary<string, double>(), false);

            // Add the goal item WITH power calculation
            results.Add(new()
            {
                Name = partName,
                Quantity = quantity,
                Ingredients = ingredients,
                Building = recipe.Building.Name,
                BuildingQuantity = buildingRatio,
                BuildingPowerUsage = GetBuildingPower(factoryCatalog, recipe.Building.Name, buildingRatio),
                Counter = counter
            });

            //Get the dependencies/ingredients for the goal item
            results.AddRange(GetIngredients(factoryCatalog, partName, quantity, counter, workingImportedParts));

            //transfer the results list into a dictonary to combine results
            Dictionary<string, Item> resultsDictionary = new();
            foreach (Item item in results)
            {
                //if the item doesn't exist in the dictionary, add it
                if (!resultsDictionary.ContainsKey(item.Name))
                {
                    resultsDictionary.Add(item.Name, item);
                }
                else
                {
                    //if the item exists in the dictionary, add the quantity to the existing item
                    resultsDictionary[item.Name].Quantity += item.Quantity;
                    // Also combine building quantities and power usage
                    resultsDictionary[item.Name].BuildingQuantity += item.BuildingQuantity;
                    resultsDictionary[item.Name].BuildingPowerUsage += item.BuildingPowerUsage;
                }
            }

            //sort the dictonary back into a list
            results = resultsDictionary.Values.ToList();

            //sort the results by counter to show the goal items first and raw items last, and then part name
            results = results.OrderBy(x => x.Counter).ThenBy(x => x.Name).ToList();

            //Sort back through the counter to ensure that raw materials have the lowest count, and end products have the highest count. 
            results = SortItems(results);

            // Update the imported parts with actual usage
            UpdateImportedPartsUsage(importedParts, workingImportedParts);

            return results;
        }

        public List<Item> ValidateProductionSetup(FactoryCatalog factoryCatalog, string partName, double quantity, Dictionary<int, ImportedItem> importedParts)
        {
            List<Item> results = new();
            int counter = 1;

            // Find recipe for the target part
            Recipe? recipe = FindRecipe(factoryCatalog, partName);
            if (recipe == null)
            {
                return results;
            }

            double buildingRatio = quantity / recipe.Products[0].perMin;

            // Create a working copy of imported parts to track availability
            Dictionary<string, double> availableImports = new();
            foreach (var import in importedParts.Values)
            {
                if (import?.Item != null && !string.IsNullOrEmpty(import.Item.Name))
                {
                    if (availableImports.ContainsKey(import.Item.Name))
                    {
                        availableImports[import.Item.Name] += import.Item.Quantity;
                    }
                    else
                    {
                        availableImports[import.Item.Name] = import.Item.Quantity;
                    }
                }
            }

            // Validate immediate ingredients only (no recursion to raw materials)
            List<Item> immediateIngredients = ValidateImmediateIngredients(factoryCatalog, partName, quantity, counter, availableImports);

            // Add the goal item with validation info
            Item goalItem = new()
            {
                Name = partName,
                Quantity = quantity,
                Ingredients = immediateIngredients,
                Building = recipe.Building.Name,
                BuildingQuantity = buildingRatio,
                BuildingPowerUsage = GetBuildingPower(factoryCatalog, recipe.Building.Name, buildingRatio),
                Counter = counter
            };

            // Check if any immediate ingredients are missing and populate MissingIngredients
            foreach (var ingredient in immediateIngredients)
            {
                if (ingredient.HasMissingIngredients || ingredient.Quantity > 0) // If quantity > 0, it means it's not fully satisfied by imports
                {
                    goalItem.MissingIngredients.AddRange(ingredient.MissingIngredients);
                    if (ingredient.Quantity > 0 && !goalItem.MissingIngredients.Contains(ingredient.Name))
                    {
                        goalItem.MissingIngredients.Add(ingredient.Name);
                    }
                }
            }

            results.Add(goalItem);
            results.AddRange(immediateIngredients);

            return results;
        }

        private List<Item> ValidateImmediateIngredients(FactoryCatalog factoryCatalog, string partName, double quantity, int counter, Dictionary<string, double> availableImports)
        {
            List<Item> results = new();
            counter++;
            Recipe? newRecipe = FindRecipe(factoryCatalog, partName);

            // If we have a recipe, validate the immediate ingredients
            if (newRecipe != null && newRecipe.Products != null)
            {
                double ratio = 0;
                foreach (Product product in newRecipe.Products)
                {
                    if (product.part == partName)
                    {
                        ratio = quantity / product.perMin;
                        break;
                    }
                }

                // Validate each immediate ingredient
                if (newRecipe.Ingredients != null)
                {
                    foreach (Ingredient ingredient in newRecipe.Ingredients)
                    {
                        double needed = ingredient.perMin * ratio;
                        double remainingNeed = needed;

                        // Check if we have imports available for this ingredient
                        if (availableImports.ContainsKey(ingredient.part) && availableImports[ingredient.part] > 0)
                        {
                            double availableFromImport = availableImports[ingredient.part];
                            double usedFromImport = Math.Min(remainingNeed, availableFromImport);

                            // Update the available import quantity
                            availableImports[ingredient.part] -= usedFromImport;
                            if (availableImports[ingredient.part] < 0.001) // Handle floating point precision
                                availableImports[ingredient.part] = 0;

                            // Reduce the needed quantity by what we got from imports
                            remainingNeed -= usedFromImport;
                        }

                        // Create ingredient item with validation info
                        Recipe? ingredientRecipe = FindRecipe(factoryCatalog, ingredient.part);
                        string buildingName = ingredientRecipe?.Building.Name ?? string.Empty;
                        double buildingRatio = 0;
                        
                        if (ingredientRecipe != null && remainingNeed > 0.001)
                        {
                            buildingRatio = remainingNeed / ingredientRecipe.Products[0].perMin;
                        }

                        Item ingredientItem = new()
                        {
                            Name = ingredient.part,
                            Quantity = remainingNeed, // This represents the unmet need
                            Ingredients = new List<Item>(), // Don't recurse - only immediate validation
                            Building = buildingName,
                            BuildingQuantity = buildingRatio,
                            BuildingPowerUsage = GetBuildingPower(factoryCatalog, buildingName, buildingRatio),
                            Counter = counter
                        };

                        // If there's still remaining need, mark this ingredient as missing
                        if (remainingNeed > 0.001)
                        {
                            ingredientItem.MissingIngredients.Add(ingredient.part);
                        }

                        results.Add(ingredientItem);
                    }
                }
            }
            return results;
        }

        private void UpdateImportedPartsUsage(Dictionary<int, ImportedItem> importedParts, Dictionary<string, double> workingImportedParts)
        {
            foreach (ImportedItem import in importedParts.Values)
            {
                if (import?.Item != null && !string.IsNullOrEmpty(import.Item.Name))
                {
                    double originalQuantity = import.Item.Quantity;
                    double remainingQuantity;
                    if (workingImportedParts.ContainsKey(import.Item.Name))
                    {
                        remainingQuantity = (double)workingImportedParts[import.Item.Name];
                    }
                    else
                    {
                        remainingQuantity = (double)originalQuantity;
                    }

                    double usedQuantity = originalQuantity - remainingQuantity;
                    import.PartQuantityImported = Math.Max(0, usedQuantity);
                }
            }
        }

        private List<Item> GetIngredients(FactoryCatalog factoryCatalog, string partName, double quantity, int counter, Dictionary<string, double> availableImports, bool recursivelySearch = true)
        {
            List<Item> results = new();
            counter++;
            Recipe? newRecipe = FindRecipe(factoryCatalog, partName);

            //If we have a recipe, calculate the ingredients
            if (newRecipe != null && newRecipe.Products != null)
            {
                double ratio = 0;
                foreach (Product product in newRecipe.Products)
                {
                    if (product.part == partName)
                    {
                        ratio = quantity / product.perMin;
                        break;
                    }
                }

                //If we have a recipe, calculate the ingredients
                if (newRecipe.Ingredients != null)
                {
                    foreach (Ingredient ingredient in newRecipe.Ingredients)
                    {
                        double needed = ingredient.perMin * ratio;

                        // Check if we have imports available for this ingredient
                        if (availableImports.ContainsKey(ingredient.part) && availableImports[ingredient.part] > 0)
                        {
                            double availableFromImport = availableImports[ingredient.part];
                            double usedFromImport = Math.Min(needed, availableFromImport);

                            // Update the available import quantity
                            availableImports[ingredient.part] -= usedFromImport;
                            if (availableImports[ingredient.part] < 0.001) // Handle floating point precision
                                availableImports[ingredient.part] = 0;

                            // Reduce the needed quantity by what we got from imports
                            needed -= usedFromImport;
                        }

                        // Only add the ingredient if there's still a need after imports
                        if (needed > 0.001) // Use small threshold to handle floating point precision
                        {
                            Recipe? ingredientRecipe = FindRecipe(factoryCatalog, ingredient.part);
                            if (ingredientRecipe != null)
                            {
                                string buildingName = ingredientRecipe.Building.Name;
                                double buildingRatio = needed / ingredientRecipe.Products[0].perMin;

                                Item newIngredient = new()
                                {
                                    Name = ingredient.part,
                                    Quantity = needed,
                                    Ingredients = GetIngredients(factoryCatalog, ingredient.part, needed, counter, new Dictionary<string, double>(), false),
                                    Building = buildingName,
                                    BuildingQuantity = buildingRatio,
                                    BuildingPowerUsage = GetBuildingPower(factoryCatalog, buildingName, buildingRatio),
                                    Counter = counter
                                };

                                results.Add(newIngredient);
                                if (recursivelySearch == true)
                                {
                                    results.AddRange(GetIngredients(factoryCatalog, ingredient.part, needed, counter, availableImports));
                                }
                            }
                        }
                    }
                }
            }
            return results;
        }

        private double GetBuildingPower(FactoryCatalog factoryCatalog, string building, double quantity)
        {
            double buildingPower = 0;
            foreach (KeyValuePair<string, double> item in factoryCatalog.Buildings)
            {
                if (building == item.Key)
                {
                    buildingPower = item.Value;
                    break;
                }
            }
            //break the quantity into whole and fractional parts
            int wholeBuildingCount = (int)Math.Floor(quantity);
            double fractionalBuildingCount = quantity - wholeBuildingCount;
            //Power usage = initial power usage x (clock speed / 100)1.321928;
            double result = (buildingPower * wholeBuildingCount) + (buildingPower * Math.Pow(fractionalBuildingCount, 1.321928));
            //round to 3 decimal places
            result = (double)Math.Round((decimal)result, 3);
            return result;
        }

        private List<Item> SortItems(List<Item> results)
        {
            // Create a lookup for dependency counting
            var itemLookup = results.ToDictionary(item => item.Name, item => item);
            var dependencyDepth = new Dictionary<string, int>();

            // Calculate the dependency depth for each item recursively
            int CalculateDepth(string itemName, HashSet<string> visiting)
            {
                if (dependencyDepth.ContainsKey(itemName))
                {
                    return dependencyDepth[itemName];
                }

                if (visiting.Contains(itemName))
                {
                    return 0; // Circular dependency, treat as raw material
                }

                if (!itemLookup.ContainsKey(itemName))
                {
                    return 0; // Item not found, treat as raw material
                }

                Item item = itemLookup[itemName];

                // If item has no ingredients or empty ingredients list, it's a raw material (depth 0)
                if (item.Ingredients == null || item.Ingredients.Count == 0)
                {
                    dependencyDepth[itemName] = 0;
                    return 0;
                }

                visiting.Add(itemName);
                int maxChildDepth = 0;

                foreach (var ingredient in item.Ingredients)
                {
                    int childDepth = CalculateDepth(ingredient.Name, visiting);
                    maxChildDepth = Math.Max(maxChildDepth, childDepth);
                }

                visiting.Remove(itemName);
                int depth = maxChildDepth + 1;
                dependencyDepth[itemName] = depth;
                return depth;
            }

            // Calculate depth for all items
            foreach (Item item in results)
            {
                CalculateDepth(item.Name, new HashSet<string>());
            }

            // Update counter values based on dependency depth
            // Raw materials (depth 0) get counter 1, next level gets counter 2, etc.
            foreach (Item item in results)
            {
                if (dependencyDepth.ContainsKey(item.Name))
                {
                    item.Counter = dependencyDepth[item.Name] + 1;
                }
                else
                {
                    item.Counter = 1; // Default to raw material level
                }
            }

            // Sort by counter (raw materials first), then by name for consistent ordering
            return results.OrderByDescending(x => x.Counter).ThenBy(x => x.Name).ToList();
        }

        private Recipe? FindRecipe(FactoryCatalog finalData, string partName)
        {
            foreach (Recipe recipe in finalData.Recipes)
            {
                //Skip alternative recipes for now
                if (recipe != null && recipe.IsAlternate == false && recipe.Building.Name != "converter")
                {
                    foreach (Product product in recipe.Products)
                    {
                        if (product.part == partName)
                        {
                            return recipe;
                        }
                    }
                }
            }

            return null;
        }

    }
}
