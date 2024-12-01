using SatisfactoryTree.Console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SatisfactoryTree.Console
{
    public class Recipes
    {
        public static List<Recipe> GetProductionRecipes(List<dynamic> data, Dictionary<string, double> producingBuildings)
        {
            var recipes = new List<Recipe>();

            var filteredData = data.Where(entry => entry.Classes != null)
                                   .SelectMany<dynamic, dynamic>(entry => (IEnumerable<dynamic>)entry.Classes)
                                   .Where(recipe =>
                                   {
                                       // Filter out recipes that don't have a producing building
                                       if (recipe.mProducedIn == null) return false;
                                       // Filter out recipes that are in the blacklist (typically items produced by the Build Gun)
                                       if (Common.Blacklist.All(building => recipe.mProducedIn.Contains(building))) return false;

                                       // Extract all producing buildings
                                       var rawBuildingKeys = new List<dynamic>();
                                       foreach (Match match in Regex.Matches(recipe.mProducedIn, @"\/([^/]+)\."))
                                       {
                                           rawBuildingKeys.Add((dynamic)match.Value);
                                       }
                                       if (!rawBuildingKeys.Any()) return false;

                                       // Process all buildings and check if any match the producingBuildings map
                                       var validBuilding = rawBuildingKeys.Any((Func<dynamic, bool>)(rawBuilding =>
                                       {
                                           var buildingKey = rawBuilding.Replace("/", "").Replace(".", "").ToLower().Replace("build_", "");
                                           return producingBuildings.ContainsKey(buildingKey);
                                       }));

                                       return validBuilding;
                                   });

            foreach (var recipe in filteredData)
            {
                var ingredients = recipe.mIngredients != null
                    ? Regex.Matches(recipe.mIngredients, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)")
                            .Cast<Match>()
                            .Select((Func<Match, Ingredient>)(match =>
                            {
                                var partName = match.Groups[1].Value;
                                var amount = int.Parse(match.Groups[2].Value);
                                if (Common.IsFluid(partName))
                                {
                                    amount /= 1000;
                                }
                                var perMin = recipe.mManufactoringDuration != null && amount > 0
                                    ? (60 / double.Parse(recipe.mManufactoringDuration)) * amount
                                    : 0;

                                return new Ingredient
                                {
                                    Part = partName,
                                    Amount = amount,
                                    PerMin = perMin
                                };
                            }))
                            .Where((Func<Ingredient, bool>)(ingredient => ingredient != null))
                            .ToList()
                    : new List<Ingredient>();

                // Parse mProduct to extract all products
                var productMatches = Regex.Matches(recipe.mProduct, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)")
                                          .Cast<Match>()
                                          .ToList();

                // Exception for automated miner recipes - as the product is a BP_ItemDescriptor
                if (recipe.ClassName == "Recipe_Alternate_AutomatedMiner_C")
                {
                    productMatches = Regex.Matches(recipe.mProduct, @"ItemClass="".*?\/BP_ItemDescriptor(.*?)\.BP_ItemDescriptor.*?"",Amount=(\d+)")
                                          .Cast<Match>()
                                          .ToList();
                }

                var products = new List<Product>();
                foreach (var match in productMatches)
                {
                    var productName = match.Groups[1].Value;
                    var amount = int.Parse(match.Groups[2].Value);
                    if (Common.IsFluid(productName))
                    {
                        amount /= 1000;  // Divide by 1000 for liquid/gas amounts
                    }
                    var perMin = recipe.mManufactoringDuration != null && amount > 0
                        ? (60 / double.Parse(recipe.mManufactoringDuration)) * amount
                        : 0;

                    products.Add(new Product
                    {
                        Part = productName,
                        Amount = amount,
                        PerMin = perMin,
                        IsByProduct = products.Count > 0
                    });
                }

                // Extract all producing buildings
                var producedInMatches = Regex.Matches(recipe.mProducedIn, @"\/(\w+)\/(\w+)\.(\w+)_C")
                                             .Cast<Match>()
                                             .Select((Func<Match, string>)(m => m.Groups[2].Value.Replace("build_", "").ToLower()))
                                             .Where((Func<string, bool>)(building => building != null && !new[] { "bp_workbenchcomponent", "bp_workshopcomponent", "factorygame" }.Contains(building)))
                                             .ToList();

                // Calculate power per building and choose the most relevant one
                double powerPerBuilding = 0;
                string selectedBuilding = null;

                if (producedInMatches.Any())
                {
                    powerPerBuilding = producedInMatches.Sum((Func<string, double>)(building =>
                    {
                        if (producingBuildings.ContainsKey(building))
                        {
                            var buildingPower = producingBuildings[building];
                            selectedBuilding = selectedBuilding ?? building; // Set the first valid building as selected
                            return buildingPower; // Add power for this building
                        }
                        return 0;
                    }));
                }

                // Calculate variable power for recipes that need it
                double? lowPower = double.TryParse(recipe.mVariablePowerConsumptionConstant, out double lp) ? lp : (double?)null;
                double? highPower = double.TryParse(recipe.mVariablePowerConsumptionFactor, out double hp) ? hp : (double?)null;
                if (selectedBuilding == "hadroncollider" || selectedBuilding == "converter" || selectedBuilding == "quantumencoder")
                {
                    // Get the power from the recipe instead of the building
                    lowPower = double.TryParse(recipe.mVariablePowerConsumptionConstant, out lp) ? lp : (double?)null;
                    highPower = double.TryParse(recipe.mVariablePowerConsumptionFactor, out hp) ? hp : (double?)null;
                    // Calculate the average power
                    if (lowPower.HasValue && highPower.HasValue)
                    {
                        powerPerBuilding = (lowPower.Value + highPower.Value) / 2;
                    }
                }

                // Create building object with the selected building and calculated power
                var building = new Building
                {
                    Name = selectedBuilding ?? "", // Use the first valid building, or empty string if none
                    Power = powerPerBuilding
                };

                if (lowPower.HasValue && highPower.HasValue)
                {
                    building.MinPower = lowPower;
                    building.MaxPower = highPower;
                }

                recipes.Add(new Recipe
                {
                    Id = recipe.ClassName?.Replace("Recipe_", "")?.Replace("_C", ""),
                    DisplayName = recipe.mDisplayName,
                    Ingredients = ingredients,
                    Products = products,
                    Building = building,
                    IsAlternate = recipe.mDisplayName.Contains("Alternate"),
                    IsFicsmas = Common.IsFicsmas(recipe.mDisplayName)
                });
            }

            return recipes.OrderBy(r => r.DisplayName).ToList();
        }

        public static List<PowerGenerationRecipe> GetPowerGeneratingRecipes(List<dynamic> data, PartDataInterface parts, Dictionary<string, double> producingBuildings)
        {
            var recipes = new List<PowerGenerationRecipe>();

            var filteredData = data.Where(entry => entry.Classes != null)
                                   .SelectMany<dynamic, dynamic>(entry => (IEnumerable<dynamic>)entry.Classes)
                                   .Where(recipe =>
                                   {
                                       // Filter out recipes that don't have a producing building
                                       if (recipe.mProducedIn == null) return false;
                                       // Filter out recipes that are in the blacklist (typically items produced by the Build Gun)
                                       if (Common.Blacklist.All(building => recipe.mProducedIn.Contains(building))) return false;

                                       // Extract all producing buildings
                                       var rawBuildingKeys = new List<dynamic>();
                                       foreach (Match match in Regex.Matches(recipe.mProducedIn, @"\/([^/]+)\."))
                                       {
                                           rawBuildingKeys.Add((dynamic)match.Value);
                                       }
                                       if (!rawBuildingKeys.Any()) return false;

                                       // Process all buildings and check if any match the producingBuildings map
                                       var validBuilding = rawBuildingKeys.Any((Func<dynamic, bool>)(rawBuilding =>
                                       {
                                           var buildingKey = rawBuilding.Replace("/", "").Replace(".", "").ToLower().Replace("build_", "");
                                           return producingBuildings.ContainsKey(buildingKey);
                                       }));

                                       return validBuilding;
                                   });

            foreach (var recipe in filteredData)
            {
                var building = new Building
                {
                    Name = recipe.mDisplayName.Replace(" ", ""), // Use the first valid building, or empty string if none
                    Power = Math.Round(recipe.mPowerProduction) // generated power - can be rounded to the nearest whole number (all energy numbers are whole numbers)
                };

                var supplementalRatio = double.Parse(recipe.mSupplementalToPowerRatio);
                // 1. Generator MW generated. This is an hourly value.
                // 2. Divide by 60, to get the minute value
                // 3. Now calculate the MJ, using the MJ->MW constant (1/3600), (https://en.wikipedia.org/wiki/Joule#Conversions)
                // 4. Now divide this number by the part energy to calculate how many pieces per min
                var powerMJ = (recipe.mPowerProduction / 60) / (1 / 3600.0);

                var fuels = recipe.mFuel is IEnumerable<dynamic> ? (IEnumerable<dynamic>)recipe.mFuel : new List<dynamic> { recipe.mFuel };
                foreach (var fuel in fuels)
                {
                    var fuelItem = new Fuel
                    {
                        PrimaryFuel = Common.GetPartName(fuel.mFuelClass),
                        SupplementalResource = fuel.mSupplementalResourceClass != null ? Common.GetPartName(fuel.mSupplementalResourceClass) : "",
                        ByProduct = fuel.mByproduct != null ? Common.GetPartName(fuel.mByproduct) : "",
                        ByProductAmount = fuel.mByproductAmount != null ? double.Parse(fuel.mByproductAmount) : 0
                    };

                    // Find the part for the primary fuel
                    var extractedPartText = Common.GetPartName(fuelItem.PrimaryFuel);
                    var primaryFuelPart = parts.Parts[extractedPartText];
                    double primaryPerMin = 0;
                    if (primaryFuelPart.EnergyGeneratedInMJ > 0)
                    {
                        // The rounding here is important to remove floating point errors that appear with some types
                        // (this is step 4 from above)
                        primaryPerMin = Math.Round(powerMJ / primaryFuelPart.EnergyGeneratedInMJ, 4);
                    }
                    double primaryAmount = 0;
                    if (primaryPerMin > 0)
                    {
                        primaryAmount = primaryPerMin / 60;

                        var ingredients = new List<Ingredient>
                                                {
                                                    new Ingredient
                                                    {
                                                        Part = fuelItem.PrimaryFuel,
                                                        Amount = (int)primaryAmount,
                                                        PerMin = primaryPerMin
                                                    }
                                                };

                        if (!string.IsNullOrEmpty(fuelItem.SupplementalResource) && supplementalRatio > 0)
                        {
                            ingredients.Add(new Ingredient
                            {
                                Part = fuelItem.SupplementalResource,
                                Amount = (int)((3 / 50.0) * supplementalRatio * building.Power / 60),
                                PerMin = (3 / 50.0) * supplementalRatio * building.Power // Calculate the ratio of the supplemental resource to the primary fuel
                            });
                        }

                        var products = new List<Product>();
                        if (!string.IsNullOrEmpty(fuelItem.ByProduct))
                        {
                            products.Add(new Product
                            {
                                Part = fuelItem.ByProduct,
                                Amount = (int)(fuelItem.ByProductAmount / 60),
                                PerMin = fuelItem.ByProductAmount,
                                IsByProduct = true
                            });
                        }

                        recipes.Add(new PowerGenerationRecipe
                        {
                            Id = Common.GetRecipeName(recipe.ClassName) + '_' + fuelItem.PrimaryFuel,
                            DisplayName = recipe.mDisplayName + " (" + primaryFuelPart.Name + ")",
                            Ingredients = ingredients,
                            Products = products,
                            Building = building
                        });
                    }
                }
            }

            return recipes.OrderBy(r => r.DisplayName).ToList();
        }
    }
}
