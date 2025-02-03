namespace SatisfactoryTree.Logic.Models
{
    public class Ingredient
    {
        public Ingredient(string partId, string partName, string partImage, double quantity)
        {
            PartId = partId;
            PartName = partName;
            PartImage = "" + partImage + "_64.png";
            Quantity = quantity;
        }
        public string PartId { get; set; }
        public string PartName { get; set; }
        public string PartImage { get; set; }
        public double Quantity { get; set; }
    }
}
