using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.Logic.Models
{
    public class Part
    {
        public Part(string partId, string partName)
        {
            PartId = partId;
            PartName = partName;
        }
        public string PartId { get; set; }
        public string PartName { get; set; }
    }
}
