using DSPTree.Helpers;
using DSPTree.Models;

namespace DSPTree
{
    public class DSPGraph
    {
        public List<Item> Items { get; set; }
        public DSPGraph(string filter = "",
            ResearchType researchType = ResearchType.Tier8,
            bool includeBuildings = false,
            bool showOnlyDirectDependencies = false)
        {
            Items = BuildDSPTree(filter,
                researchType,
                includeBuildings,
                showOnlyDirectDependencies);
        }

        private static List<Item> BuildDSPTree(string nameFilter,
            ResearchType researchType,
            bool includeBuildings,
            bool showOnlyDirectDependencies)
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

                
            };

            ////Include buildings
            //if (includeBuildings == true)
            //{
            //    List<Item> buildings = new()
            //    {
            //        BuildingsNoScience.ConveyorBeltMkI(),

            //        BuildingsPreBlueScience.TeslaTower(),
            //        BuildingsPreBlueScience.WindTurbine(),
            //        BuildingsPreBlueScience.SorterMkI(),
            //        BuildingsPreBlueScience.AssemblingMachineMkI(),
            //        BuildingsPreBlueScience.MiningMachine(),
            //        BuildingsPreBlueScience.ArcSmelter(),
            //        BuildingsPreBlueScience.StorageMkI(),
            //        BuildingsPreBlueScience.MatrixLab(),

            //        BuildingsBlueScience.WirelessPowerTower(),
            //        BuildingsBlueScience.ThermalPowerPlant(),
            //        BuildingsBlueScience.SolarPanel(),
            //        BuildingsBlueScience.Splitter(),
            //        BuildingsBlueScience.OilExtractor(),
            //        BuildingsBlueScience.OilRefinery(),
            //        BuildingsBlueScience.WaterPump(),
            //        BuildingsBlueScience.StorageTank(),
            //        BuildingsBlueScience.SorterMkII(),
            //        BuildingsBlueScience.TrafficMonitor(),
            //        BuildingsBlueScience.ChemicalPlant(),
            //        BuildingsBlueScience.SprayCoater(),
            //        BuildingsBlueScience.Foundation(),

            //        BuildingsRedScience.Accumulator(),
            //        BuildingsRedScience.StorageMkII(),
            //        BuildingsRedScience.ConveyorBeltMkII(),
            //        BuildingsRedScience.AssemblingMachineMkII(),
            //        BuildingsRedScience.ConveyorBeltMkIII(),
            //        BuildingsRedScience.SorterMkIII(),
            //        BuildingsRedScience.Fractionator(),
            //        BuildingsRedScience.EMRailEjector(),
            //        BuildingsRedScience.RayReceiver(),
            //        BuildingsRedScience.PlanetaryLogisticsStation(),
            //        BuildingsRedScience.LogisticsDrone(),

            //        BuildingsYellowScience.SatelliteSubstation(),
            //        BuildingsYellowScience.MiniFusionPowerPlant(),
            //        BuildingsYellowScience.EnergyExchanger(),
            //        BuildingsYellowScience.FullAccumulator(),
            //        BuildingsYellowScience.MiniatureParticleCollider(),
            //        BuildingsYellowScience.InterstellarLogisticsStation(),
            //        BuildingsYellowScience.OrbitalCollector(),
            //        BuildingsYellowScience.AutomaticPiler(),
            //        BuildingsYellowScience.LogisticsVessel(),

            //        BuildingsPurpleScience.AssemblingMachineMkIII(),
            //        BuildingsPurpleScience.PlaneSmelter(),
            //        BuildingsPurpleScience.VerticalLaunchingSilo(),
            //        BuildingsPurpleScience.SmallCarrierRocket(),

            //        BuildingsGreenScience.ArtificialStar(),
            //        BuildingsGreenScience.AdvancedMiningMachine(),

            //        //BuildingsWhiteScience.(),
            //    };
            //    items.AddRange(buildings);
            //}

            //Filter by science level
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].ResearchType > researchType)
                {
                    items.RemoveAt(i);
                }
            }

            //Filter by name
            if (string.IsNullOrEmpty(nameFilter) == false)
            {
                Item? filteredItem = FindItem(items, nameFilter);
                if (filteredItem == null)
                {
                    throw new Exception(nameFilter + " item not found");
                }
                else
                {
                    List<Item> filteredItems = new();
                    //Add the root - this is the final item
                    filteredItems.Add(filteredItem);

                    //Get all of the inputs leading up to it
                    filteredItems.AddRange(GetInputs(items, filteredItem.Recipes));

                    //Sort the items by level
                    filteredItems = filteredItems.OrderBy(b => b.Level).ToList();
                    items = filteredItems;
                }
            }

            ////If enabled, only show the direct inputs to product an item
            //if (showOnlyDirectDependencies == true)
            //{
            //    Dictionary<string, int> inputs = new();
            //    List<Item> filteredItems = new();
            //    foreach (Item? item in items)
            //    {
            //        if (item.ItemType != ItemType.Building)
            //        {
            //            //If the item is not a building, hide it's recipe
            //            item.Recipes = new List<Recipe>();
            //        }
            //        else
            //        {
            //            //If it is a building, log all of it's inputs
            //            foreach (Recipe? recipe in item.Recipes)
            //            {
            //                foreach (KeyValuePair<string, int> input in recipe.Inputs)
            //                {
            //                    if (inputs.ContainsKey(input.Key) == false)
            //                    {
            //                        inputs.Add(input.Key, 1);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    //Add each item to the filter.
            //    foreach (Item? item in items)
            //    {
            //        if (!filteredItems.Contains(item) &&
            //            (item.ItemType == ItemType.Building ||
            //            inputs.ContainsKey(item.Name)))
            //        {
            //            filteredItems.Add(item);
            //        }
            //    }

            //    items = filteredItems;
            //}

            //for (int i = 0; i < items.Count; i++)
            //{
            //    Item? item = items[i];
            //    if (item.Name == "Accumulator")
            //    {
            //        int j = i;
            //    }
            //}

            return items;
        }

        //Get recipe inputs
        private static List<Item> GetInputs(List<Item> items, List<Recipe> recipes)
        {
            List<Item> inputs = new();
            foreach (Recipe recipe in recipes)
            {
                foreach (KeyValuePair<string, decimal> item in recipe.Inputs)
                {
                    Item? inputItem = FindItem(items, item.Key);
                    if (inputItem != null && inputs.Contains(inputItem) == false)
                    {
                        inputs.Add(inputItem);
                        List<Item> newItems = GetInputs(items, inputItem.Recipes);
                        foreach (Item newItem in newItems)
                        {
                            if (newItem != null && inputs.Contains(newItem) == false)
                            {
                                inputs.Add(newItem);
                            }
                        }
                    }
                }
            }
            return inputs;
        }

        private static Item? FindItem(List<Item> items, string name)
        {
            return items.Where(i => i.Name == name).FirstOrDefault();
        }

    }
}
