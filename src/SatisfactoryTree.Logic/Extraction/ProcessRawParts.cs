using SatisfactoryTree.Logic.Models;
using System.Text.Json;

namespace SatisfactoryTree.Logic.Extraction
{
    public class ProcessRawParts
    {

        public static RawPartsAndRawMaterials GetItems(List<JsonElement> data, List<Recipe> recipes)
        {
            Dictionary<string, Part> parts = new();
            Dictionary<string, string> collectables = new();

            // Scan all recipes (not parts), looking for parts that are used in recipes.
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
            }

            Dictionary<string, RawResource> rawResources = GetRawResources(data, parts, recipes);

            // Sort the parts and collectables by key
            return new RawPartsAndRawMaterials
            {
                Parts = parts.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                RawResources = rawResources
            };
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
                    bool removePart = false;
                    // don't process converter recipes here - as most of the raw ores are a product of the converter
                    if (recipe.building.Name != "converter" && recipe.building.Name != "generatornuclear")
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
                    limit = limits.ContainsKey(part) ? limits[part] : 100000000
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

        public static void FixItemNames(RawPartsAndRawMaterials items)
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
                    items.Parts[search].name = fixItems[search];
                }
            }
        }

        public static void FixTurbofuel(RawPartsAndRawMaterials items, List<Recipe> recipes)
        {
            // Rename the current "Turbofuel" which is actually "Packaged Turbofuel"
            items.Parts["PackagedTurboFuel"] = items.Parts["TurboFuel"];

            // Add the actual "Turbofuel" as a new item
            items.Parts["LiquidTurboFuel"] = new Part
            {
                name = "Turbofuel",
                stackSize = 0,
                isFluid = true,
                isFicsmas = false,
                energyGeneratedInMJ = 2000
            };

            // Rename the packaged item to PackagedTurboFuel
            items.Parts["PackagedTurboFuel"] = new Part
            {
                name = "Packaged Turbofuel",
                stackSize = 100, // SS_MEDIUM
                isFluid = false,
                isFicsmas = false,
                energyGeneratedInMJ = 2000
            };

            // Remove the incorrect packaged turbofuel
            items.Parts.Remove("TurboFuel");

            // Now we need to go through the recipes and wherever "TurboFuel" is mentioned, it needs to be changed to "PackagedTurboFuel"
            foreach (Recipe recipe in recipes)
            {
                foreach (Product product in recipe.products)
                {
                    if (product.part == "TurboFuel")
                    {
                        product.part = "PackagedTurboFuel";
                    }
                }

                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    if (ingredient.part == "TurboFuel")
                    {
                        ingredient.part = "PackagedTurboFuel";
                    }
                }
            }
        }
    }
}
