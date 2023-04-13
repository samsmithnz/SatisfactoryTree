namespace SatisfactoryTree.Models
{
    public class Building
    {
        public Building(string name,
            string image,
            ManufactoringBuildingType buildingType)
        {
            Name = name; ;
            Image = image;
            BuildingType = buildingType;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public ManufactoringBuildingType BuildingType { get; set; }
    }
}
