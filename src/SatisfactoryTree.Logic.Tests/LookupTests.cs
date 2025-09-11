using SatisfactoryTree.Logic.Calculations;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Logic.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class LookupTests
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
                Assert.Fail("Factory catalog is null");
            }
        }

        [TestMethod]
        public void GetPartsTest()
        {
            //Arrange

            //Act
            List<Part> parts = Lookups.GetParts(factoryCatalog);

            //Assert
            Assert.IsNotNull(parts);
            Assert.AreEqual(168, parts.Count);

            Assert.AreEqual("Adaptive Control Unit", parts[0].Name);
            Assert.AreEqual(0, parts[0].EnergyGeneratedInMJ);

            Assert.AreEqual("Battery", parts[14].Name);
            Assert.AreEqual(6000, parts[14].EnergyGeneratedInMJ);
        }

        [TestMethod]
        public void GetRecipesTest()
        {
            //Arrange
            string part = "ModularFrameHeavy";

            //Act
            List<Recipe> recipes = Lookups.GetRecipes(factoryCatalog, part);

            //Assert
            Assert.IsNotNull(recipes);
            Assert.AreEqual(3, recipes.Count);

            
            Assert.AreEqual("ModularFrameHeavy", recipes[0].Name);
            Assert.AreEqual("Heavy Modular Frame", recipes[0].DisplayName);
            Assert.AreEqual(false, recipes[0].IsAlternate);

            Assert.AreEqual("Alternate_HeavyFlexibleFrame", recipes[1].Name);
            Assert.AreEqual("Alternate: Heavy Flexible Frame", recipes[1].DisplayName);
            Assert.AreEqual(true, recipes[1].IsAlternate);

            Assert.AreEqual("Alternate_ModularFrameHeavy", recipes[2].Name);
            Assert.AreEqual("Alternate: Heavy Encased Frame", recipes[2].DisplayName);
            Assert.AreEqual( true, recipes[2].IsAlternate);


        }

    }
}
