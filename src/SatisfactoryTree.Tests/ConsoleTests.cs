using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryTree.Tests;

[TestClass]
public class ConsoleTests
{

    private Console.OldModels.FinalData? results = null;

    [TestInitialize]
    public async Task Initialize()
    {
        //arrange
        Processor processor = new();
        processor.GetContentFiles();
        if (processor != null)
        {
            string inputFile = processor.InputFile;
            string outputFile = processor.OutputFile;

            //act
            results = await Processor.ProcessFileOldModel(inputFile, outputFile);
        }
    }

    [TestMethod]
    public void PartsCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.items.parts)
        //{
        //    System.Diagnostics.Debug.WriteLine(results.items.parts[item.Key].name);
        //}
        Assert.AreEqual(168, results.items.parts.Count);
    }

    [TestMethod]
    public void PartsRawResourcesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.items.rawResources)
        //{
        //    System.Diagnostics.Debug.WriteLine(results.items.rawResources[item.Key].name);
        //}
        Assert.AreEqual(24, results.items.rawResources.Count);
    }

    [TestMethod]
    public void RecipesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //bool alternateCoal2RecipeFound = false;
        //foreach (var item in results.recipes)
        //{
        //    //Sometimes the "_C" gets stripped off the ID, so we need to check that it remains
        //    if (item.id == "Alternate_Coal_2")
        //    {
        //        alternateCoal2RecipeFound = true;
        //        break;
        //    }
        //    //System.Diagnostics.Debug.WriteLine(item.DisplayName);
        //}
        //Assert.IsTrue(alternateCoal2RecipeFound);
        Assert.AreEqual(291, results.recipes.Count);
    }

    [TestMethod]
    public void BuildingsCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.Recipes)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.DisplayName);
        //}
        Assert.AreEqual(15, results.buildings.Count);
    }

    [TestMethod]
    public void PowerGeneratingBuildingsCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.Recipes)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.DisplayName);
        //}
        Assert.AreEqual(17, results.powerGenerationRecipes.Count);
    }
    [TestMethod]
    public void NewRecipesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.Recipes)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.DisplayName);
        //}
        Assert.AreEqual(results.recipes.Count + results.powerGenerationRecipes.Count, results.newRecipes.Count);
    }

    [TestMethod]
    public void SAMOreDetectionTest()
    {
        //Arrange
        
        //Act
        
        //Assert
        Assert.IsNotNull(results);
        
        // Count recipes that use SAM ore
        int samRecipeCount = 0;
        int nonSamRecipeCount = 0;
        
        foreach (var recipe in results.newRecipes)
        {
            bool hasSAMIngot = recipe.ingredients.Any(ingredient => ingredient.part == "SAMIngot");
            
            if (hasSAMIngot)
            {
                samRecipeCount++;
                Assert.IsTrue(recipe.usesSAMOre, $"Recipe '{recipe.displayName}' has SAMIngot ingredient but usesSAMOre is false");
            }
            else
            {
                nonSamRecipeCount++;
                Assert.IsFalse(recipe.usesSAMOre, $"Recipe '{recipe.displayName}' has no SAMIngot ingredient but usesSAMOre is true");
            }
        }
        
        // Verify we found some SAM recipes (based on the data we saw earlier, there should be 22)
        Assert.IsTrue(samRecipeCount > 0, "No recipes with SAMIngot were found");
        Assert.IsTrue(nonSamRecipeCount > 0, "No recipes without SAMIngot were found");
        
        System.Diagnostics.Debug.WriteLine($"Found {samRecipeCount} recipes that use SAM ore");
        System.Diagnostics.Debug.WriteLine($"Found {nonSamRecipeCount} recipes that do not use SAM ore");
    }

    [TestMethod]
    public void SAMOreSpecificRecipeTest()
    {
        //Arrange
        
        //Act
        
        //Assert
        Assert.IsNotNull(results);
        
        // Find a specific recipe that should use SAM ore (based on our earlier analysis)
        var darkMatterRecipe = results.newRecipes.FirstOrDefault(r => r.displayName == "Dark Matter Residue");
        Assert.IsNotNull(darkMatterRecipe, "Dark Matter Residue recipe not found");
        Assert.IsTrue(darkMatterRecipe.usesSAMOre, "Dark Matter Residue recipe should use SAM ore");
        
        // Find a recipe that should NOT use SAM ore
        var ironPlateRecipe = results.newRecipes.FirstOrDefault(r => r.displayName == "Iron Plate");
        Assert.IsNotNull(ironPlateRecipe, "Iron Plate recipe not found");
        Assert.IsFalse(ironPlateRecipe.usesSAMOre, "Iron Plate recipe should NOT use SAM ore");
    }

}