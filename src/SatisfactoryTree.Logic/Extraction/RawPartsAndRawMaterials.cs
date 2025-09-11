using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Extraction
{
    public class RawPartsAndRawMaterials
    {
        public Dictionary<string, Part> Parts { get; set; }
        public Dictionary<string, RawResource> RawResources { get; set; }
    }
}
