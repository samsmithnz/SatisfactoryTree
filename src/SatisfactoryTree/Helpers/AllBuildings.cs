//using SatisfactoryTree.Models;

//namespace SatisfactoryTree.Helpers
//{
//    public static class AllBuildings
//    {
//        public static List<Building> GetAllBuildings()
//        {
//            List<Building> buildings = new()
//            {
//               BuildingPool.Assembler(),
//               BuildingPool.BiomassBurner(),
//               BuildingPool.Blender(),
//               BuildingPool.CoalGenerator(),
//               BuildingPool.Constructor(),
//               BuildingPool.Foundry(),
//               BuildingPool.FuelGenerator(),
//               BuildingPool.GeothermalPowerGenerator(),
//               BuildingPool.Manufacturer(),
//               BuildingPool.MiningMachineMk1(),
//               BuildingPool.MiningMachineMk2(),
//               BuildingPool.MiningMachineMk3(),
//               BuildingPool.NuclearPowerPlant(),
//               BuildingPool.OilExtractor(),
//               BuildingPool.Packager(),
//               BuildingPool.ParticleAccelerator(),
//               BuildingPool.Refinery(),
//               BuildingPool.ResourceSink(),
//               BuildingPool.ResourceWellExtractor(),
//               BuildingPool.ResourceWellPressurizer(),
//               BuildingPool.Smelter(),
//               BuildingPool.WaterExtractor()
//            };

//            return buildings;
//        }

//        public static Building FindBuilding(string name)
//        {
//            List<Building> buildings = GetAllBuildings();
//            foreach (Building item in buildings)
//            {
//                if (item.Name == name)
//                {
//                    return item;
//                }
//            }
//            return null;
//        }
//    }
//}
