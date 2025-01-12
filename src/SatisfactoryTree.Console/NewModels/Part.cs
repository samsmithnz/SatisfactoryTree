namespace SatisfactoryTree.Console.NewModels
{
    public class Part
    {
        public string name { get; set; }
        public int stackSize { get; set; }
        public bool isFluid { get; set; }
        public bool isFicsmas { get; set; }
        public double energyGeneratedInMJ { get; set; }
    }

    public class PartDataInterface
    {
        public Dictionary<string, Part> parts { get; set; }
        public Dictionary<string, RawResource> rawResources { get; set; }
    }

    public class RawResource
    {
        public string name { get; set; }
        public double limit { get; set; }
    }
}
