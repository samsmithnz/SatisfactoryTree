namespace SatisfactoryTree.Logic.Models
{
    public class Part
    {
        public string name { get; set; } = string.Empty;
        public int stackSize { get; set; }
        public bool isFluid { get; set; }
        public bool isFicsmas { get; set; }
        public double energyGeneratedInMJ { get; set; }
    }
}
