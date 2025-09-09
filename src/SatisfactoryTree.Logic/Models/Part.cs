namespace SatisfactoryTree.Logic.Models
{
    public class Part
    {
        public string Name { get; set; } = string.Empty;
        public int StackSize { get; set; }
        public bool IsFluid { get; set; }
        public bool IsFicsmas { get; set; }
        public double EnergyGeneratedInMJ { get; set; }
    }
}
