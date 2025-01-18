using SatisfactoryTree.Console.OldModels;

namespace SatisfactoryTree.Console
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<Item> Ingredients { get; set; }
        public string Building { get; set; }
        public double BuildingQuantity { get; set; }

        public int Counter { get; set; }
    }

    public class Calculator
    {

        //Using a target item, calculate the total number of items needed to produce the target item
        public Calculator() { }
        public List<Item> CalculateProduction(FinalData finalData, string partName, double quantity)
        {
            List<Item> results = new();
            int counter = 1;

            //Add the goal item
            NewRecipe recipe = FindRecipe(finalData, partName);
            double buildingRatio = quantity / recipe.products[0].perMin;
            results.Add(new() { Name = partName, Quantity = quantity, Ingredients = new(), Building = recipe.building.name, BuildingQuantity = buildingRatio, Counter = counter });
            //Get the dependencies/ingredients for the goal item
            results.AddRange(GetIngredients(finalData, partName, quantity, counter));

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

        private List<Item> GetIngredients(FinalData finalData, string partName, double quantity, int counter)
        {
            List<Item> results = new();
            counter++;
            NewRecipe newRecipe = FindRecipe(finalData, partName);

            //If we have a recipe, calculate the ingredients
            if (newRecipe != null && newRecipe.products != null)
            {
                double ratio = 0;
                foreach (Product product in newRecipe.products)
                {
                    if (product.part == partName)
                    {
                        ratio = quantity / product.perMin;
                        break;
                    }
                }

                //If we have a recipe, calculate the ingredients
                if (newRecipe != null && newRecipe.ingredients != null)
                {
                    foreach (Ingredient ingredient in newRecipe.ingredients)
                    {
                        //Get this ingredient's recipe
                        NewRecipe ingredientRecipe = FindRecipe(finalData, ingredient.part);
                        string? buildingName = null;
                        double buildingRatio = 0;
                        if (ingredientRecipe != null)
                        {
                            buildingName = ingredientRecipe.building.name;
                            buildingRatio = ingredient.perMin * ratio / ingredientRecipe.products[0].perMin;
                        }

                        //Add this ingredient
                        results.Add(new() { Name = ingredient.part, Quantity = ingredient.perMin * ratio, Ingredients = new(), Building = buildingName, BuildingQuantity = buildingRatio, Counter = counter });
                        //Search for this ingredient's dependencies
                        results.AddRange(GetIngredients(finalData, ingredient.part, ingredient.perMin * ratio, counter));
                    }
                }
                ////Check to see if the part is a raw material
                //else if (finalData.items.rawResources.ContainsKey(partName))
                //{
                //    results.Add(new() { Name = partName, Quantity = quantity, Ingredients = new(), Counter = counter });
                //}
            }
            return results;
        }

        private NewRecipe FindRecipe(FinalData finalData, string partName)
        {
            foreach (NewRecipe recipe in finalData.newRecipes)
            {
                //Skip alternative recipes for now
                if (recipe != null && recipe.isAlternate == false && recipe.building.name != "converter")
                {
                    foreach (Product product in recipe.products)
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
