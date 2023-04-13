namespace SatisfactoryTree.Models
{
    public class Recipe
    {
        public Recipe(
            decimal processingTimeInSeconds,
            decimal throughPutPerMinute,
            Dictionary<string, decimal> inputs,
            Dictionary<string, decimal> outputs,
            ManufactoringBuildingType manufactoringBuilding,
            ManufactoringMethodType manufactoringMethod = ManufactoringMethodType.Manufactured,
            bool primaryMethodOfManufacture = true)
        {
            ProcessingTimeInSeconds = processingTimeInSeconds;
            ThroughPutPerMinute = throughPutPerMinute;
            Inputs = inputs;
            Outputs = outputs;
            ManufactoringBuilding = manufactoringBuilding;
            ManufactoringMethod = manufactoringMethod;
            PrimaryMethodOfManufacture = primaryMethodOfManufacture;
        }
        public decimal ProcessingTimeInSeconds { get; internal set; }
        public decimal ThroughPutPerMinute { get; internal set; }
        public Dictionary<string, decimal> Inputs { get; set; }
        public Dictionary<string, decimal> Outputs { get; set; }
        public ManufactoringBuildingType ManufactoringBuilding { get; set; }
        public ManufactoringMethodType ManufactoringMethod { get; set; }
        public bool PrimaryMethodOfManufacture { get; set; }
    }

    public enum ManufactoringMethodType
    {
        Gathered = 0,
        Manufactured = 1
    }

    public enum ManufactoringBuildingType
    {
        MiningMachine = 1,
        Smelter = 2,
        Foundry = 3,
        Refinery = 4,
        Constructor = 5,
        Assembler = 6,
        Manufacturer = 7,
        OilExtractor = 8,
        WaterExtractor = 9,
        Blender = 10,
        ResourceWellExtractor = 11,
        ParticleAccelerator = 12,
        NuclearPowerPlant = 13,
        None = 14
    }
}
