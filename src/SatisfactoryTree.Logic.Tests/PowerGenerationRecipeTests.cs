using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Logic.Extraction;
using SatisfactoryTree.Logic.Models;
using System.Text.Json;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public class PowerGenerationRecipeTests
    {
        [TestMethod]
        public void PowerGenerationRecipe_SupplementaryIngredients_ShouldHaveMwPerItem()
        {
            // Arrange
            var parts = new RawPartsAndRawMaterials
            {
                Parts = new Dictionary<string, Part>
                {
                    ["Coal"] = new Part { Name = "Coal", EnergyGeneratedInMJ = 300 },
                    ["Water"] = new Part { Name = "Water", EnergyGeneratedInMJ = 0 }
                },
                RawResources = new Dictionary<string, RawResource>()
            };

            var buildings = new Dictionary<string, double>
            {
                ["GeneratorCoal"] = 75.0  // 75 MW power generation
            };

            // Create mock JSON data for a coal generator
            var jsonData = JsonSerializer.Deserialize<List<JsonElement>>(@"
            [
                {
                    ""ClassName"": ""Build_GeneratorCoal_C"",
                    ""mDisplayName"": ""Coal-Powered Generator"",
                    ""mPowerProduction"": ""75"",
                    ""mSupplementalToPowerRatio"": ""1"",
                    ""mFuel"": [
                        {
                            ""mFuelClass"": ""Desc_Coal_C"",
                            ""mSupplementalResourceClass"": ""Desc_Water_C"",
                            ""mByproduct"": """",
                            ""mByproductAmount"": ""0""
                        }
                    ]
                }
            ]")!;

            // Act
            var powerRecipes = ProcessRawRecipes.GetPowerGeneratingRecipes(jsonData, parts, buildings);

            // Assert
            Assert.IsTrue(powerRecipes.Count > 0, "Should generate at least one power recipe");
            
            var coalRecipe = powerRecipes.FirstOrDefault(r => r.displayName.Contains("Coal"));
            Assert.IsNotNull(coalRecipe, "Should have a coal-powered generator recipe");
            
            Assert.AreEqual(2, coalRecipe.ingredients.Count, "Coal generator should have 2 ingredients (coal + water)");
            
            // Check primary ingredient (coal)
            var primaryIngredient = coalRecipe.ingredients[0];
            Assert.AreEqual("Coal", primaryIngredient.part, "First ingredient should be Coal");
            Assert.IsTrue(primaryIngredient.mwPerItem.HasValue, "Primary ingredient should have mwPerItem");
            Assert.IsTrue(primaryIngredient.mwPerItem.Value > 0, "Primary ingredient mwPerItem should be positive");
            
            // Check supplementary ingredient (water) - this is the fix we're testing
            var supplementaryIngredient = coalRecipe.ingredients[1];
            Assert.AreEqual("Water", supplementaryIngredient.part, "Second ingredient should be Water");
            Assert.IsTrue(supplementaryIngredient.mwPerItem.HasValue, "Supplementary ingredient should have mwPerItem (THIS WAS THE BUG)");
            Assert.IsTrue(supplementaryIngredient.mwPerItem.Value > 0, "Supplementary ingredient mwPerItem should be positive");
            Assert.IsTrue(supplementaryIngredient.supplementalRatio.HasValue, "Supplementary ingredient should have supplementalRatio");
        }
    }
}