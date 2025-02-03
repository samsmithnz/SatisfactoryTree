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
