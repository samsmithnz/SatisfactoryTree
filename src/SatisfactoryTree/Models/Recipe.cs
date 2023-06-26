namespace SatisfactoryTree.Models
{
    public class Recipe
    {
        public Recipe(
            Dictionary<string, decimal> inputs,
            Dictionary<string, decimal> outputs,
            ManufactoringBuildingType manufactoringBuilding,
            bool primaryMethodOfManufacture = true,
            string? name = null)
        {
            if (name == null && outputs != null)
            {
                Name = outputs.FirstOrDefault().Key;
            }
            else if (name != null)
            {
                Name = name;
            }
            else
            {
                Name = "Unknown";
            }
            Inputs = inputs;
            if (outputs == null)
            {
                outputs = new();
            }
            Outputs = outputs;
            ManufactoringBuilding = manufactoringBuilding;
            PrimaryMethodOfManufacture = primaryMethodOfManufacture;
        }

        //public Recipe(
        //    decimal processingTimeInSeconds,
        //    decimal throughPutPerMinute,
        //    Dictionary<string, decimal> inputs,
        //    Dictionary<string, decimal> outputs,
        //    ManufactoringBuildingType manufactoringBuilding,
        //    ManufactoringMethodType manufactoringMethod = ManufactoringMethodType.Manufactured,
        //    bool primaryMethodOfManufacture = true)
        //{
        //    ProcessingTimeInSeconds = processingTimeInSeconds;
        //    ThroughPutPerMinute = throughPutPerMinute;
        //    Inputs = inputs;
        //    Outputs = outputs;
        //    ManufactoringBuilding = manufactoringBuilding;
        //    ManufactoringMethod = manufactoringMethod;
        //    PrimaryMethodOfManufacture = primaryMethodOfManufacture;
        //}

        public string Name { get; set; }
        public decimal ProcessingTimeInSeconds { get; internal set; }
        public decimal ThroughPutPerMinute { get; internal set; }
        public Dictionary<string, decimal> Inputs { get; set; }
        public Dictionary<string, decimal> Outputs { get; set; }
        public ManufactoringBuildingType ManufactoringBuilding { get; set; }
        public bool PrimaryMethodOfManufacture { get; set; }

    }

    public enum ManufactoringBuildingType
    {
        None = 0,
        Extraction = 1,
        Production = 2,
        PowerGeneration = 3,
        Special = 4
    }
}
