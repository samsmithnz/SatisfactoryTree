
namespace SatisfactoryTree.Logic.Models
{
    public class Fuel
    {
        public string primaryFuel { get; set; }
        public string supplementaryFuel { get; set; }
        public string byProduct { get; set; } = string.Empty;
        public double byProductAmount { get; set; }
        public double byProductAmountPerMin { get; set; }
        public double burnDurationInS { get; set; }
    }
}
