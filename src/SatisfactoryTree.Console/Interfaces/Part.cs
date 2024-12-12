namespace SatisfactoryTree.Console.Interfaces
{
    public class Part
    {
        public string Name { get; set; }
        public int StackSize { get; set; }
        public bool IsFluid { get; set; }
        public bool IsFicsmas { get; set; }
        public double EnergyGeneratedInMJ { get; set; }
    }

    public class PartDataInterface
    {
        public Dictionary<string, Part> Parts { get; set; }
        public Dictionary<string, RawResource> RawResources { get; set; }
    }

    public class RawResource
    {
        public string Name { get; set; }
        public double Limit { get; set; }
    }
}
