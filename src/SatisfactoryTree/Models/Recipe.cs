namespace DSPTree.Models
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
        MiningMachine = 0,
        Smelter = 1,
        Foundry = 2,
        Refinery = 3,
        Constructor = 4,
        Assembler = 5,
        Manufacturer = 6,
        OilExtractor = 7,
        WaterExtractor = 8,
        //OilRefinery = 5,
        //ChemicalPlant = 6,
        //MatrixLab = 7,
        //MiniatureParticleCollider = 8,
        //EnergyExchanger = 9,
        //Fractionator = 10,
        //RayReceiver = 11,
        None = 12
    }
}
