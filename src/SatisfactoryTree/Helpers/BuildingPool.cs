using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public class BuildingPool
    {
        public static Building MiningMachineMk1()
        {
            return new Building("Mining Machine",
                "MinerMk3_256.png",
                ManufactoringBuildingType.MiningMachine);
        }

        public static Building AssemblerMk1()
        {
            return new Building("Assembler",
                "AssemblerMk1_256.png",
                ManufactoringBuildingType.Assembler);
        }

        public static Building ConstructorMk1()
        {
            return new Building("Constructor",
                "ConstructorMk1_256.png",
                ManufactoringBuildingType.Constructor);
        }
    }
}
