using System.Text.Json;
using System.Text.RegularExpressions;

namespace SatisfactoryTree.Console
{
    public class Buildings
    {
        public static List<string> GetProducingBuildings(List<JsonElement> data)
        {
            HashSet<string> producingBuildingsSet = new();
            foreach (JsonElement entry in data)
            {
                string className = entry.GetProperty("ClassName").ToString();
                string? producedIn = entry.TryGetProperty("mProducedIn", out JsonElement mProducedIn) ? mProducedIn.GetString() : string.Empty;
                //string? fuels = entry.TryGetProperty("mFuel", out JsonElement mFuel) ? mFuel.GetString() : string.Empty;
                JsonElement? fuelJSON = entry.TryGetProperty("mFuel", out JsonElement mFuel) ? mFuel : (JsonElement?)null;

                if (producedIn != null)
                {
                    IEnumerable<string> producedInBuildings = Regex.Matches(producedIn, @"\/(\w+)\/(\w+)\.(\w+)_C")
                                                   .Cast<Match>()
                                                   .Select((match =>
                                                   {
                                                       string building = match.Groups[2].Value;
                                                       return building.StartsWith("Build_", StringComparison.OrdinalIgnoreCase)
                                                              ? building.Replace("Build_", "", StringComparison.OrdinalIgnoreCase).ToLower()
                                                              : building.ToLower();
                                                   }))
                                                   .Where(new Func<string, bool>(buildingName => !string.IsNullOrEmpty(buildingName)));

                    foreach (string? buildingName in producedInBuildings)
                    {
                        if (buildingName != "bp_buildgun")
                        {
                            producingBuildingsSet.Add(buildingName);
                        }
                    }
                }
                // If a power generator
                if (fuelJSON.HasValue)
                {
                    string? name = Common.GetPowerProducerBuildingName(className);
                    producingBuildingsSet.Add(name);
                }
            }

            return producingBuildingsSet.ToList();
        }


        public static Dictionary<string, double> GetPowerConsumptionForBuildings(List<JsonElement> data, List<string> producingBuildings)
        {
            var buildingsPowerMap = new Dictionary<string, double>();

            //var filteredData = data.Where(entry => entry.Classes != null)
            //                       .SelectMany<dynamic, dynamic>(entry => entry.Classes);

            foreach (JsonElement entry in data)
            {
                string className = entry.GetProperty("ClassName").ToString();
                string powerConsumptionString = entry.TryGetProperty("mPowerConsumption", out JsonElement mPowerConsumption) ? mPowerConsumption.ToString() : string.Empty;
                if (className != null && powerConsumptionString != "")
                {
                    double powerConsumption = double.TryParse(powerConsumptionString, out double power) ? power : 0;
                    // Normalize the building name by removing "_Build" prefix, "_C" suffix, and lowercasing it
                    string buildingName = Common.GetPowerProducerBuildingName(className); //Common.GetBuildingName(className).ToLower();

                    // Only include power data if the building is in the producingBuildings list
                    if (producingBuildings.Contains(buildingName))
                    {
                        buildingsPowerMap[buildingName] = powerConsumption;
                    }
                }
            }

            // Finally sort the map by key
            var sortedMap = buildingsPowerMap.OrderBy(kvp => kvp.Key)
                                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return sortedMap;
        }
    }
}
