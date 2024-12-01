using SatisfactoryTree.Console.Interfaces;

namespace SatisfactoryTree.Console
{
    public class Parts
    {
        public static PartDataInterface GetItems(List<dynamic> data)
        {
            var parts = new Dictionary<string, Part>();
            var collectables = new Dictionary<string, string>();
            var rawResources = GetRawResources(data);

            // Scan all recipes (not parts), looking for parts that are used in recipes.
            var filteredData = data.Where((dynamic entry) => entry.Classes != null)
                       .SelectMany((dynamic entry) => (IEnumerable<dynamic>)entry.Classes);

            foreach (var entry in filteredData)
            {
                // There are two exception products we need to check for and add to the parts list
                if (entry.ClassName == "Desc_NuclearWaste_C")
                {
                    // Note that this part id is NuclearWaste, not Uranium Waste
                    parts["NuclearWaste"] = new Part
                    {
                        Name = "Uranium Waste",
                        StackSize = 500, // SS_HUGE
                        IsFluid = Common.IsFluid("NuclearWaste"),
                        IsFicsmas = Common.IsFicsmas(entry.mDisplayName),
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_PlutoniumWaste_C")
                {
                    parts["PlutoniumWaste"] = new Part
                    {
                        Name = "Plutonium Waste",
                        StackSize = 500, // SS_HUGE
                        IsFluid = Common.IsFluid("PlutoniumWaste"),
                        IsFicsmas = Common.IsFicsmas(entry.mDisplayName),
                        EnergyGeneratedInMJ = 0
                    };
                }
                // These are exception products that aren't produced by mines or extractors, they are raw materials
                else if (entry.ClassName == "Desc_Leaves_C")
                {
                    parts["Leaves"] = new Part
                    {
                        Name = "Leaves",
                        StackSize = 500, // SS_HUGE
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 15
                    };
                }
                else if (entry.ClassName == "Desc_Wood_C")
                {
                    parts["Wood"] = new Part
                    {
                        Name = "Wood",
                        StackSize = 200, // SS_BIG
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 100
                    };
                }
                else if (entry.ClassName == "Desc_Mycelia_C")
                {
                    parts["Mycelia"] = new Part
                    {
                        Name = "Mycelia",
                        StackSize = 200, // SS_BIG
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 20
                    };
                }
                else if (entry.ClassName == "Desc_HogParts_C")
                {
                    parts["HogParts"] = new Part
                    {
                        Name = "Hog Remains",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 250
                    };
                }
                else if (entry.ClassName == "Desc_SpitterParts_C")
                {
                    parts["SpitterParts"] = new Part
                    {
                        Name = "Spitter Remains",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 250
                    };
                }
                else if (entry.ClassName == "Desc_StingerParts_C")
                {
                    parts["StingerParts"] = new Part
                    {
                        Name = "Stinger Remains",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 250
                    };
                }
                else if (entry.ClassName == "Desc_HatcherParts_C")
                {
                    parts["HatcherParts"] = new Part
                    {
                        Name = "Hatcher Remains",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 250
                    };
                }
                else if (entry.ClassName == "Desc_DissolvedSilica_C")
                {
                    // This is a special intermediate alt product
                    parts["DissolvedSilica"] = new Part
                    {
                        Name = "Dissolved Silica",
                        StackSize = 0, // SS_FLUID
                        IsFluid = true,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_LiquidOil_C")
                {
                    // This is a special liquid raw material
                    parts["LiquidOil"] = new Part
                    {
                        Name = "Liquid Oil",
                        StackSize = 0, // SS_FLUID
                        IsFluid = true,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_Gift_C")
                {
                    // this is a ficsmas collectable
                    parts["Gift"] = new Part
                    {
                        Name = "Gift",
                        StackSize = 500, // SS_HUGE
                        IsFluid = false,
                        IsFicsmas = true,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_Snow_C")
                {
                    // this is a ficsmas collectable
                    parts["Snow"] = new Part
                    {
                        Name = "Snow",
                        StackSize = 500, // SS_HUGE
                        IsFluid = false,
                        IsFicsmas = true,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_Crystal_C")
                {
                    parts["Crystal"] = new Part
                    {
                        Name = "Blue Power Slug",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_Crystal_mk2_C")
                {
                    parts["Crystal_mk2"] = new Part
                    {
                        Name = "Yellow Power Slug",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_Crystal_mk3_C")
                {
                    parts["Crystal_mk3"] = new Part
                    {
                        Name = "Purple Power Slug",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_SAM_C")
                {
                    parts["SAM"] = new Part
                    {
                        Name = "SAM",
                        StackSize = 100, // SS_MEDIUM
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "Desc_CrystalShard_C")
                {
                    parts["CrystalShard"] = new Part
                    {
                        Name = "Power Shard",
                        StackSize = 100, // SS_MEDIUM
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }
                else if (entry.ClassName == "BP_ItemDescriptorPortableMiner_C")
                {
                    parts["PortableMiner"] = new Part
                    {
                        Name = "Portable Miner",
                        StackSize = 50, // SS_SMALL
                        IsFluid = false,
                        IsFicsmas = false,
                        EnergyGeneratedInMJ = 0
                    };
                }

                if (string.IsNullOrEmpty(entry.ClassName)) continue;

                // Ensures it's a recipe, we only care about items that are produced within a recipe.
                if (entry.mProducedIn == null) continue;

                if (Common.Blacklist.Any(building => entry.mProducedIn.Contains(building))) continue;

                // Check if it's an alternate recipe and skip it for parts
                if (entry.ClassName.StartsWith("Recipe_Alternate")) continue;

                // Check if it's an unpackage recipe and skip it for parts
                if (entry.mDisplayName.Contains("Unpackage")) continue;

                // Extract the part name
                var productMatches = System.Text.RegularExpressions.Regex.Matches(entry.mProduct, @"ItemClass="".*?\/Desc_(.*?)\.Desc_.*?"",Amount=(\d+)");

                foreach (System.Text.RegularExpressions.Match match in productMatches)
                {
                    var partName = Common.GetPartName(match.Groups[1].Value);  // Use the mProduct part name
                    var friendlyName = Common.GetFriendlyName(entry.mDisplayName);  // Use the friendly name

                    // Extract the product's Desc_ class name so we can find it in the class descriptors to get the stack size
                    var productClass = System.Text.RegularExpressions.Regex.Match(match.Groups[0].Value, @"Desc_(.*?)\.Desc_")?.Groups[1].Value;

                    var classDescriptor = data
                        .SelectMany<dynamic, dynamic>((dynamic entry) => (IEnumerable<dynamic>)entry.Classes)
                        .FirstOrDefault(e => e.ClassName == $"Desc_{productClass}_C");

                    // Extract stack size
                    var stackSize = StackSizeConvert(classDescriptor?.mStackSize ?? "SS_UNKNOWN");
                    // Extract the energy value
                    var energyValue = classDescriptor?.mEnergyValue ?? 0;

                    // Check if the part is a collectable (e.g., Power Slug)
                    if (IsCollectable(entry.mIngredients))
                    {
                        collectables[partName] = friendlyName;
                    }
                    else
                    {
                        parts[partName] = new Part
                        {
                            Name = friendlyName,
                            StackSize = stackSize,
                            IsFluid = Common.IsFluid(partName),
                            IsFicsmas = Common.IsFicsmas(entry.mDisplayName),
                            EnergyGeneratedInMJ = Math.Round(energyValue) // Round to the nearest whole number (all energy numbers are whole numbers)
                        };
                    }
                }
            }

            // Sort the parts and collectables by key
            return new PartDataInterface
            {
                Parts = parts.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Collectables = collectables.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                RawResources = rawResources
            };
        }

        // Helper function to determine if an ingredient is a collectable (e.g., Power Slug)
        public static bool IsCollectable(string ingredients)
        {
            var collectableDescriptors = new List<string>
            {
                "Desc_Crystal.Desc_Crystal_C",        // Blue Power Slug
                "Desc_Crystal_mk2.Desc_Crystal_mk2_C", // Yellow Power Slug
                "Desc_Crystal_mk3.Desc_Crystal_mk3_C"  // Purple Power Slug
            };

            return collectableDescriptors.Any(descriptor => ingredients.Contains(descriptor));
        }

        public static int StackSizeConvert(string stackSize)
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

        public static Dictionary<string, RawResource> GetRawResources(List<dynamic> data)
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
            { "Water", 9007199254740991 }
        };

            var filteredData = data.Where((dynamic entry) => entry.Classes != null)
                                   .SelectMany<dynamic, dynamic>((dynamic entry) => (IEnumerable<dynamic>)entry.Classes);

            foreach (var resource in filteredData)
            {
                var className = Common.GetPartName(resource.ClassName);
                var displayName = resource.mDisplayName;

                var resourceData = new RawResource
                {
                    Name = displayName,
                    Limit = limits.ContainsKey(className) ? limits[className] : 0
                };

                if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(displayName))
                {
                    rawResources[className] = resourceData;
                }
            }

            // Manually add "Leaves" and "Wood" to the rawResources list
            rawResources["Leaves"] = new RawResource
            {
                Name = "Leaves",
                Limit = limits.ContainsKey("Leaves") ? limits["Leaves"] : 100000000
            };

            rawResources["Wood"] = new RawResource
            {
                Name = "Wood",
                Limit = limits.ContainsKey("Wood") ? limits["Wood"] : 100000000
            };

            rawResources["Mycelia"] = new RawResource
            {
                Name = "Mycelia",
                Limit = limits.ContainsKey("Mycelia") ? limits["Mycelia"] : 100000000
            };

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
                if (items.Parts.ContainsKey(search))
                {
                    items.Parts[search].Name = fixItems[search];
                }
            }
        }

        public static void FixTurbofuel(PartDataInterface items, List<Recipe> recipes)
        {
            // Rename the current "Turbofuel" which is actually "Packaged Turbofuel"
            items.Parts["PackagedTurboFuel"] = items.Parts["TurboFuel"];

            // Add the actual "Turbofuel" as a new item
            items.Parts["LiquidTurboFuel"] = new Part
            {
                Name = "Turbofuel",
                StackSize = 0,
                IsFluid = true,
                IsFicsmas = false,
                EnergyGeneratedInMJ = 2000
            };

            // Rename the packaged item to PackagedTurboFuel
            items.Parts["PackagedTurboFuel"] = new Part
            {
                Name = "Packaged Turbofuel",
                StackSize = 100, // SS_MEDIUM
                IsFluid = false,
                IsFicsmas = false,
                EnergyGeneratedInMJ = 2000
            };

            // Remove the incorrect packaged turbofuel
            items.Parts.Remove("TurboFuel");

            // Now we need to go through the recipes and wherever "TurboFuel" is mentioned, it needs to be changed to "PackagedTurboFuel"
            foreach (var recipe in recipes)
            {
                foreach (var product in recipe.Products)
                {
                    if (product.Part == "TurboFuel")
                    {
                        product.Part = "PackagedTurboFuel";
                    }
                }

                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredient.Part == "TurboFuel")
                    {
                        ingredient.Part = "PackagedTurboFuel";
                    }
                }
            }
        }
    }
}
