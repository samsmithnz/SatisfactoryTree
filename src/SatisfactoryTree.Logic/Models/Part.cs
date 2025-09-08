using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Models
{
    public class Part
    {
        public string name { get; set; }
        public int stackSize { get; set; }
        public bool isFluid { get; set; }
        public bool isFicsmas { get; set; }
        public double energyGeneratedInMJ { get; set; }
    }
}
