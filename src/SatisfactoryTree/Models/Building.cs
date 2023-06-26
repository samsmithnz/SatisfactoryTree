namespace SatisfactoryTree.Models
{
    public class Building
    {
        public Building(string name,
            string image,
            ManufactoringBuildingType buildingType,
            decimal powerConsumption)
        {
            Name = name; ;
            Image = image;
            BuildingType = buildingType;
            PowerConsumption = powerConsumption;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public ManufactoringBuildingType BuildingType { get; set; };
        public decimal PowerConsumption { get; set; }
    }
}
