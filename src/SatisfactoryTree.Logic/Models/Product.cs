namespace SatisfactoryTree.Logic.Models
{
    public class Product
    {
        public string part { get; set; } = string.Empty;
        public double amount { get; set; }
        public double perMin { get; set; }
        public bool? isByProduct { get; set; }
    }
}
