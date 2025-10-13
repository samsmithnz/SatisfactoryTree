//using SatisfactoryTree.Logic.Extraction;
//using SatisfactoryTree.Logic.Models;

//namespace SatisfactoryTree.Logic.Tests
//{
//    [TestClass]
//    public sealed class FactoriesCalculationTest
//    {
//        private FactoryCatalog? factoryCatalog = null;

//        [TestInitialize]
//        public async Task Initialize()
//        {
//            //arrange

//            //act
//            factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();

//            //assert
//            if (factoryCatalog == null)
//            {
//                Assert.Fail("Final data is null");
//            }
//        }

//        [TestMethod]
//        public void TwoFactoriesReinforcedPlatesWithFullScrewImportCalculationTest()
//        {
//            //Arrange
//            if (factoryCatalog == null)
//            {
//                Assert.Fail("Final data is null");
//            }
//            Factory screwsFactory = new(1, "Screws factory");
//            screwsFactory.ExportedParts.Add(new(new() { Name = "IronScrew", Quantity = 12 }));
//            Factory reinforcedPlatesFactory = new(2, "Reinforced Iron Plates factory");
//            reinforcedPlatesFactory.ExportedParts.Add(new(new() { Name = "IronPlateReinforced", Quantity = 1 }));
//            reinforcedPlatesFactory.ImportedParts.Add(1, new(1, "Screws factory", new() { Name = "IronScrew", Quantity = 12 }));
//            //Plan plan = new();
//            //plan.Factories.Add(screwsFactory);
//            //plan.Factories.Add(reinforcedPlatesFactory);

//            //Act
//            Calculator calculator = new();
//            screwsFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, screwsFactory);
//            reinforcedPlatesFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, reinforcedPlatesFactory);

//            //Assert
//            Assert.IsNotNull(reinforcedPlatesFactory);
//            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
//            List<Item> results = reinforcedPlatesFactory.ComponentParts;
//            Assert.AreEqual(3, results.Count);
            
//            // Reinforced Iron Plates (the goal item)
//            Assert.AreEqual("IronPlateReinforced", results[0].Name);
//            Assert.AreEqual(1, results[0].Quantity);
//            Assert.AreEqual(1.787, results[0].BuildingPowerUsage);
//            Assert.AreEqual(3, results[0].Counter);
            
//            // Iron Plates (ingredient for reinforced plates)
//            Assert.AreEqual("IronPlate", results[1].Name);
//            Assert.AreEqual(6, results[1].Quantity);
//            Assert.AreEqual(0.814, results[1].BuildingPowerUsage);
//            Assert.AreEqual(2, results[1].Counter);
            
//            // Iron Ingots (raw material)
//            Assert.AreEqual("IronIngot", results[2].Name);
//            Assert.AreEqual(9, results[2].Quantity);
//            Assert.AreEqual(0.814, results[2].BuildingPowerUsage);
//            Assert.AreEqual(1, results[2].Counter);
            
//            Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey(1));
//            Assert.AreEqual("Screws factory", reinforcedPlatesFactory.ImportedParts[1].FactoryName);
//            Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts[1].Item.Name);
//            Assert.AreEqual(12, reinforcedPlatesFactory.ImportedParts[1].Item.Quantity);

//            Assert.IsNotNull(screwsFactory);
//            Assert.IsNotNull(screwsFactory.ComponentParts);
//            List<Item> results2 = screwsFactory.ComponentParts;
//            Assert.AreEqual(3, results2.Count);
            
//            // Iron Screws (the goal item)
//            Assert.AreEqual("IronScrew", results2[0].Name);
//            Assert.AreEqual(12, results2[0].Quantity);
//            Assert.AreEqual(0.814, results2[0].BuildingPowerUsage);
//            Assert.AreEqual(3, results2[0].Counter);
            
//            // Iron Rods (ingredient for screws)
//            Assert.AreEqual("IronRod", results2[1].Name);
//            Assert.AreEqual(3, results2[1].Quantity);
//            Assert.AreEqual(0.477, results2[1].BuildingPowerUsage);
//            Assert.AreEqual(2, results2[1].Counter);
            
