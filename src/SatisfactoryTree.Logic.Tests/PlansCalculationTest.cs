using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System.Xml.Linq;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public sealed class PlansCalculationTest
    {
        private FactoryCatalog? factoryCatalog = null;

        [TestInitialize]
        public async Task Initialize()
        {
            //arrange

            //act
            factoryCatalog = await FactoryCatalogExtractor.LoadDataFromFile();

            //assert
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }
        }

        [TestMethod]
        public void TwoFactoriesInPlanCalculationTest()
        {
            //Arrange
            if (factoryCatalog == null)
            {
                Assert.Fail("Final data is null");
            }
            Plan plan = new();
            Factory screwsFactorySetup = new(1,"Screws factory");
            screwsFactorySetup.TargetParts.Add(new() { Name = "IronScrew", Quantity = 12 });
            plan.Factories.Add(screwsFactorySetup);
            Factory reinforcedPlatesFactorySetup = new(2,"Reinforced Iron Plates factory");
            reinforcedPlatesFactorySetup.TargetParts.Add(new() { Name = "IronPlateReinforced", Quantity = 1 });
            reinforcedPlatesFactorySetup.ImportedParts.Add(1, new() { Name = "IronScrew", Quantity = 12 });
            plan.Factories.Add(reinforcedPlatesFactorySetup);            

            //Act
            Calculator calculator = new();
            plan.UpdatePlanCalculations(factoryCatalog);

            //Assert
            Assert.IsNotNull(plan);
            Assert.AreEqual(2, plan.Factories.Count);

            Factory screwsFactory = plan.Factories[0];
            Assert.IsNotNull(screwsFactory);
            Assert.IsNotNull(screwsFactory.ComponentParts);
            List<Item> results2 = screwsFactory.ComponentParts;
            Assert.AreEqual(3, results2.Count);
            Assert.AreEqual("IronScrew", results2[0].Name);
            Assert.AreEqual(12, results2[0].Quantity);
            Assert.AreEqual(3, results2[0].Counter);
            Assert.AreEqual("IronRod", results2[1].Name);
            Assert.AreEqual(3, results2[1].Quantity);
            Assert.AreEqual(2, results2[1].Counter);
            Assert.AreEqual("IronIngot", results2[2].Name);
            Assert.AreEqual(3, results2[2].Quantity);
            Assert.AreEqual(1, results2[2].Counter);

            Factory reinforcedPlatesFactory = plan.Factories[1];
            Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
            List<Item> results = reinforcedPlatesFactory.ComponentParts;
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(3, results[0].Counter);
            Assert.AreEqual("IronPlate", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("IronIngot", results[2].Name);
            Assert.AreEqual(9, results[2].Quantity);
            Assert.AreEqual(1, results[2].Counter);
            Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey(1));
            Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts[1].Name);
            Assert.AreEqual(12, reinforcedPlatesFactory.ImportedParts[1].Quantity);

        }

        //[TestMethod]
        //public void TwoFactoriesInPlanNotEnoughImportsCalculationTest()
        //{
        //    //Arrange
        //    if (factoryCatalog == null)
        //    {
        //        Assert.Fail("Final data is null");
        //    }
        //    Plan plan = new();
        //    Factory screwsFactorySetup = new(1,"Screws factory");
        //    screwsFactorySetup.TargetParts.Add(new() { Name = "IronScrew", Quantity = 10 });
        //    plan.Factories.Add(screwsFactorySetup);
        //    Factory reinforcedPlatesFactorySetup = new(2,"Reinforced Iron Plates factory");
        //    reinforcedPlatesFactorySetup.TargetParts.Add(new() { Name = "IronPlateReinforced", Quantity = 1 });
        //    reinforcedPlatesFactorySetup.ImportedParts.Add(1,new() { Name = "IronScrew", Quantity = 12 });
        //    plan.Factories.Add(reinforcedPlatesFactorySetup);

        //    //Act
        //    Calculator calculator = new();
        //    plan.UpdatePlanCalculations(factoryCatalog);

        //    //Assert
        //    Assert.IsNotNull(plan);
        //    Assert.AreEqual(2, plan.Factories.Count);

        //    Factory screwsFactory = plan.Factories[0];
        //    Assert.IsNotNull(screwsFactory);
        //    Assert.IsNotNull(screwsFactory.ComponentParts);
        //    List<Item> results2 = screwsFactory.ComponentParts;
        //    Assert.AreEqual(4, results2.Count);
        //    Assert.AreEqual("IronScrew", results2[0].Name);
        //    Assert.AreEqual(12, results2[0].Quantity);
        //    Assert.AreEqual(4, results2[0].Counter);
        //    Assert.AreEqual("IronRod", results2[1].Name);
        //    Assert.AreEqual(3, results2[1].Quantity);
        //    Assert.AreEqual(3, results2[1].Counter);
        //    Assert.AreEqual("IronIngot", results2[2].Name);
        //    Assert.AreEqual(3, results2[2].Quantity);
        //    Assert.AreEqual(2, results2[2].Counter);
        //    Assert.AreEqual("OreIron", results2[3].Name);
        //    Assert.AreEqual(3, results2[3].Quantity);
        //    Assert.AreEqual(1, results2[3].Counter);

        //    Factory reinforcedPlatesFactory = plan.Factories[1];
        //    Assert.IsNotNull(reinforcedPlatesFactory.ComponentParts);
        //    List<Item> results = reinforcedPlatesFactory.ComponentParts;
        //    Assert.AreEqual(4, results.Count);
        //    Assert.AreEqual("IronPlateReinforced", results[0].Name);
        //    Assert.AreEqual(1, results[0].Quantity);
        //    Assert.AreEqual(4, results[0].Counter);
        //    Assert.AreEqual("IronPlate", results[1].Name);
        //    Assert.AreEqual(6, results[1].Quantity);
        //    Assert.AreEqual(3, results[1].Counter);
        //    Assert.AreEqual("IronIngot", results[2].Name);
        //    Assert.AreEqual(9, results[2].Quantity);
        //    Assert.AreEqual(2, results[2].Counter);
        //    Assert.AreEqual("OreIron", results[3].Name);
        //    Assert.AreEqual(9, results[3].Quantity);
        //    Assert.AreEqual(1, results[3].Counter);
        //    Assert.IsTrue(reinforcedPlatesFactory.ImportedParts.ContainsKey(1));
        //    Assert.AreEqual("IronScrew", reinforcedPlatesFactory.ImportedParts[1].Name);
        //    Assert.AreEqual(10, reinforcedPlatesFactory.ImportedParts[1].Quantity);

        //}

    }
}
