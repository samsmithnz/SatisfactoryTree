using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services;

public class FactoryItemDisplayService : IFactoryItemDisplayService
{
    public string GetPartImagePath(string partName)
    {
        //// Handle special mapping cases where the part name doesn't directly match the image name
        //string imageName = partName switch
        //{
        //    "IronPlateReinforced" => "ReinforcedIronPlate",
        //    "OreIron" => "IronOre", 
        //    "IronScrew" => "IronScrews",
        //    _ => partName.Replace(" ", "") // Default: remove spaces
        //};
        string imageName = partName.Replace(" ", "");

        return $"images/parts/{imageName}_256.png";
    }

    public string GetBuildingImagePath(string buildingName)
    {
        Console.WriteLine(buildingName);
        // Handle building name mappings to match image files
        string imageName = buildingName switch
        {
            "smeltermk1" => "SmelterMk1_256.png",
            "foundrymk1" => "Foundry_256.png",
            "constructormk1" => "ConstructorMk1_256.png",
            "assemblermk1" => "AssemblerMk1_256.png",
            "manufacturermk1" => "Manufacturer_256.png",
            "refinery" => "OilRefinery_256.png",
            "packager" => "Packager_256.png",
            "blender" => "Blender_256.png",
            "hadronCollider" => "ParticleAccelerator_256.png",
            "generatorcoal" => "CoalGenerator_256.png",
            "generatorfuel" => "FuelGenerator_256.png",
            "generatornuclear" => "NuclearPowerplant_256.png",
            "generatorbiomass" => "BiomassBurner_256.png",
            "generatorgeothermal" => "GeothermalPowerGenerator_256.png",
            "minermk1" => "MinerMk1_256.png",
            "minermk2" => "MinerMk2_256.png",
            "minermk3" => "MinerMk3_256.png",
            "oilpump" => "OilExtractor_256.png",
            "waterpump" => "WaterExtractor_256.png",
            "frackingextractor" => "ResourceWellExtractor_256.png",
            "frackingsmasher" => "ResourceWellPressurizer_256.png",
            "resourcesink" => "ResourceSink_256.png",
            _ => $"{buildingName}_256.png" // Default: use building name as-is
        };
        Console.WriteLine(imageName);

        return $"images/buildings/{imageName}";
    }

    public string GetBuildingName(string buildingName)
    {
        string name = buildingName switch
        {
            "smeltermk1" => "Smelter",
            "foundrymk1" => "Foundry",
            "constructormk1" => "Constructor",
            "assemblermk1" => "Assembler",
            "manufacturermk1" => "Manufacturer",
            "refinery" => "Refinery",
            "packager" => "Packager",
            "blender" => "Blender",
            "hadronCollider" => "Particle Accelerator",
            "generatorcoal" => "Coal Generator",
            "generatorfuel" => "Fuel Generator",
            "generatornuclear" => "Nuclear Power Plant",
            "generatorbiomass" => "Biomass Burner",
            "generatorgeothermal" => "Geothermal Power Generator",
            "minermk1" => "Miner Mk1",
            "minermk2" => "Miner Mk2",
            "minermk3" => "Miner Mk3",
            "oilpump" => "Oil Extractor",
            "waterpump" => "Water Extractor",
            "frackingextractor" => "Resource Well Extractor",
            "frackingsmasher" => "Resource Well Pressurizer",
            "resourcesink" => "Resource Sink",
            _ => $"{buildingName}" // Default: use building name as-is
        };
        return name;
    }

    public bool HasBuildingImage(string buildingName)
    {
        // Check if we have a specific image mapping for this building
        return buildingName switch
        {
            "smeltermk1" or "foundrymk1" or "constructormk1" or "assemblermk1" or "manufacturermk1" or
            "oilrefinery" or "packager" or "blender" or "hadronCollider" or
            "generatorcoal" or "generatorfuel" or "generatornuclear" or
            "generatorbiomass" or "generatorgeothermal" or
            "minermk1" or "minermk2" or "minermk3" or
            "oilpump" or "waterpump" or "frackingextractor" or
            "frackingsmasher" or "resourcesink" => true,
            _ => false
        };
    }

    public string GetPartDisplayName(Part part)
    {
        return part.Name ?? "Unknown";
    }

    public bool GetPartIsFluid(Part part)
    {
        return part.IsFluid;
    }

    public bool GetPartIsFicsmas(Part part)
    {
        return part.IsFicsmas;
    }

}