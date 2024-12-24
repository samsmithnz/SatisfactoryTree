using SatisfactoryTree.Console.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SatisfactoryTree.Console
{
    public class Common
    {
        // Blacklist for excluding items produced by the Build Gun
        public static readonly List<string> Blacklist = new List<string>
        {
            "(\"/Game/FactoryGame/Equipment/BuildGun/BP_BuildGun.BP_BuildGun_C\")",
            "(\"/Script/FactoryGame.FGBuildGun\")",
            "(\"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C\")"
        };

        public static readonly List<string> Whitelist = new List<string>
        {
            // Nuclear Waste is not produced by any buildings - it's a byproduct of Nuclear Power Plants
            "Desc_NuclearWaste_C",
            "Desc_PlutoniumWaste_C",
            // These are collectible items, not produced by buildings
            "Desc_Leaves_C",
            "Desc_Wood_C",
            "Desc_Mycelia_C",
            "Desc_HogParts_C",
            "Desc_SpitterParts_C",
            "Desc_StingerParts_C",
            "Desc_HatcherParts_C",
            "Desc_DissolvedSilica_C",
            "Desc_Crystal_C",
            "Desc_Crystal_mk2_C",
            "Desc_Crystal_mk3_C",
            // Liquid Oil can be produced by oil extractors and oil wells
            "Desc_LiquidOil_C",
            // FICSMAS items
            "Desc_Gift_C",
            "Desc_Snow_C",
            // SAM Ore is mined, but for some reason doesn't have a produced in property
            "Desc_SAM_C",
            // Special items 
            "Desc_CrystalShard_C",
            "BP_ItemDescriptorPortableMiner_C"
        };

        // Helper function to check if a recipe is likely to be liquid based on building type and amount
        public static bool IsFluid(string productName)
        {
            var liquidProducts = new HashSet<string>
            {
                "water", "liquidoil", "heavyoilresidue", "liquidfuel", "liquidturbofuel", "liquidbiofuel",
                "aluminasolution", "sulfuricacid", "nitricacid", "dissolvedsilica"
            };

            var gasProducts = new HashSet<string>
            {
                "nitrogengas", "rocketfuel", "ionizedfuel", "quantumenergy", "darkenergy"
            };

            productName = productName.ToLower();

            return liquidProducts.Contains(productName) || gasProducts.Contains(productName);
        }

        public static bool IsFicsmas(string? displayName)
        {
            if (displayName == null)
            {
                return false;
            }
            return displayName.Contains("FICSMAS", StringComparison.OrdinalIgnoreCase) ||
                   displayName.Contains("Gift", StringComparison.OrdinalIgnoreCase) ||
                   displayName.Contains("Snow", StringComparison.OrdinalIgnoreCase) ||
                   displayName.Contains("Candy", StringComparison.OrdinalIgnoreCase) ||
                   displayName.Contains("Fireworks", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetRecipeName(string name)
        {
            //return name.Replace(oldValue: "Build_", "").Replace("_C", "");
            name = Regex.Replace(name, @"_C$", ""); // Replace _C only at the end of the string
            name = name.Replace("Recipe_", "");
            return name;
        }

        public static string GetPowerGenerationRecipeName(string name)
        {
            //return name.Replace(oldValue: "Build_", "").Replace("_C", "");
            name = Regex.Replace(name, @"_C$", ""); // Replace _C only at the end of the string
            name = name.Replace("Build_", "");
            return name;
        }

        

        public static string GetBuildingName(string name)
        {
            //name = name.Replace("Build_", "").Replace("_C", "");
            name = Regex.Replace(name, @"_C$", ""); // Replace _C only at the end of the string
            name = name.Replace("Build_", "");
            return name;
        }
        
        public static string GetPowerBuildingName(string name)
        {
            Match match = Regex.Match(name, @"Build_(\w+)_");
            if (match.Success)
            {
                string buildingName = match.Groups[1].Value.ToLower();
                // If contains _automated, remove it
                return buildingName.Replace("_automated", "");
            }
            return "";
        }



        public static string GetPartName(string name)
        {
            if (name == "BP_ItemDescriptorPortableMiner_C")
            {
                return "PortableMiner";
            }
            //name = name.Replace("Desc_", "").Replace("_C", "");
            name = Regex.Replace(name, @"_C$", ""); // Replace _C only at the end of the string
            name = name.Replace("Desc_", "");
            return name;
        }

        public static string GetFriendlyName(string? name)
        {
            if (name == null)
            {
                return "";
            }
            // Remove any text within brackets, including the brackets themselves
            return Regex.Replace(name, @"\s*\(.*?\)", "");
        }

        // Example: Build_GeneratorBiomass_Automated_C
        // Change into "generatorbiomas"
        public static string GetPowerProducerBuildingName(string className)
        {
            var match = Regex.Match(className, @"Build_(\w+)_");
            if (match.Success)
            {
                var buildingName = match.Groups[1].Value.ToLower();
                // If contains _automated, remove it
                return buildingName.Replace("_automated", "");
            }

            return null;
        }


    }
}
