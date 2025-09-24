namespace SatisfactoryTree.Logic.Models
{
    public class LookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public LookupItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
