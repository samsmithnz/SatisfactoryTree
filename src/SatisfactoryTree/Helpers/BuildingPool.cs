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
                ManufactoringBuildingType.Extraction,
                5M);
        }
        public static Building MiningMachineMk2()
        {
            return new Building("Mining Machine Mk2",
                "MinerMk2_256.png",
                ManufactoringBuildingType.Extraction,
                12M);
        }
        public static Building MiningMachineMk3()
        {
            return new Building("Mining Machine Mk3",
                "MinerMk3_256.png",
                ManufactoringBuildingType.Extraction,
                30M);
        }

        public static Building OilExtractor()
        {
            return new Building("Oil Extractor",
                "OilExtractor_256.png",
                ManufactoringBuildingType.Extraction,
                40M);
        }

        public static Building WaterExtractor()
        {
            return new Building("Water Extractor",
                "WaterExtractor_256.png",
                ManufactoringBuildingType.Extraction,
                20M);
        }

        public static Building ResourceWellPressurizer()
        {
            return new Building("Resource Well Pressurizer",
                "ResourceWellPressurizer_256.png",
                ManufactoringBuildingType.Extraction,
                150M);
        }

        public static Building ResourceWellExtractor()
        {
            return new Building("Resource Well Extractor",
                "ResourceWellExtractor_256.png",
                ManufactoringBuildingType.Extraction,
                0M);
        }

        //Production
        public static Building Smeltor()
        {
            return new Building("Smeltor",
                "SmeltorMk1_256.png",
                ManufactoringBuildingType.Production,
                4M);
        }
        public static Building Foundry()
        {
            return new Building("Foundry",
                "Foundry_256.png",
                ManufactoringBuildingType.Production,
                16M);
        }

        public static Building Constructor()
        {
            return new Building("Constructor",
                "ConstructorMk1_256.png",
                ManufactoringBuildingType.Production,
                4M);
        }

        public static Building Assembler()
        {
            return new Building("Assembler",
                "AssemblerMk1_256.png",
                ManufactoringBuildingType.Production,
                15M);
        }

        public static Building Manufacturer()
        {
            return new Building("Manufacturer",
                "Manufacturer_256.png",
                ManufactoringBuildingType.Production,
                55M);
        }

        public static Building Refinery()
        {
            return new Building("Refinery",
                "OilRefinery_256.png",
                ManufactoringBuildingType.Production,
                30M);
        }

        public static Building Packager()
        {
            return new Building("Packager",
                "Packager_256.png",
                ManufactoringBuildingType.Production,
                10M);
        }

        public static Building Blender()
        {
            return new Building("Blender",
                "Blender_256.png",
                ManufactoringBuildingType.Production,
                75M);
        }

        public static Building ParticleAccelerator()
        {
            return new Building("Particle Accelerator",
                "ParticleAccelerator_256.png",
                ManufactoringBuildingType.Production,
                250M);
        }

        //Power Generation
        public static Building BiomassBurner()
        {
            return new Building("Biomass Burner",
                "BiomassBurner_256.png",
                ManufactoringBuildingType.PowerGeneration,
                0M,
                30M);
        }

        public static Building CoalGenerator()
        {
            return new Building("Biomass Burner",
                "BiomassBurner_256.png",
                ManufactoringBuildingType.PowerGeneration,
                0M,
                75M);
        }

        public static Building FuelGenerator()
        {
            return new Building("Biomass Burner",
                "BiomassBurner_256.png",
                ManufactoringBuildingType.PowerGeneration,
                0M,
                150M);
        }

        public static Building GeothermalPowerGenerator()
        {
            return new Building("Geothermal Power Generator",
                "GeothermalPowerGenerator_256.png",
                ManufactoringBuildingType.PowerGeneration,
                0M,
                200M); // Actual varies constantly: https://satisfactory-calculator.com/en/buildings/detail/id/Build_GeneratorGeoThermal_C/name/Geothermal+Generator
        }

        public static Building NuclearPower()
        {
            return new Building("Biomass Burner",
                "BiomassBurner_256.png",
                ManufactoringBuildingType.PowerGeneration,
                0M,
                2500M);
        }

        //Special
        public static Building ResourceSink()
        {
            return new Building("Resource Sink",
                "ResourceSink_256.png",
                ManufactoringBuildingType.Special,
                30M);
        }
    }
}
