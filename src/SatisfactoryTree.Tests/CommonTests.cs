using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;

namespace SatisfactoryTree.Tests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void Blacklist_ContainsExpectedItems()
        {
            // Assert
            Assert.IsNotNull(Common.Blacklist);
            Assert.AreEqual(3, Common.Blacklist.Count);
            Assert.IsTrue(Common.Blacklist.Contains("(\"/Game/FactoryGame/Equipment/BuildGun/BP_BuildGun.BP_BuildGun_C\")"));
            Assert.IsTrue(Common.Blacklist.Contains("(\"/Script/FactoryGame.FGBuildGun\")"));
            Assert.IsTrue(Common.Blacklist.Contains("(\"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C\")"));
        }

        [TestMethod]
        public void Whitelist_ContainsExpectedItems()
        {
            // Assert
            Assert.IsNotNull(Common.Whitelist);
            Assert.IsTrue(Common.Whitelist.Count > 10);
            Assert.IsTrue(Common.Whitelist.Contains("Desc_NuclearWaste_C"));
            Assert.IsTrue(Common.Whitelist.Contains("Desc_PlutoniumWaste_C"));
            Assert.IsTrue(Common.Whitelist.Contains("Desc_Leaves_C"));
            Assert.IsTrue(Common.Whitelist.Contains("Desc_SAM_C"));
        }

        [TestMethod]
        public void IsFluid_WithLiquidProducts_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(Common.IsFluid("water"));
            Assert.IsTrue(Common.IsFluid("liquidoil"));
            Assert.IsTrue(Common.IsFluid("heavyoilresidue"));
            Assert.IsTrue(Common.IsFluid("liquidfuel"));
            Assert.IsTrue(Common.IsFluid("aluminasolution"));
            Assert.IsTrue(Common.IsFluid("sulfuricacid"));
        }

        [TestMethod]
        public void IsFluid_WithGasProducts_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(Common.IsFluid("nitrogengas"));
            Assert.IsTrue(Common.IsFluid("rocketfuel"));
            Assert.IsTrue(Common.IsFluid("ionizedfuel"));
            Assert.IsTrue(Common.IsFluid("quantumenergy"));
        }

        [TestMethod]
        public void IsFluid_WithCaseVariations_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(Common.IsFluid("WATER"));
            Assert.IsTrue(Common.IsFluid("Water"));
            Assert.IsTrue(Common.IsFluid("LiquidOil"));
            Assert.IsTrue(Common.IsFluid("NITROGENGAS"));
        }

        [TestMethod]
        public void IsFluid_WithSolidProducts_ReturnsFalse()
        {
            // Assert
            Assert.IsFalse(Common.IsFluid("ironore"));
            Assert.IsFalse(Common.IsFluid("ironingot"));
            Assert.IsFalse(Common.IsFluid("ironplate"));
            Assert.IsFalse(Common.IsFluid("concrete"));
        }

        [TestMethod]
        public void IsFicsmas_WithFicsmasItems_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(Common.IsFicsmas("FICSMAS Tree"));
            Assert.IsTrue(Common.IsFicsmas("FICSMAS Gift"));
            Assert.IsTrue(Common.IsFicsmas("Gift Box"));
            Assert.IsTrue(Common.IsFicsmas("Snow Ball"));
            Assert.IsTrue(Common.IsFicsmas("Candy Cane"));
            Assert.IsTrue(Common.IsFicsmas("Fireworks"));
        }

        [TestMethod]
        public void IsFicsmas_WithNormalItems_ReturnsFalse()
        {
            // Assert
            Assert.IsFalse(Common.IsFicsmas("Iron Ore"));
            Assert.IsFalse(Common.IsFicsmas("Iron Ingot"));
            Assert.IsFalse(Common.IsFicsmas("Concrete"));
        }

        [TestMethod]
        public void IsFicsmas_WithNull_ReturnsFalse()
        {
            // Assert
            Assert.IsFalse(Common.IsFicsmas(null));
        }

        [TestMethod]
        public void GetRecipeName_RemovesExpectedSuffixes()
        {
            // Assert
            Assert.AreEqual("IronIngot", Common.GetRecipeName("Recipe_IronIngot_C"));
            Assert.AreEqual("IronIngot", Common.GetRecipeName("IronIngot_C"));
            Assert.AreEqual("IronIngot", Common.GetRecipeName("Recipe_IronIngot"));
        }

        [TestMethod]
        public void GetPowerGenerationRecipeName_RemovesExpectedPrefixesAndSuffixes()
        {
            // Assert
            Assert.AreEqual("GeneratorBiomass", Common.GetPowerGenerationRecipeName("Build_GeneratorBiomass_C"));
            Assert.AreEqual("GeneratorCoal", Common.GetPowerGenerationRecipeName("Build_GeneratorCoal_C"));
        }

        [TestMethod]
        public void GetBuildingName_RemovesExpectedPrefixesAndSuffixes()
        {
            // Assert
            Assert.AreEqual("ConstructorMk1", Common.GetBuildingName("Build_ConstructorMk1_C"));
            Assert.AreEqual("AssemblerMk1", Common.GetBuildingName("Build_AssemblerMk1_C"));
        }

        [TestMethod]
        public void GetPowerBuildingName_ExtractsCorrectName()
        {
            // Assert
            Assert.AreEqual("generatorbiomass", Common.GetPowerBuildingName("Build_GeneratorBiomass_Automated_C"));
            Assert.AreEqual("generatorcoal", Common.GetPowerBuildingName("Build_GeneratorCoal_C"));
        }

        [TestMethod]
        public void GetPowerBuildingName_WithInvalidFormat_ReturnsEmpty()
        {
            // Assert
            Assert.AreEqual("", Common.GetPowerBuildingName("InvalidFormat"));
            Assert.AreEqual("", Common.GetPowerBuildingName("Build_"));
        }

        [TestMethod]
        public void GetPartName_RemovesExpectedPrefixesAndSuffixes()
        {
            // Assert
            Assert.AreEqual("IronOre", Common.GetPartName("Desc_IronOre_C"));
            Assert.AreEqual("IronIngot", Common.GetPartName("Desc_IronIngot_C"));
        }

        [TestMethod]
        public void GetPartName_WithPortableMiner_ReturnsSpecialCase()
        {
            // Assert
            Assert.AreEqual("PortableMiner", Common.GetPartName("BP_ItemDescriptorPortableMiner_C"));
        }

        [TestMethod]
        public void GetFriendlyName_RemovesBracketContent()
        {
            // Assert
            Assert.AreEqual("Iron Ingot", Common.GetFriendlyName("Iron Ingot (Smelted)"));
            Assert.AreEqual("Copper Wire", Common.GetFriendlyName("Copper Wire (Alternative)"));
            Assert.AreEqual("Basic Item", Common.GetFriendlyName("Basic Item"));
        }

        [TestMethod]
        public void GetFriendlyName_WithNull_ReturnsEmpty()
        {
            // Assert
            Assert.AreEqual("", Common.GetFriendlyName(null));
        }

        [TestMethod]
        public void GetPowerProducerBuildingName_ExtractsCorrectName()
        {
            // Assert
            Assert.AreEqual("generatorbiomass", Common.GetPowerProducerBuildingName("Build_GeneratorBiomass_Automated_C"));
            Assert.AreEqual("generatorcoal", Common.GetPowerProducerBuildingName("Build_GeneratorCoal_C"));
        }

        [TestMethod]
        public void GetPowerProducerBuildingName_WithInvalidFormat_ReturnsNull()
        {
            // Assert
            Assert.IsNull(Common.GetPowerProducerBuildingName("InvalidFormat"));
            Assert.IsNull(Common.GetPowerProducerBuildingName("Build_"));
        }
    }
}