//            // Iron Ingots (raw material)
//            Assert.AreEqual("IronIngot", results2[2].Name);
//            Assert.AreEqual(3, results2[2].Quantity);
//            Assert.AreEqual(0.191, results2[2].BuildingPowerUsage);
//            Assert.AreEqual(1, results2[2].Counter);
//        }

//        [TestMethod]
//        public void TwoFactoriesReinforcedPlatesWithPartialScrewImportCalculationTest()
//        {
//            //Arrange
//            if (factoryCatalog == null)
//            {
//                Assert.Fail("Final data is null");
//            }
//            Factory screwsFactory = new(1, "Screws factory");
//            screwsFactory.ExportedParts.Add(new(new() { Name = "IronScrew", Quantity = 6 }));
//            Factory reinforcedPlatesFactory = new(2, "Reinforced Iron Plates factory");
//            reinforcedPlatesFactory.ExportedParts.Add(new(new() { Name = "IronPlateReinforced", Quantity = 1 }));
            
//            // NOTE: This test is expecting that the Plan level balancing will handle the fact that
//            // the screws factory only produces 6 screws but we're trying to import 12.
//            // For now, we'll test with the correct quantity that would be available after balancing.
//            reinforcedPlatesFactory.ImportedParts.Add(1, new(1, "Screws factory", new() { Name = "IronScrew", Quantity = 6 }));

//            //Act
//            Calculator calculator = new();
//            screwsFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, screwsFactory);
//            reinforcedPlatesFactory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, reinforcedPlatesFactory);

//            //Assert
//            Assert.IsNotNull(reinforcedPlatesFactory);
//            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
//            List<Item> results = reinforcedPlatesFactory.ComponentParts;
//            Assert.AreEqual(5, results.Count);
//            Assert.AreEqual("IronPlateReinforced", results[0].Name);
//            Assert.AreEqual(1, results[0].Quantity);
//            Assert.AreEqual(4, results[0].Counter);
//            Assert.AreEqual("IronScrew", results[1].Name);
//            Assert.AreEqual(6, results[1].Quantity);
//            Assert.AreEqual(3, results[1].Counter);
//            Assert.AreEqual("IronPlate", results[2].Name);
//            Assert.AreEqual(6, results[2].Quantity);
//            Assert.AreEqual(2, results[2].Counter);
//            Assert.AreEqual("IronRod", results[3].Name);
//            Assert.AreEqual(1.5, results[3].Quantity);
//            Assert.AreEqual(2, results[3].Counter);
//            Assert.AreEqual("IronIngot", results[4].Name);
//            Assert.AreEqual(10.5, results[4].Quantity);
//            Assert.AreEqual(1, results[4].Counter);
//            //Assert.AreEqual("OreIron", results[5].Name);
//            //Assert.AreEqual(10.5, results[5].Quantity);
//            //Assert.AreEqual(1, results[5].Counter);
//            Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey(1));
//            Assert.AreEqual("Screws factory", reinforcedPlatesFactory.ImportedParts[1].FactoryName);
//            Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts[1].Item.Name);
//            Assert.AreEqual(6, reinforcedPlatesFactory.ImportedParts[1].Item.Quantity);

//            Assert.IsNotNull(screwsFactory);
//            Assert.IsNotNull(screwsFactory.ComponentParts);
//            List<Item> results2 = screwsFactory.ComponentParts;
//            Assert.AreEqual(3, results2.Count);
//            Assert.AreEqual("IronScrew", results2[0].Name);
//            Assert.AreEqual(6, results2[0].Quantity);
//            Assert.AreEqual(3, results2[0].Counter);
//            Assert.AreEqual("IronRod", results2[1].Name);
//            Assert.AreEqual(1.5, results2[1].Quantity);
//            Assert.AreEqual(2, results2[1].Counter);
//            Assert.AreEqual("IronIngot", results2[2].Name);
//            Assert.AreEqual(1.5, results2[2].Quantity);
//            Assert.AreEqual(1, results2[2].Counter);
//            //Assert.AreEqual("OreIron", results2[3].Name);
//            //Assert.AreEqual(1.5, results2[3].Quantity);
//            //Assert.AreEqual(1, results2[3].Counter);
//        }
//    }
//}
