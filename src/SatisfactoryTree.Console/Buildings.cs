using System.Text.RegularExpressions;

namespace SatisfactoryTree.Console
{
    public class Buildings
    {
        public static List<string> GetProducingBuildings(List<dynamic> data)
        {
            var producingBuildingsSet = new HashSet<string>();

            var filteredData = data.Where(entry => entry.Classes != null)
                                   .SelectMany<dynamic, dynamic>(entry => entry.Classes);

            foreach (var entry in filteredData)
            {
                if (entry.mProducedIn != null)
                {
                    var producedInBuildings = Regex.Matches(entry.mProducedIn, @"\/(\w+)\/(\w+)\.(\w+)_C")
                                                   .Cast<Match>()
                                                   .Select((Func<Match, string>)(match =>
                                                   {
                                                       var building = match.Groups[2].Value;
                                                       return building.StartsWith("Build_", StringComparison.OrdinalIgnoreCase)
                                                              ? building.Replace("Build_", "", StringComparison.OrdinalIgnoreCase).ToLower()
                                                              : building.ToLower();
                                                   }))
                                                   .Where(new Func<string, bool>(buildingName => !string.IsNullOrEmpty(buildingName)));

                    foreach (var buildingName in producedInBuildings)
                    {
                        producingBuildingsSet.Add(buildingName);
                    }
                }
                else if (entry.ClassName == "Desc_NuclearWaste_C")
                {
                    producingBuildingsSet.Add("nuclear power plant");
                }
            }

            return producingBuildingsSet.ToList();
        }


        public static Dictionary<string, double> GetPowerConsumptionForBuildings(List<dynamic> data, List<string> producingBuildings)
        {
            var buildingsPowerMap = new Dictionary<string, double>();

            var filteredData = data.Where(entry => entry.Classes != null)
                                   .SelectMany<dynamic, dynamic>(entry => entry.Classes);

            foreach (var building in filteredData)
            {
                if (building.ClassName != null && building.mPowerConsumption != null)
                {
                    // Normalize the building name by removing "_C" and lowercasing it
                    string buildingName = building.ClassName.Replace("_C", "").ToLower();
                    buildingName = buildingName.StartsWith("build_") ? buildingName.Replace("build_", "") : buildingName;

                    // Only include power data if the building is in the producingBuildings list
                    if (producingBuildings.Contains(buildingName))
                    {
                        buildingsPowerMap[buildingName] = double.TryParse(building.mPowerConsumption.ToString(), out double powerConsumption) ? powerConsumption : 0;
                    }
                }
            }

            // Manually add nuclear power plant
            buildingsPowerMap["nuclearpowerplant"] = 0;

            // Finally sort the map by key
            var sortedMap = buildingsPowerMap.OrderBy(kvp => kvp.Key)
                                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return sortedMap;
        }
    }
}
