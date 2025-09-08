using SatisfactoryTree.Logic.Extraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SatisfactoryTree.Logic.Models
{
    public class Factory
    {
        public List<RawPart> Imports { get; set; }
        public List<RawPart> Surplus { get; set; }
        public List<RawPart> PartGoals { get; set; }
        public List<RawPart> Parts { get; set; }

        public Factory()
        {
            Imports = new List<RawPart>();
            Surplus = new List<RawPart>();
            PartGoals = new List<RawPart>();
            Parts = new List<RawPart>();
        }
    }
}
