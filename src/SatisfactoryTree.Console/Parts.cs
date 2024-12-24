using SatisfactoryTree.Console.Interfaces;
using System.Text.Json;

namespace SatisfactoryTree.Console
{
    public class Parts
    {

        public static PartDataInterface GetItems(List<JsonElement> data, List<Recipe> recipes)
        {
            Dictionary<string, Part> parts = new();
            Dictionary<string, string> collectables = new();

            // Scan all recipes (not parts), looking for parts that are used in recipes.
            //var filteredData = rawData.Where((dynamic entry) => entry.Classes != null)
            //           .SelectMany((dynamic entry) => (IEnumerable<dynamic>)entry.Classes);

            HashSet<string> rawParts = new();
            foreach (Recipe recipe in recipes)
            {
                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    rawParts.Add(ingredient.part);
                }
                foreach (Product product in recipe.products)
                {
                    rawParts.Add(product.part);
                }
            }

            foreach (JsonElement entry in data)
            {
                //Check first if the part exists
                string className = entry.GetProperty("ClassName").ToString();
                string partName = Common.GetPartName(className);
                if (!rawParts.Contains(partName))
                {
                    continue;
                }
                string? displayName = entry.TryGetProperty("mDisplayName", out JsonElement mDisplayName) ? mDisplayName.GetString() : string.Empty;
                string? producedIn = entry.TryGetProperty("mProducedIn", out JsonElement mProducedIn) ? mProducedIn.GetString() : string.Empty;
                //string? ingredients = entry.TryGetProperty("mIngredients", out JsonElement mIngredients) ? mIngredients.GetString() : string.Empty;
                //string? products = entry.TryGetProperty("mProduct", out JsonElement mProduct) ? mProduct.GetString() : string.Empty;

                //Get the stack size
                string? stackSizeString = entry.TryGetProperty("mStackSize", out JsonElement mStackSizeElement) ? mStackSizeElement.GetString() : "SS_UNKNOWN";
                int stackSize = StackSizeConvert(stackSizeString);

                string? energyValueString = entry.TryGetProperty("mEnergyValue", out JsonElement energyValueElement) ? energyValueElement.GetString() : "0";
                double energyValue = 0;
                if (energyValueString != null)
                {
                    energyValue = double.Parse(energyValueString);
                }
                bool isFluid = Common.IsFluid(partName);
                if (isFluid)
                {
                    energyValue *= 1000; // Convert from MJ to kJ
                }

                parts[partName] = new Part
                {
                    name = displayName,
                    stackSize = stackSize,
                    isFluid = isFluid,
                    isFicsmas = Common.IsFicsmas(displayName),
                    energyGeneratedInMJ = Math.Round(energyValue) // Round to the nearest whole number (all energy numbers are whole numbers)
                };

                //// There are two exception products we need to check for and add to the parts list
                //if (className == "Desc_NuclearWaste_C")
                //{
                //    // Note that this part id is NuclearWaste, not Uranium Waste
                //    parts["NuclearWaste"] = new Part
                //    {
                //        Name = "Uranium Waste",
                //        StackSize = 500, // SS_HUGE
                //        IsFluid = Common.IsFluid("NuclearWaste"),
                //        IsFicsmas = Common.IsFicsmas(displayName),
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_PlutoniumWaste_C")
                //{
                //    parts["PlutoniumWaste"] = new Part
                //    {
                //        Name = "Plutonium Waste",
                //        StackSize = 500, // SS_HUGE
                //        IsFluid = Common.IsFluid("PlutoniumWaste"),
                //        IsFicsmas = Common.IsFicsmas(displayName),
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //// These are exception products that aren't produced by mines or extractors, they are raw materials
                //else if (className == "Desc_Leaves_C")
                //{
                //    parts["Leaves"] = new Part
                //    {
                //        Name = "Leaves",
                //        StackSize = 500, // SS_HUGE
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 15
                //    };
                //}
                //else if (className == "Desc_Wood_C")
                //{
                //    parts["Wood"] = new Part
                //    {
                //        Name = "Wood",
                //        StackSize = 200, // SS_BIG
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 100
                //    };
                //}
                //else if (className == "Desc_Mycelia_C")
                //{
                //    parts["Mycelia"] = new Part
                //    {
                //        Name = "Mycelia",
                //        StackSize = 200, // SS_BIG
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 20
                //    };
                //}
                //else if (className == "Desc_HogParts_C")
                //{
                //    parts["HogParts"] = new Part
                //    {
                //        Name = "Hog Remains",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 250
                //    };
                //}
                //else if (className == "Desc_SpitterParts_C")
                //{
                //    parts["SpitterParts"] = new Part
                //    {
                //        Name = "Spitter Remains",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 250
                //    };
                //}
                //else if (className == "Desc_StingerParts_C")
                //{
                //    parts["StingerParts"] = new Part
                //    {
                //        Name = "Stinger Remains",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 250
                //    };
                //}
                //else if (className == "Desc_HatcherParts_C")
                //{
                //    parts["HatcherParts"] = new Part
                //    {
                //        Name = "Hatcher Remains",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 250
                //    };
                //}
                //else if (className == "Desc_DissolvedSilica_C")
                //{
                //    // This is a special intermediate alt product
                //    parts["DissolvedSilica"] = new Part
                //    {
                //        Name = "Dissolved Silica",
                //        StackSize = 0, // SS_FLUID
                //        IsFluid = true,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_LiquidOil_C")
                //{
                //    // This is a special liquid raw material
                //    parts["LiquidOil"] = new Part
                //    {
                //        Name = "Liquid Oil",
                //        StackSize = 0, // SS_FLUID
                //        IsFluid = true,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_Gift_C")
                //{
                //    // this is a ficsmas collectable
                //    parts["Gift"] = new Part
                //    {
                //        Name = "Gift",
                //        StackSize = 500, // SS_HUGE
                //        IsFluid = false,
                //        IsFicsmas = true,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_Snow_C")
                //{
                //    // this is a ficsmas collectable
                //    parts["Snow"] = new Part
                //    {
                //        Name = "Snow",
                //        StackSize = 500, // SS_HUGE
                //        IsFluid = false,
                //        IsFicsmas = true,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_Crystal_C")
                //{
                //    parts["Crystal"] = new Part
                //    {
                //        Name = "Blue Power Slug",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_Crystal_mk2_C")
                //{
                //    parts["Crystal_mk2"] = new Part
                //    {
                //        Name = "Yellow Power Slug",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_Crystal_mk3_C")
                //{
                //    parts["Crystal_mk3"] = new Part
                //    {
                //        Name = "Purple Power Slug",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_SAM_C")
                //{
                //    parts["SAM"] = new Part
                //    {
                //        Name = "SAM",
                //        StackSize = 100, // SS_MEDIUM
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "Desc_CrystalShard_C")
                //{
                //    parts["CrystalShard"] = new Part
                //    {
                //        Name = "Power Shard",
                //        StackSize = 100, // SS_MEDIUM
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}
                //else if (className == "BP_ItemDescriptorPortableMiner_C")
                //{
                //    parts["PortableMiner"] = new Part
                //    {
                //        Name = "Portable Miner",
                //        StackSize = 50, // SS_SMALL
                //        IsFluid = false,
                //        IsFicsmas = false,
                //        EnergyGeneratedInMJ = 0
                //    };
                //}

