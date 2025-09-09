namespace SatisfactoryTree.Logic.Models
{
    public class PowerIngredient
    {
        public string part { get; set; } = string.Empty;
        public double perMin { get; set; }
        public double? mwPerItem { get; set; }
        public double? supplementalRatio { get; set; }
    }
}
