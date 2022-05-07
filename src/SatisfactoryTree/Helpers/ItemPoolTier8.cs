using DSPTree.Models;
using BuildingType = DSPTree.Models.ManufactoringBuildingType;
//using MethodType = DSPTree.Models.ManufactoringMethodType;

namespace DSPTree.Helpers
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
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Uranium", 1 }
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
                    new Recipe(12,
                        25,
                        new()
                        {
                            { "Uranium", 10 },
                            { "Concrete", 3 },
                            { "Sulfuric Acid", 8 }
                        },
                        new()
                        {
                            { "Encased Uranium Cell", 5 },
                            { "Sulfuric Acid", 2 }
                        },
                        BuildingType.Blender)
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
                    new Recipe(30,
                        4,
                        new()
                        {
                            { "Stator", 3 },
                            { "AI Limiter", 2 }
                        },
                        new()
                        {
                            { "Electromagnetic Control Rod", 2 }
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
                    new Recipe(150,
                        0.4m,
                        new()
                        {
                            { "Encased Uranium Cell", 50 },
                            { "Encased Industrial Beam", 3 },
                            { "Electromagnetic Control Rod", 5 }
                        },
                        new()
                        {
                            { "Uranium Fuel Rod", 1 }
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
                    new Recipe(120,
                        1,
                        new()
                        {
                            { "Versatile Framework", 5 },
                            { "Electromagnetic Control Rod", 2 },
                            { "Battery", 10 }
                        },
                        new()
                        {
                            { "Magnetic Field Generator", 2 }
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
                    new Recipe(1,
                        60,
                        new(),
                        new()
                        {
                            { "Nitrogen Gas", 1 }
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
                    new Recipe(8,
                        7.5m,
                        new()
                        {
                            { "Alclad Aluminum Sheet", 5 },
                            { "Copper Sheet", 3 }
                        },
                        new()
                        {
                            { "Heat Sink", 1 }
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
                    new Recipe(10,
                        6,
                        new()
                        {
                            { "Heat Sink", 2 },
                            { "Rubber", 2 },
                            { "Water", 5 },
                            { "Nitrogen Gas", 25 }
                        },
                        new()
                        {
                            { "Cooling System", 4 }
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
                    new Recipe(40,
                        1.5m,
                        new()
                        {
                            { "Heavy Modular Frame", 1 },
                            { "Aluminum Casing", 50 },
                            { "Nitrogen Gas", 25 }
                        },
                        new()
                        {
                            { "Fused Modular Frame", 99 }
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
                    new Recipe(32,
                        1.875m,
                        new()
                        {
                            { "Cooling System", 4 },
                            { "Radio Control Unit", 2 },
                            { "Motor", 4 },
                            { "Rubber", 24 }
                        },
                        new()
                        {
                            { "Turbo Motor", 1 }
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
                    new Recipe(120,
                        1,
                        new()
                        {
                            { "Modular Engine", 5 },
                            { "Turbo Motor", 2 },
                            { "Cooling System", 6 },
                            { "Fused Modular Frame", 2 }
                        },
                        new()
                        {
                            { "Thermal Propulsion Rocket", 2 }
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
                    new Recipe(6,
                        30,
                        new()
                        {
                            { "Nitrogen Gas", 12 },
                            { "Water", 3 },
                            { "Iron Plate", 1 }
                        },
                        new()
                        {
                            { "Nitric Acid", 3 }
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
                    new Recipe(300,
                        10,
                        new()
                        {
                            { "Uranium Fuel Rod", 1 },
                            { "Sulfuric Acid", 1500 }
                        },
                        new()
                        {
                            { "Uranium Waste", 50 }
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
                    new Recipe(24,
                        50,
                        new()
                        {
                            { "Uranium Waste", 15 },
                            { "Silica", 10 },
                            { "Nitric Acid", 6 },
                            { "Sulfuric Acid", 6 }
                        },
                        new()
                        {
                            { "Non-fissile Uranium", 20 },
                            { "Water", 15 }
                        },
                        BuildingType.Blender)
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
                    new Recipe(60,
                        30,
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
                "Encased_Plutonium_Cell",
                ItemType.Item,
                ResearchType.Tier8)
            {
                Recipes =
                {
                    new Recipe(12,
                        5,
                        new()
                        {
                            { "Plutonium Pellet", 2 },
                            { "Concrete", 4 }
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
                    new Recipe(240,
                        0.25m,
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
