namespace SatisfactoryTree.Models
{
    public class NewBuilding
    {
        public NewBuilding(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is NewBuilding otherBuilding)
            {
                return Name == otherBuilding.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
