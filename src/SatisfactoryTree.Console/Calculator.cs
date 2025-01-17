using SatisfactoryTree.Console.OldModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Console
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<Item> Ingredients { get; set; }
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
            results.Add(new() { Name = partName, Quantity = quantity, Ingredients = new(), Counter = counter });
            //Get the dependencies/ingredients for the goal item
            results.AddRange(GetIngredients(finalData, partName, quantity, counter));

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
                        //Add this ingredient
                        results.Add(new() { Name = ingredient.part, Quantity = ingredient.perMin * ratio, Ingredients = new(), Counter = counter });
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
