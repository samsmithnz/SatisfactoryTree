using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class AllItems
    {
        public static List<Item> GetAllItems()
        {
            List<Item> items = new()
            {
                ItemPoolTier0.IronOre(),
                ItemPoolTier0.CopperOre(),
                ItemPoolTier0.Limestone(),
                ItemPoolTier0.AlienProtein(),
                ItemPoolTier0.Leaves(),
                ItemPoolTier0.Mycelia(),
                ItemPoolTier0.Wood(),
                ItemPoolTier0.Coal(),
                ItemPoolTier0.Water(),
                ItemPoolTier0.CrudeOil(),
                ItemPoolTier0.CateriumOre(),

                ItemPoolTier1.IronIngot(),
                ItemPoolTier1.CopperIngot(),
                ItemPoolTier1.Concrete(),
                ItemPoolTier1.Biomass(),
                ItemPoolTier1.SteelIngot(),
                ItemPoolTier1.Plastic(),
                ItemPoolTier1.Rubber(),
                ItemPoolTier1.CateriumIngot(),

                ItemPoolTier2.IronPlate(),
                ItemPoolTier2.IronRod(),
                ItemPoolTier2.Wire(),
                ItemPoolTier2.CopperSheet(),
                ItemPoolTier2.Cable(),
                ItemPoolTier2.Screw(),
                ItemPoolTier2.SolidBiofuel(),
                ItemPoolTier2.SteelPipe(),
                ItemPoolTier2.SteelBeam(),
                ItemPoolTier2.HeavyOilResidue(),
                ItemPoolTier2.Quickwire(),

                ItemPoolTier3.ReinforcedIronPlate(),
                ItemPoolTier3.Rotor(),
                ItemPoolTier3.CoalPowerGeneration(),
                ItemPoolTier3.SolidBiofuelPowerGeneration(),
                ItemPoolTier3.EncasedIndustrialBeam(),
                ItemPoolTier3.Stator(),
                ItemPoolTier3.Fuel(),
                ItemPoolTier3.PetroleumCoke(),
                ItemPoolTier3.CircuitBoard(),

                ItemPoolTier4.SmartPlating(),
                ItemPoolTier4.ModularFrame(),
                ItemPoolTier4.AutomatedWiring(),
                ItemPoolTier4.Motor(),
                ItemPoolTier4.Computer(),
                ItemPoolTier4.HighSpeedConnector(),
                ItemPoolTier4.FuelPowerGeneration(),

                ItemPoolTier5.HeavyModularFrame(),
                ItemPoolTier5.VersatileFramework(),
                ItemPoolTier5.ModularEngine(),

                ItemPoolTier6.AdaptiveControlUnit(),

                ItemPoolTier7.Bauxite(),
                ItemPoolTier7.AluminaSolution(),
                ItemPoolTier7.AluminumScrap(),
                ItemPoolTier7.AluminumIngot(),
                ItemPoolTier7.AlcladAluminumSheet(),
                ItemPoolTier7.AluminumCasing(),
                ItemPoolTier7.RawQuartz(),
                ItemPoolTier7.QuartzCrystal(),
                ItemPoolTier7.Silica(),
                ItemPoolTier7.CrystalOscillator(),
                ItemPoolTier7.RadioControlUnit(),
                ItemPoolTier7.Sulfur(),
                ItemPoolTier7.SulfuricAcid(),
                ItemPoolTier7.Battery(),
                ItemPoolTier7.AILimiter(),
                ItemPoolTier7.Supercomputer(),
                ItemPoolTier7.AssemblyDirectorSystem(),

                ItemPoolTier8.Uranium(),
                ItemPoolTier8.EncasedUraniumCell(),
                ItemPoolTier8.ElectromagneticControlRod(),
                ItemPoolTier8.UraniumFuelRod(),
                ItemPoolTier8.MagneticFieldGenerator(),
                ItemPoolTier8.NitrogenGas(),
                ItemPoolTier8.HeatSink(),
                ItemPoolTier8.CoolingSystem(),
                ItemPoolTier8.FusedModularFrame(),
                ItemPoolTier8.TurboMotor(),
                ItemPoolTier8.ThermalPropulsionRocket(),
                ItemPoolTier8.NitricAcid(),
                ItemPoolTier8.UraniumWaste(),
                ItemPoolTier8.NonfissileUranium(),
                ItemPoolTier8.PlutoniumPellet(),
                ItemPoolTier8.EncasedPlutoniumCell(),
                ItemPoolTier8.PlutoniumFuelRod(),
                ItemPoolTier8.PlutoniumWaste(),
                ItemPoolTier8.CopperPowder(),
                ItemPoolTier8.PressureConversionCube(),
                ItemPoolTier8.NuclearPasta(),
                ItemPoolTier8.NuclearPowerGeneration(),
            };

            return items;
        }
    }
}
