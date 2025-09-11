namespace SatisfactoryTree.Logic.Models
{
    public class Building
    {
        public string Name { get; set; }
        public double Power { get; set; }
        public double? MinPower { get; set; }
        public double? MaxPower { get; set; }

        // Parameterless constructor for JSON deserialization
        public Building()
        {
            Name = string.Empty;
        }

        public Building(string name)
        {
            Name = name;
        }

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