                //if (string.IsNullOrEmpty(className)) continue;

                //// Ensures it's a recipe, we only care about items that are produced within a recipe.
                //if (producedIn == null) continue;

                //if (Common.Blacklist.Any(building => producedIn.Contains(building))) continue;

                // Check if it's an alternate recipe and skip it for parts
                //if (className.StartsWith("Recipe_Alternate")) continue;

                // Check if it's an unpackage recipe and skip it for parts
                //if (displayName != null && displayName.Contains("Unpackage")) continue;

                //// Extract the part name
                //if (products != null)
                //{
                //    System.Text.RegularExpressions.MatchCollection productMatches = System.Text.RegularExpressions.Regex.Matches(products, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)");

                //    foreach (System.Text.RegularExpressions.Match match in productMatches)
                //    {
                //        var partName = Common.GetPartName(match.Groups[1].Value);  // Use the mProduct part name
                //        var friendlyName = Common.GetFriendlyName(displayName);  // Use the friendly name

                //        // Extract the product's Desc_ class name so we can find it in the class descriptors to get the stack size
                //        var productClass = System.Text.RegularExpressions.Regex.Match(match.Groups[0].Value, @"Desc_(.*?)\.Desc_")?.Groups[1].Value;

                //        //var classDescriptor = rawData
                //        //    .SelectMany<dynamic, dynamic>((dynamic entry) => (IEnumerable<dynamic>)entry.Classes)
                //        //    .FirstOrDefault(e => e.ClassName == $"Desc_{productClass}_C");

