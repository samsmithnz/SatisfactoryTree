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

            foreach (Item item in factory.TargetParts)
            {
                results.AddRange(CalculateProduction(factoryCatalog, item.Name, item.Quantity, factory.ImportedParts));
            }

            return results;
        }

        public List<Item> CalculateProduction(FactoryCatalog factoryCatalog, string partName, double quantity, List<Item> importedParts)
        {
            List<Item> results = new();
            int counter = 1;

            //Add the goal item
            Recipe? recipe = FindRecipe(factoryCatalog, partName);
            double buildingRatio = quantity / recipe.Products[0].perMin;
            results.Add(new() { Name = partName, Quantity = quantity, Ingredients = GetIngredients(factoryCatalog, partName, quantity, counter, new(), false), Building = recipe.Building.Name, BuildingQuantity = buildingRatio, Counter = counter });
            //Get the dependencies/ingredients for the goal item
            results.AddRange(GetIngredients(factoryCatalog, partName, quantity, counter, importedParts));

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
                }
            }

            //sort the dictonary back into a list
            results = resultsDictionary.Values.ToList();

            //sort the results by counter to show the goal items first and raw items last, and then part name
            results = results.OrderBy(x => x.Counter).ThenBy(x => x.Name).ToList();

            return results;
        }

        private List<Item> GetIngredients(FactoryCatalog factoryCatalog, string partName, double quantity, int counter, List<Item> importedParts, bool recursivelySearch = true)
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
                if (newRecipe != null && newRecipe.Ingredients != null)
                {
                    foreach (Ingredient ingredient in newRecipe.Ingredients)
                    {
                        // Check importedParts for this ingredient
                        Item? imported = importedParts.FirstOrDefault(ip => ip.Name == ingredient.part && ip.Quantity > 0);
                        double needed = ingredient.perMin * ratio;
                        double importedUsed = 0;

                        if (imported != null)
                        {
                            if (imported.Quantity >= needed)
                            {
                                importedUsed = needed;
                                //imported.Quantity -= needed;
                                needed = 0;
                            }
                            else
                            {
                                importedUsed = imported.Quantity;
                                needed -= imported.Quantity;
                                //imported.Quantity = 0;
                            }
                        }

                        // Only add the ingredient if there's still a need after imports
                        if (needed > 0)
                        {
                            Recipe? ingredientRecipe = FindRecipe(factoryCatalog, ingredient.part);
                            string buildingName = "";
                            double buildingRatio = 0;
                            if (ingredientRecipe != null)
                            {
                                buildingName = ingredientRecipe.Building.Name;
                                buildingRatio = needed / ingredientRecipe.Products[0].perMin;
                            }

                            Item newIngredient = new()
                            {
                                Name = ingredient.part,
                                Quantity = needed,
                                Ingredients = GetIngredients(factoryCatalog, ingredient.part, quantity, counter, new(), false),
                                Building = buildingName,
                                BuildingQuantity = buildingRatio,
                                Counter = counter
                            };

                            results.Add(newIngredient);
                            if (recursivelySearch == true)
                            {
                                results.AddRange(GetIngredients(factoryCatalog, ingredient.part, needed, counter, importedParts));
                            }
                        }
                        // If all was satisfied by imports, you may want to log or track that as well if needed
                    }
                }
            }
            return results;
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
