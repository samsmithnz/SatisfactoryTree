using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public class BuildingPool
    {
        //Extraction
        public static Building MiningMachineMk1()
        {
            return new Building("Mining Machine Mk1",
                "MinerMk1_256.png",
                ManufactoringBuildingType.MiningMachine,
                5M);
        }
        public static Building MiningMachineMk2()
        {
            return new Building("Mining Machine Mk2",
                "MinerMk2_256.png",
                ManufactoringBuildingType.MiningMachine,
                12M);
        }
        public static Building MiningMachineMk3()
        {
            return new Building("Mining Machine Mk3",
                "MinerMk3_256.png",
                ManufactoringBuildingType.MiningMachine,
                30M);
        }

        public static Building OilExtractor()
        {
            return new Building("Oil Extractor",
                "OilExtractor_256.png",
                ManufactoringBuildingType.OilExtractor,
                40M);
        }

        public static Building WaterExtractor()
        {
            return new Building("Water Extractor",
                "WaterExtractor_256.png",
                ManufactoringBuildingType.WaterExtractor,
                20M);
        }

        public static Building ResourceWellPressurizer()
        {
            return new Building("Resource Well Pressurizer",
                "ResourceWellPressurizer_256.png",
                ManufactoringBuildingType.ResourceWellPressurizer,
                150M);
        }

        public static Building ResourceWellExtractor()
        {
            return new Building("Resource Well Extractor",
                "ResourceWellExtractor_256.png",
                ManufactoringBuildingType.ResourceWellExtractor,
                0M);
        }

        //Production
        public static Building Smeltor()
        {
            return new Building("Smeltor",
                "SmeltorMk1_256.png",
                ManufactoringBuildingType.Smelter,
                4M);
        }
        public static Building Foundry()
        {
            return new Building("Assembler",
                "AssemblerMk1_256.png",
                ManufactoringBuildingType.Assembler,
                15M);
        }

        public static Building Constructor()
        {
            return new Building("Constructor",
                "ConstructorMk1_256.png",
                ManufactoringBuildingType.Constructor,
                4M);
        }

        public static Building Assembler()
        {
            return new Building("Assembler",
                "AssemblerMk1_256.png",
                ManufactoringBuildingType.Assembler,
                15M);
        }

        public static Building Manufacturer()
        {
            return new Building("Manufacturer",
                "ManufacturerMk1_256.png",
                ManufactoringBuildingType.Manufacturer,
                15M);
        }

        public static Building Manufacturer()
        {
            return new Building("Manufacturer",
                "ManufacturerMk1_256.png",
                ManufactoringBuildingType.Manufacturer,
                15M);
        }

        public static Building Packager()
        {
            return new Building("Packager",
                "Packager_256.png",
                ManufactoringBuildingType.Packager,
                10M);
        }

        public static Building Blender()
        {
            return new Building("Blender",
                "Blender_256.png",
                ManufactoringBuildingType.Blender,
                75M);
        }

        public static Building ParticleAccelerator()
        {
            return new Building("Particle Accelerator",
                "ParticleAccelerator_256.png",
                ManufactoringBuildingType.ParticleAccelerator,
                250M);
        }
    }
}
