namespace SatisfactoryTree.Models
{
    public class Recipe
    {
        public Recipe(
            Dictionary<string, decimal> inputs,
            Dictionary<string, decimal> outputs,
            string building,
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
            Building = building;
            PrimaryMethodOfManufacture = primaryMethodOfManufacture;
        }

        public string Name { get; set; }
        public decimal ProcessingTimeInSeconds { get; internal set; }
        public decimal ThroughPutPerMinute { get; internal set; }
        public Dictionary<string, decimal> Inputs { get; set; }
        public Dictionary<string, decimal> Outputs { get; set; }
        public string Building { get; set; }
        public bool PrimaryMethodOfManufacture { get; set; }

    }
}
