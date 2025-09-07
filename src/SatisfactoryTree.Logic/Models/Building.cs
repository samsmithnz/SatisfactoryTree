namespace SatisfactoryTree.Logic.Models
{
    public class Building
    {
        public Building(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Building otherBuilding)
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
