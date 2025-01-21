using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Models
{
    public class Product
    {
        public Product(string partId, string partImage, string recipeId, double quantity)
        {
            PartId = partId;
            PartImage = "" + partImage + "_64.png";
            RecipeId = recipeId;
            Quantity = quantity;
        }
        public string PartId { get; set; }
        public string PartImage { get; set; }
        public string RecipeId { get; set; }
        public double Quantity { get; set; }
    }
}
