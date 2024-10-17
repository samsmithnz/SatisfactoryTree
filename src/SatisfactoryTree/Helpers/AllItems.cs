//using SatisfactoryTree.Models;

//namespace SatisfactoryTree.Helpers
//{
//    public static class AllItems
//    {
//        public static List<Item> GetAllItems()
//        {
//            List<Item> items = new()
//            {
//                ItemPoolTier0.IronOre(),
//                ItemPoolTier0.CopperOre(),
//                ItemPoolTier0.Limestone(),
//                ItemPoolTier0.AlienProtein(),
//                ItemPoolTier0.Leaves(),
//                ItemPoolTier0.Mycelia(),
//                ItemPoolTier0.Wood(),
//                ItemPoolTier0.Coal(),
//                ItemPoolTier0.Water(),
//                ItemPoolTier0.CrudeOil(),
//                ItemPoolTier0.CateriumOre(),
//                ItemPoolTier0.Bauxite(),
//                ItemPoolTier0.RawQuartz(),
//                ItemPoolTier0.Sulfur(),
//                ItemPoolTier0.Uranium(),
//                ItemPoolTier0.NitrogenGas(),

//                ItemPoolTier1.IronIngot(),
//                ItemPoolTier1.CopperIngot(),
//                ItemPoolTier1.Concrete(),
//                ItemPoolTier1.Biomass(),
//                ItemPoolTier1.SteelIngot(),
//                ItemPoolTier1.Plastic(),
//                ItemPoolTier1.Rubber(),
//                ItemPoolTier1.CateriumIngot(),
//                ItemPoolTier1.AluminaSolution(),
//                ItemPoolTier1.QuartzCrystal(),
//                ItemPoolTier1.Silica(),
//                ItemPoolTier1.SulfuricAcid(),

//                ItemPoolTier2.IronPlate(),
//                ItemPoolTier2.IronRod(),
//                ItemPoolTier2.Wire(),
//                ItemPoolTier2.CopperSheet(),
//                ItemPoolTier2.Cable(),
//                ItemPoolTier2.Screw(),
//                ItemPoolTier2.SolidBiofuel(),
//                ItemPoolTier2.SteelPipe(),
//                ItemPoolTier2.SteelBeam(),
//                ItemPoolTier2.HeavyOilResidue(),
//                ItemPoolTier2.Quickwire(),
//                ItemPoolTier2.AluminumScrap(),
//                ItemPoolTier2.EncasedUraniumCell(),
//                ItemPoolTier2.CopperPowder(),

//                ItemPoolTier3.ReinforcedIronPlate(),
//                ItemPoolTier3.Rotor(),
//                ItemPoolTier3.CoalPowerGeneration(),
//                ItemPoolTier3.SolidBiofuelPowerGeneration(),
//                ItemPoolTier3.EncasedIndustrialBeam(),
//                ItemPoolTier3.Stator(),
//                ItemPoolTier3.Fuel(),
//                ItemPoolTier3.PetroleumCoke(),
//                ItemPoolTier3.CircuitBoard(),
//                ItemPoolTier3.AluminumIngot(),
//                ItemPoolTier3.AILimiter(),        
//                ItemPoolTier3.NitricAcid(),

//                ItemPoolTier4.SmartPlating(),
//                ItemPoolTier4.ModularFrame(),
//                ItemPoolTier4.AutomatedWiring(),
//                ItemPoolTier4.Motor(),
//                ItemPoolTier4.Computer(),
//                ItemPoolTier4.HighSpeedConnector(),
//                ItemPoolTier4.FuelPowerGeneration(),
//                ItemPoolTier4.AlcladAluminumSheet(),
//                ItemPoolTier4.AluminumCasing(),
//                ItemPoolTier4.CrystalOscillator(),
//                ItemPoolTier4.ElectromagneticControlRod(),

//                ItemPoolTier5.HeavyModularFrame(),
//                ItemPoolTier5.VersatileFramework(),
//                ItemPoolTier5.ModularEngine(),
//                ItemPoolTier5.Battery(),
//                ItemPoolTier5.RadioControlUnit(),
//                ItemPoolTier5.Supercomputer(),
//                ItemPoolTier5.UraniumFuelRod(),
//                ItemPoolTier5.HeatSink(),

//                ItemPoolTier6.AdaptiveControlUnit(),
//                ItemPoolTier6.MagneticFieldGenerator(),
//                ItemPoolTier6.CoolingSystem(),
//                ItemPoolTier6.FusedModularFrame(),
//                ItemPoolTier6.UraniumWaste(),
//                ItemPoolTier6.NuclearPowerGeneration(),

//                ItemPoolTier7.AssemblyDirectorSystem(),
//                ItemPoolTier7.TurboMotor(),
//                ItemPoolTier7.NonfissileUranium(),
//                ItemPoolTier7.PressureConversionCube(),

//                ItemPoolTier8.ThermalPropulsionRocket(),
//                ItemPoolTier8.PlutoniumPellet(),
//                ItemPoolTier8.NuclearPasta(),

//                ItemPoolTier9.EncasedPlutoniumCell(),

//                ItemPoolTierA.PlutoniumFuelRod(),
                
//                ItemPoolTierB.PlutoniumWaste(),
//            };

//            return items;
//        }

//        public static Item? FindItem(string name)
//        {
//            List<Item> items = GetAllItems();
//            foreach (Item item in items)
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
