using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public List<Part> Imports { get; set; }
        public List<Part> Surplus { get; set; }
        public List<Part> PartGoals { get; set; }
        public List<Part> Parts { get; set; }
    }
}