                //        JsonElement? productItem = FindData(data, $"Desc_{productClass}_C");
                //        if (productItem != null)
                //        {
                //            //Get the stack size
                //            string? stackSizeString = productItem.Value.TryGetProperty("mStackSize", out JsonElement mStackSizeElement) ? mStackSizeElement.GetString() : "SS_UNKNOWN";
                //            int stackSize = StackSizeConvert(stackSizeString);

                //            string? energyValueString = productItem.Value.TryGetProperty("mEnergyValue", out JsonElement energyValueElement) ? energyValueElement.GetString() : "0";
                //            double energyValue = 0;
                //            if (energyValueString != null)
                //            {
                //                energyValue = double.Parse(energyValueString);
                //            }

                //            // Extract stack size
                //            //var stackSize = 0;// StackSizeConvert(productItem?.mStackSize ?? "SS_UNKNOWN");
                //            // Extract the energy value
                //            //var energyValue = 0;// productItem?.mEnergyValue ?? 0;

                //            // Check if the part is a collectable (e.g., Power Slug)
                //            if (IsCollectable(ingredients))
                //            {
                //                collectables[partName] = friendlyName;
                //            }
                //            else
                //            {
                //                parts[partName] = new Part
                //                {
                //                    Name = friendlyName,
                //                    StackSize = stackSize,
                //                    IsFluid = Common.IsFluid(partName),
                //                    IsFicsmas = Common.IsFicsmas(displayName),
                //                    EnergyGeneratedInMJ = Math.Round(energyValue) // Round to the nearest whole number (all energy numbers are whole numbers)
                //                };
                //            }
                //        }
                //    }
                //}
            }

            Dictionary<string, RawResource> rawResources = GetRawResources(data, parts, recipes);

            // Sort the parts and collectables by key
            return new PartDataInterface
            {
                parts = parts.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                rawResources = rawResources
            };
        }

        private static JsonElement? FindData(List<JsonElement> data, string key)
        {
            JsonElement? result = null;
            foreach (JsonElement item in data)
            {
                string className = item.GetProperty("ClassName").ToString();
                if (className == key)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        // Helper function to determine if an ingredient is a collectable (e.g., Power Slug)
        public static bool IsCollectable(string? ingredients)
        {
            if (ingredients == null)
            {
                return false;
            }
            List<string> collectableDescriptors = new()
            {
                "Desc_Crystal.Desc_Crystal_C",        // Blue Power Slug
                "Desc_Crystal_mk2.Desc_Crystal_mk2_C", // Yellow Power Slug
                "Desc_Crystal_mk3.Desc_Crystal_mk3_C"  // Purple Power Slug
            };

            return collectableDescriptors.Any(descriptor => ingredients.Contains(descriptor));
        }

        public static int StackSizeConvert(string? stackSize)
        {
            // Convert e.g. SS_HUGE to 500
            switch (stackSize)
            {
                case "SS_HUGE":
                    return 500;
                case "SS_BIG":
                    return 200;
                case "SS_MEDIUM":
                    return 100;
                case "SS_SMALL":
                    return 50;
                default:
                    return 0;
            }
        }

        public static Dictionary<string, RawResource> GetRawResources(List<JsonElement> data, Dictionary<string, Part> parts, List<Recipe> recipes)
        {
            var rawResources = new Dictionary<string, RawResource>();
            var limits = new Dictionary<string, long>
            {
                { "Coal", 42300 },
                { "LiquidOil", 12600 },
                { "NitrogenGas", 12000 },
                { "OreBauxite", 12300 },
                { "OreCopper", 36900 },
                { "OreGold", 15000 },
                { "OreIron", 92100 },
                { "OreUranium", 2100 },
                { "RawQuartz", 13500 },
                { "SAM", 10200 },
                { "Stone", 69900 },
                { "Sulfur", 10800 },
                { "Water", 9007199254740991 },
                { "Crystal", 596 },
                { "Crystal_mk2", 389 },
                { "Crystal_mk3", 257 }
            };

            //var filteredData = data.Where((dynamic entry) => entry.Classes != null)
            //                       .SelectMany<dynamic, dynamic>((dynamic entry) => (IEnumerable<dynamic>)entry.Classes);

            //Start with a list of parts.
            //loop through all the recipes, looking to see if the part is a product.
            //If it's a product, it's not a raw material, remove it from the list
            //the remaining list are the raw materials
            HashSet<string> partsRemaining = new();
            foreach (Recipe recipe in recipes)
            {
                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    partsRemaining.Add(ingredient.part);
                }
            }

            foreach (Recipe recipe in recipes)
            {
                foreach (Product product in recipe.products)
                {
                    //if (product.part == "TimeCrystal")
                    //{
                    //    int y = 0;
                    //}
                    bool removePart = false;
                    // don't process converter recipes here - as most of the raw ores are a product of the converter
                    if (recipe.building.name != "converter" && recipe.building.name != "generatornuclear")
                    {
                        removePart = true;
                    }

                    if (removePart)
                    {
                        partsRemaining.Remove(product.part);
                    }
                }
            }
            //Fiter out both Uranium and Plutonium waste
            partsRemaining.Remove("NuclearWaste");
            partsRemaining.Remove("PlutoniumWaste");
            //Filter out specific Converter recipes that all have different 
            partsRemaining.Remove("FicsiteIngot");
            partsRemaining.Remove("TimeCrystal");
            partsRemaining.Remove("QuantumEnergy");
            //Add water - which comes into the game in a different way from every other product
            partsRemaining.Add("Water");
            partsRemaining.Add("LiquidOil");
            partsRemaining.Add("NitrogenGas");
            partsRemaining.Add("Coal");


            foreach (string part in partsRemaining)
            {
                rawResources[part] = new RawResource
                {
                    name = part,
                    limit = limits.ContainsKey(part) ? limits[part] : -1
                };
            }
            // Update the resource part id with the part name
            foreach (KeyValuePair<string, RawResource> rawResource in rawResources)
            {
                foreach (KeyValuePair<string, Part> part in parts)
                {
                    if (part.Key == rawResource.Key)
                    {
                        rawResource.Value.name = part.Value.name;
                    }
                }
            }
            rawResources = rawResources.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return rawResources;
        }

        public static void FixItemNames(PartDataInterface items)
        {
            // Go through the item names and do some manual fixes, e.g. renaming "Residual Plastic" to "Plastic"
            var fixItems = new Dictionary<string, string>
            {
                { "AlienProtein", "Alien Protein" },
                { "CompactedCoal", "Compacted Coal" },
                { "DarkEnergy", "Dark Matter Residue" },
                { "HeavyOilResidue", "Heavy Oil Residue" },
                { "LiquidFuel", "Fuel" },
                { "Plastic", "Plastic" },
                { "PolymerResin", "Polymer Resin" },
                { "Rubber", "Rubber" },
                { "Snow", "Snow" },
                { "Water", "Water" }
            };

            foreach (var search in fixItems.Keys)
            {
                if (items.parts.ContainsKey(search))
                {
                    items.parts[search].name = fixItems[search];
                }
            }
        }

        public static void FixTurbofuel(PartDataInterface items, List<Recipe> recipes)
        {
            //// Rename the current "Turbofuel" which is actually "Packaged Turbofuel"
            //items.parts["PackagedTurboFuel"] = items.parts["TurboFuel"];

            //// Add the actual "Turbofuel" as a new item
            //items.parts["LiquidTurboFuel"] = new Part
            //{
            //    name = "Turbofuel",
            //    stackSize = 0,
            //    isFluid = true,
            //    isFicsmas = false,
            //    energyGeneratedInMJ = 2000
            //};

            //// Rename the packaged item to PackagedTurboFuel
            //items.parts["PackagedTurboFuel"] = new Part
            //{
            //    name = "Packaged Turbofuel",
            //    stackSize = 100, // SS_MEDIUM
            //    isFluid = false,
            //    isFicsmas = false,
            //    energyGeneratedInMJ = 2000
            //};

            //// Remove the incorrect packaged turbofuel
            //items.parts.Remove("TurboFuel");

            //// Now we need to go through the recipes and wherever "TurboFuel" is mentioned, it needs to be changed to "PackagedTurboFuel"
            //foreach (var recipe in recipes)
            //{
            //    foreach (var product in recipe.products)
            //    {
            //        if (product.part == "TurboFuel")
            //        {
            //            product.part = "PackagedTurboFuel";
            //        }
            //    }

            //    foreach (var ingredient in recipe.ingredients)
            //    {
            //        if (ingredient.part == "TurboFuel")
            //        {
            //            ingredient.part = "PackagedTurboFuel";
            //        }
            //    }
            //}
        }
    }
}
