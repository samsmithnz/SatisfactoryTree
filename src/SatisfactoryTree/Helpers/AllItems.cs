using SatisfactoryTree.Models;

namespace SatisfactoryTree.Helpers
{
    public static class AllItems
    {
        public static List<Item> GetAllItems()
        {
            List<Item> items = new()
            {
                ItemPoolTier1.IronOre(),
                ItemPoolTier1.IronIngot(),
                ItemPoolTier1.IronPlate(),
                ItemPoolTier1.IronRod(),
                ItemPoolTier1.CopperOre(),
                ItemPoolTier1.CopperIngot(),
                ItemPoolTier1.Wire(),
                ItemPoolTier1.Cable(),
                ItemPoolTier1.Limestone(),
                ItemPoolTier1.Concrete(),
                ItemPoolTier1.Screw(),
                ItemPoolTier1.ReinforcedIronPlate(),

                ItemPoolTier2.CopperSheet(),
                ItemPoolTier2.ModularFrame(),
                ItemPoolTier2.Rotor(),
                ItemPoolTier2.SmartPlating(),

                ItemPoolTier3.Coal(),
                ItemPoolTier3.SteelIngot(),
                ItemPoolTier3.SteelPipe(),
                ItemPoolTier3.SteelBeam(),
                ItemPoolTier3.VersatileFramework(),
                ItemPoolTier3.Water(),

                ItemPoolTier4.EncasedIndustrialBeam(),
                ItemPoolTier4.AutomatedWiring(),
                ItemPoolTier4.Stator(),
                ItemPoolTier4.Motor(),
                ItemPoolTier4.HeavyModularFrame(),


                ItemPoolTier5.CircuitBoard(),
                ItemPoolTier5.CrudeOil(),
                ItemPoolTier5.Plastic(),
                ItemPoolTier5.Rubber(),
                ItemPoolTier5.HeavyOilResidue(),
                ItemPoolTier5.PetroleumCoke(),
                ItemPoolTier5.Fuel(),
                ItemPoolTier5.Computer(),
                ItemPoolTier5.ModularEngine(),
                ItemPoolTier5.AdaptiveControlUnit(),

                ItemPoolTier6.CateriumOre(),
                ItemPoolTier6.CateriumIngot(),
                ItemPoolTier6.Quickwire(),
                ItemPoolTier6.HighSpeedConnector(),

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
            };

            return items;
        }
    }
}
