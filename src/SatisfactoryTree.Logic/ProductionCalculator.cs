using SatisfactoryTree.Logic.Extraction.ExtractionModels;
using SatisfactoryTree.Logic;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace SatisfactoryTree.Logic
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

    public class ProductionCalculator
    {
        private readonly HttpClient _httpClient;
        public FinalData _finalData { get; set; }

        //Using a target item, calculate the total number of items needed to produce the target item
        public ProductionCalculator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InitializeAsync()
        {
            _finalData = await _httpClient.GetFromJsonAsync<FinalData>("content/gameData.json");

        }

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

        public static List<Models.Product> GetProducts()
        {
            List<Models.Product> products = new()
            {
                new Models.Product("ironPlate", "iron-plate", "ironPlate", 30),
                new Models.Product("ironPlate", "iron-plate", "alt_SteelPlate", 3),
                new Models.Product("ironIngot", "iron-ingot", "ironIngot", 45)
            };
            return products;
        }

        public static List<string> GetImports()
        {
            List<string> imports = new() { "Hi", "Bye" };
            return imports;
        }

        public static List<string> GetStorages()
        {
            List<string> storages = new() { "Hi", "Bye" };
            return storages;
        }

        public static List<Models.Part> GetParts()
        {
            List<Models.Part> parts = new() { new Models.Part("ironIngot", "Iron Ingot"), new Models.Part("ironPlate", "Iron Plate") };
            //List<Models.Part> parts = new();
            //if (FinalData != null)
            //{
            //    foreach (KeyValuePair<string, Part> item in FinalData.items.parts)
            //    {
            //        parts.Add(new Models.Part(item.Key, item.Value.name));
            //    }
            //}
            return parts;
        }

        public static List<Models.Recipe> GetRecipes()
        {
            List<Models.Recipe> recipes = new()
            {
                new Models.Recipe("ironIngot", "ironIngot", "Iron Ingots", new()
                {
                    new("ironore", "Iron Ore", "iron-ore", 30)
                }, 30),
                new Models.Recipe("ironPlate", "ironPlate", "Iron Plates", new()
                {
                    new("ironIngot", "Iron Ingot", "iron-ingot", 30)
                }, 20),
                new Models.Recipe("ironPlate", "alt_SteelPlate", "Alt: Steel Plates", new()
                {
                    new("ironIngot", "Iron Ingot", "iron-ingot", 15),
                    new("steelIngot", "Steel Ingot", "steel-ingot", 15)
                }, 20)
            };
            return recipes;
        }



    }
}
