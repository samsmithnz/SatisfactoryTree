using SatisfactoryTree.Console.OldModels;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SatisfactoryTree.Console
{
    public class Recipes
    {
        private static readonly string[] sourceArray = ["bp_workbenchcomponent", "bp_workshopcomponent", "factorygame"];

        public static List<Recipe> GetProductionRecipes(List<JsonElement> data, Dictionary<string, double> producingBuildings)
        {
            List<Recipe> recipes = new();

            foreach (JsonElement entry in data)
            {
                //Debug.Write(entry.ToString());
                string? producedIn = entry.TryGetProperty("mProducedIn", out JsonElement mProducedIn) ? mProducedIn.GetString() : string.Empty;
                string? displayName = entry.TryGetProperty("mDisplayName", out JsonElement mDisplayName) ? mDisplayName.GetString() : string.Empty;
                if (string.IsNullOrEmpty(producedIn) || Common.Blacklist.Contains(producedIn))
                {
                    continue;
                }
                string className = entry.GetProperty("ClassName").ToString();
                string? ingredientsJSON = entry.TryGetProperty("mIngredients", out JsonElement mIngredients) ? mIngredients.GetString() : string.Empty;
                string? productsJSON = entry.TryGetProperty("mProduct", out JsonElement mProduct) ? mProduct.GetString() : string.Empty;
                string? manufacturingDurationJSON = entry.TryGetProperty("mManufactoringDuration", out JsonElement mManufactoringDuration) ? mManufactoringDuration.GetString() : string.Empty;
                double manufacturingDuration = 0;
                if (manufacturingDurationJSON != null)
                {
                    double.TryParse(manufacturingDurationJSON.ToString(), out manufacturingDuration);
                }

                List<Ingredient> ingredients = new();
                if (ingredientsJSON != null && ingredientsJSON.Length > 0)
                {
                    MatchCollection ingredientMatches = Regex.Matches(ingredientsJSON, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)");
                    if (ingredientMatches != null)
                    {
                        foreach (Match match in ingredientMatches)
                        {
                            string partName = match.Groups[1].Value;
                            double partAmount = 0;
                            double.TryParse(match.Groups[2].Value, out partAmount);
                            if (Common.IsFluid(partName))
                            {
                                partAmount = partAmount / 1000;
                            }
                            double perMin = 0;
                            if (manufacturingDuration > 0 && partAmount > 0)
                            {
                                perMin = (60 / manufacturingDuration) * partAmount;
                            }
                            ingredients.Add(new()
                            {
                                part = partName,
                                amount = partAmount,
                                perMin = perMin
                            });
                        }
                    }
                }


                List<Product> products = new();
                if (productsJSON != null && productsJSON.Length > 0)
                {
                    MatchCollection productMatches;
                    if (className == "Recipe_Alternate_AutomatedMiner_C")
                    {
                        // BP_ItemDescriptorPortableMiner_C
                        productMatches = Regex.Matches(productsJSON, @"ItemClass="".*?\/BP_ItemDescriptor(.*?)\.BP_ItemDescriptor.*?"",Amount=(\d+)");
                    }
                    else
                    {
                        productMatches = Regex.Matches(productsJSON, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)");
                    }
                    if (productMatches != null)
                    {
                        foreach (Match match in productMatches)
                        {
                            string partName = match.Groups[1].Value;
                            double partAmount = 0;
                            double.TryParse(match.Groups[2].Value, out partAmount);
                            if (Common.IsFluid(partName))
                            {
                                partAmount = partAmount / 1000;
                            }
                            double perMin = 0;
                            if (manufacturingDuration > 0 && partAmount > 0)
                            {
                                perMin = (60 / manufacturingDuration) * partAmount;
                            }
                            products.Add(new()
                            {
                                part = partName,
                                amount = partAmount,
                                perMin = perMin,
                                isByProduct = products.Count > 0
                            });
                        }
                    }
                }
             

                // Extract all producing buildings
                var producedInMatches = Regex.Matches(producedIn, @"Build_\w+_C");

                // Calculate power per building and choose the most relevant one
                double powerPerBuilding = 0;
                string? selectedBuilding = null;

                if (producedInMatches.Any())
                {
                    foreach (object? buildingMatch in producedInMatches)
                    {
                        if (buildingMatch != null)
                        {
                            string building2 = buildingMatch.ToString();
                            building2 = Common.GetBuildingName(building2).ToLower();
                            if (producingBuildings.ContainsKey(building2))
                            {
                                double buildingPower = producingBuildings[building2];
                                if (selectedBuilding == null)
                                {
                                    selectedBuilding = building2; // Set the first valid building as selected
                                }
                                powerPerBuilding += buildingPower; // Add power for this building
                            }
                        }
                    }
                }

                // Calculate variable power for recipes that need it
                double? lowPower = null;
                double? highPower = null;
                if (selectedBuilding == "hadroncollider" || selectedBuilding == "converter" || selectedBuilding == "quantumencoder")
                {
                    // Get the power from the recipe instead of the building
                    double lowPowerTemp = 0;
                    double highPowerTemp = 0;
                    string? lowPowerJson = entry.GetProperty("mVariablePowerConsumptionConstant").ToString();
                    string? highPowerJson = entry.GetProperty("mVariablePowerConsumptionFactor").ToString();

                    if (lowPowerJson != null)
                    {
                        double.TryParse(lowPowerJson.ToString(), out lowPowerTemp);
                        lowPower = lowPowerTemp;
                    }
                    if (highPowerJson != null)
                    {
                        double.TryParse(highPowerJson.ToString(), out highPowerTemp);
                        highPower = highPowerTemp;
                    }

                    // Calculate the average power
                    if (lowPower != null && highPower != null)
                    {
                        powerPerBuilding = (double)((lowPower + highPower) / 2);
                    }

                }

                // Create building object with the selected building and calculated power
                Building building = new Building
                {
                    name = selectedBuilding ?? "", // Use the first valid building, or empty string if none
                    power = powerPerBuilding
                };

                if (lowPower.HasValue && highPower.HasValue)
                {
                    building.minPower = lowPower;
                    building.maxPower = highPower;
                }

                //if (blacklist. producedIn)
                recipes.Add(new Recipe
                {
                    id = Common.GetRecipeName(className),
                    displayName = displayName,
                    ingredients = ingredients,
                    products = products,
                    building = building,
                    isAlternate = displayName.Contains("Alternate"),
                    isFicsmas = Common.IsFicsmas(displayName)
                });
            }

            return recipes.OrderBy(r => r.displayName).ToList();
        }

        public static List<PowerGenerationRecipe> GetPowerGeneratingRecipes(List<JsonElement> data, PartDataInterface parts, Dictionary<string, double> producingBuildings)
        {
            var recipes = new List<PowerGenerationRecipe>();

            foreach (JsonElement entry in data)
            {
                string className = entry.GetProperty("ClassName").ToString();
                string? displayName = entry.TryGetProperty("mDisplayName", out JsonElement mDisplayName) ? mDisplayName.GetString() : string.Empty;
                //string? producedIn = entry.TryGetProperty("mProducedIn", out JsonElement mProducedIn) ? mProducedIn.GetString() : string.Empty;
                //string? products = entry.TryGetProperty("mProduct", out JsonElement mProduct) ? mProduct.GetString() : string.Empty;
                //string? ingredients = entry.TryGetProperty("mIngredients", out JsonElement mIngredients) ? mIngredients.GetString() : string.Empty;

                string? powerProductionJSON = entry.TryGetProperty("mPowerProduction", out JsonElement mPowerProduction) ? mPowerProduction.GetString() : string.Empty;
                double powerProduction = 0;
                if (powerProductionJSON != null)
                {
                    double.TryParse(powerProductionJSON.ToString(), out powerProduction);
                }
                string? supplementalToPowerRatioJSON = entry.TryGetProperty("mSupplementalToPowerRatio", out JsonElement mSupplementalToPowerRatio) ? mSupplementalToPowerRatio.GetString() : string.Empty;
                double supplementalToPowerRatio = 0;
                if (supplementalToPowerRatioJSON != null)
                {
                    double.TryParse(supplementalToPowerRatioJSON.ToString(), out supplementalToPowerRatio);
                }

                //string? fuelJSON = entry.TryGetProperty("mFuel", out JsonElement mFuel) ? mFuel.GetString() : string.Empty;
                JsonElement? fuelJSON = entry.TryGetProperty("mFuel", out JsonElement mFuel) ? mFuel : (JsonElement?)null;

                Building building = new()
                {
                    name = Common.GetPowerBuildingName(className), // Use the first valid building, or empty string if none
                    power = Math.Round(powerProduction) // generated power - can be rounded to the nearest whole number (all energy numbers are whole numbers)
                };

                double supplementalRatio = supplementalToPowerRatio;
                // 1. Generator MW generated. This is an hourly value.
                // 2. Divide by 60, to get the minute value
                // 3. Now calculate the MJ, using the MJ->MW constant (1/3600), (https://en.wikipedia.org/wiki/Joule#Conversions)
                // 4. Now divide this number by the part energy to calculate how many pieces per min
                double burnRateMJ = (powerProduction / 60d) / (1d / 3600d);

                //1List<Ingredient> ingredients = new();
                if (fuelJSON.HasValue && fuelJSON.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement fuel in fuelJSON.Value.EnumerateArray())
                    {

                        string? primaryFuel = fuel.TryGetProperty("mFuelClass", out JsonElement mFuelClass) ? mFuelClass.GetString() : string.Empty;
                        string primaryFuelName = Common.GetPartName(primaryFuel);
                        string? byProductAmountJSON = fuel.TryGetProperty("mByproductAmount", out JsonElement mByproductAmount) ? mByproductAmount.GetString() : string.Empty;
                        double byProductAmount = 0;
                        if (byProductAmountJSON != null)
                        {
                            double.TryParse(byProductAmountJSON.ToString(), out byProductAmount);
                        }
                        Part primaryFuelPart = parts.parts[primaryFuelName];
                        double burnDurationInMins = primaryFuelPart.energyGeneratedInMJ / burnRateMJ;
                        double burnDurationInS = burnDurationInMins * 60; // Convert to seconds
                        Fuel fuelItem = new()
                        {
                            primaryFuel = primaryFuelName,
                            supplementaryFuel = fuel.TryGetProperty("mSupplementalResourceClass", out JsonElement mSupplementalResourceClass) ? Common.GetPartName(mSupplementalResourceClass.GetString()) : "",
                            byProduct = fuel.TryGetProperty("mByproduct", out JsonElement mByproduct) ? Common.GetPartName(mByproduct.GetString()) : "",
                            byProductAmount = byProductAmount,
                            byProductAmountPerMin = byProductAmount / burnDurationInMins,
                            burnDurationInS = burnDurationInS
                        };

                        // Find the part for the primary fuel
                        double primaryPerMin = 0;
                        if (primaryFuelPart.energyGeneratedInMJ > 0)
                        {
                            // The rounding here is important to remove floating point errors that appear with some types
                            // (this is step 4 from above)
                            primaryPerMin = Math.Round(burnRateMJ / primaryFuelPart.energyGeneratedInMJ, 5);
                        }
                        double primaryAmount = 0;
                        if (primaryPerMin > 0)
                        {
                            primaryAmount = primaryPerMin / 60d;

                            List<PowerIngredient> ingredients = new()
                            {
                                new()
                                {
                                    part = fuelItem.primaryFuel,
                                    perMin = primaryPerMin,
                                    mwPerItem = building.power / primaryPerMin
                                }
                            };

                            if (!string.IsNullOrEmpty(fuelItem.supplementaryFuel) && supplementalRatio > 0)
                            {
                                ingredients.Add(new PowerIngredient
                                {
                                    part = fuelItem.supplementaryFuel,
                                    perMin = (3d / 50d) * supplementalRatio * building.power, // Calculate the ratio of the supplemental resource to the primary fuel
                                    supplementalRatio = (3d / 50d) * supplementalRatio
                                });
                            }

                            PowerProduct? products = null;
                            if (!string.IsNullOrEmpty(fuelItem.byProduct))
                            {
                                products = new PowerProduct
                                {
                                    part = fuelItem.byProduct,
                                    perMin = fuelItem.byProductAmountPerMin
                                };
                            }

                            recipes.Add(new PowerGenerationRecipe
                            {
                                id = Common.GetPowerGenerationRecipeName(className) + '_' + fuelItem.primaryFuel,
                                displayName = displayName + " (" + primaryFuelPart.name + ")",
                                ingredients = ingredients,
                                byproduct = products,
                                building = building
                            });
                            if (recipes[recipes.Count - 1].id == "GeneratorFuel_RocketFuel")
                            {
                                System.Diagnostics.Debug.Write("GeneratorFuel_RocketFuel");
                            }
                        }
                    }
                }
            }
           
            return recipes.OrderBy(r => r.displayName).ToList();
        }
    }
}
