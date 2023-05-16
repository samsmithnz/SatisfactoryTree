using SatisfactoryTree.Models;
using BuildingType = SatisfactoryTree.Models.ManufactoringBuildingType;

namespace SatisfactoryTree.Helpers
{
    public static class ItemPoolTier8
    {
        //Level 8
        public static Item Uranium()
        {
            return new Item(8, "Uranium",
                "Uranium.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Uranium", 60 }
                        },
                        BuildingType.MiningMachine)
                }
            };
        }

        public static Item EncasedUraniumCell()
        {
            return new Item(8, "Encased Uranium Cell",
                "Encased_Uranium_Cell.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium", 50 },
                            { "Concrete", 15 },
                            { "Sulfuric Acid", 40 }
                        },
                        new()
                        {
                            { "Encased Uranium Cell", 25 },
                            { "Sulfuric Acid", 10 }
                        },
                        BuildingType.Blender,
                        ManufactoringMethodType.Manufactured,
                        true,
                        "Encased Uranium Cell")
                }
            };
        }

        public static Item ElectromagneticControlRod()
        {
            return new Item(8, "Electromagnetic Control Rod",
                "Electromagnetic_Control_Rod.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Stator", 6 },
                            { "AI Limiter", 4 }
                        },
                        new()
                        {
                            { "Electromagnetic Control Rod", 4 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item UraniumFuelRod()
        {
            return new Item(8, "Uranium Fuel Rod",
                "Uranium_Fuel_Rod.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Encased Uranium Cell", 20 },
                            { "Encased Industrial Beam", 1.2M },
                            { "Electromagnetic Control Rod", 2 }
                        },
                        new()
                        {
                            { "Uranium Fuel Rod", 0.4M }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item MagneticFieldGenerator()
        {
            return new Item(8, "Magnetic Field Generator",
                "Magnetic_Field_Generator.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Versatile Framework", 2.5M },
                            { "Electromagnetic Control Rod", 1 },
                            { "Battery", 5 }
                        },
                        new()
                        {
                            { "Magnetic Field Generator", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item NitrogenGas()
        {
            return new Item(8, "Nitrogen Gas",
                "Nitrogen_Gas.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new(),
                        new()
                        {
                            { "Nitrogen Gas", 120 }
                        },
                        BuildingType.ResourceWellExtractor)
                }
            };
        }

        public static Item HeatSink()
        {
            return new Item(8, "Heat Sink",
                "Heat_Sink.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Alclad Aluminum Sheet", 37.5M },
                            { "Copper Sheet", 3 }
                        },
                        new()
                        {
                            { "Heat Sink", 7.5M }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item CoolingSystem()
        {
            return new Item(8, "Cooling System",
                "Cooling_System.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Heat Sink", 12 },
                            { "Rubber", 12 },
                            { "Water", 30 },
                            { "Nitrogen Gas", 150 }
                        },
                        new()
                        {
                            { "Cooling System", 6 }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item FusedModularFrame()
        {
            return new Item(8, "Fused Modular Frame",
                "Fused_Modular_Frame.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Heavy Modular Frame", 1.5M },
                            { "Aluminum Casing", 75 },
                            { "Nitrogen Gas", 37.5M }
                        },
                        new()
                        {
                            { "Fused Modular Frame", 1.5M }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item TurboMotor()
        {
            return new Item(8, "Turbo Motor",
                "Turbo_Motor.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Cooling System", 7.5M },
                            { "Radio Control Unit", 3.75M },
                            { "Motor", 7.5M },
                            { "Rubber", 45 }
                        },
                        new()
                        {
                            { "Turbo Motor", 1.88M }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item ThermalPropulsionRocket()
        {
            return new Item(8, "Thermal Propulsion Rocket",
                "Thermal_Propulsion_Rocket.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Modular Engine", 2.5M },
                            { "Turbo Motor", 1 },
                            { "Cooling System", 3 },
                            { "Fused Modular Frame", 1 }
                        },
                        new()
                        {
                            { "Thermal Propulsion Rocket", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item NitricAcid()
        {
            return new Item(8, "Nitric Acid",
                "Nitric_Acid.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Nitrogen Gas", 120 },
                            { "Water", 30 },
                            { "Iron Plate", 10 }
                        },
                        new()
                        {
                            { "Nitric Acid", 30 }
                        },
                        BuildingType.Blender)
                }
            };
        }

        public static Item UraniumWaste()
        {
            return new Item(8, "Uranium Waste",
                "Uranium_Waste.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium Fuel Rod", 0.2M },
                            { "Water", 240 }
                        },
                        new()
                        {
                            { "Uranium Waste", 10 }
                        },
                        BuildingType.NuclearPowerPlant)
                }
            };
        }

        public static Item NonfissileUranium()
        {
            return new Item(8, "Non-fissile Uranium",
                "Non-fissile_Uranium.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Uranium Waste", 37.5M },
                            { "Silica", 25 },
                            { "Nitric Acid", 15 },
                            { "Sulfuric Acid", 15 }
                        },
                        new()
                        {
                            { "Non-fissile Uranium", 50 },
                            { "Water", 15 }
                        },
                        BuildingType.Blender,
                        ManufactoringMethodType.Manufactured,
                        true,
                        "Non-fissile Uranium")
                }
            };
        }

        public static Item PlutoniumPellet()
        {
            return new Item(8, "Plutonium Pellet",
                "Plutonium_Pellet.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Non-fissile Uranium", 100 },
                            { "Uranium Waste", 25 }
                        },
                        new()
                        {
                            { "Plutonium Pellet", 30 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item EncasedPlutoniumCell()
        {
            return new Item(8, "Encased Plutonium Cell",
                "Encased_Plutonium_Cell.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Plutonium Pellet", 10 },
                            { "Concrete", 20 }
                        },
                        new()
                        {
                            { "Encased Plutonium Cell", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item PlutoniumFuelRod()
        {
            return new Item(8, "Plutonium Fuel Rod",
                "Plutonium_Fuel_Rod.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(
                        new()
                        {
                            { "Encased Plutonium Cell", 30 },
                            { "Steel Beam", 18 },
                            { "Electromagnetic Control Rod", 6 },
                            { "Heat Sink", 10 }
                        },
                        new()
                        {
                            { "Plutonium Fuel Rod", 1 }
                        },
                        BuildingType.Manufacturer)
                }
            };
        }

        public static Item PlutoniumWaste()
        {
            return new Item(8, "Plutonium Waste",
                "Plutonium_Waste.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(600,
                        1,
                        new()
                        {
                            { "Plutonium Fuel Rod", 1 },
                            { "Water", 3000 }
                        },
                        new()
                        {
                            { "Plutonium Waste", 10 }
                        },
                        BuildingType.NuclearPowerPlant)
                }
            };
        }

        public static Item CopperPowder()
        {
            return new Item(8, "Copper Powder",
                "Copper_Powder.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(6,
                        50,
                        new()
                        {
                            { "Copper Ingot", 30 }
                        },
                        new()
                        {
                            { "Copper Powder", 5 }
                        },
                        BuildingType.Constructor)
                }
            };
        }

        public static Item PressureConversionCube()
        {
            return new Item(8, "Pressure Conversion Cube",
                "Pressure_Conversion_Cube.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(60,
                        1,
                        new()
                        {
                            { "Fused Modular Frame", 1 },
                            { "Radio Control Unit", 2 }
                        },
                        new()
                        {
                            { "Pressure Conversion Cube", 1 }
                        },
                        BuildingType.Assembler)
                }
            };
        }

        public static Item NuclearPasta()
        {
            return new Item(8, "Nuclear Pasta",
                "Nuclear_Pasta.webp",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(120,
                        0.5m,
                        new()
                        {
                            { "Copper Powder", 200 },
                            { "Pressure Conversion Cube", 1 }
                        },
                        new()
                        {
                            { "Nuclear Pasta", 1 }
                        },
                        BuildingType.ParticleAccelerator)
                }
            };
        }



    }
}
