using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;
using SatisfactoryTree.Console.OldModels;   
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
        foreach (var item in results.items.parts)
        {
            System.Diagnostics.Debug.WriteLine(results.items.parts[item.Key].name);
        }
        Assert.AreEqual(168, results.items.parts.Count);
    }

    [TestMethod]
    public void PartsRawResourcesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        foreach (var item in results.items.rawResources)
        {
            System.Diagnostics.Debug.WriteLine(results.items.rawResources[item.Key].name);
        }
        Assert.AreEqual(24, results.items.rawResources.Count);
    }

    [TestMethod]
    public void RecipesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        bool alternateCoal2RecipeFound = false;
        foreach (var item in results.recipes)
        {
            //Sometimes the "_C" gets stripped off the ID, so we need to check that it remains
            if (item.id == "Alternate_Coal_2")
            {
                alternateCoal2RecipeFound = true;
                break;
            }
            //System.Diagnostics.Debug.WriteLine(item.DisplayName);
        }
        Assert.IsTrue(alternateCoal2RecipeFound);
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
        Assert.AreEqual(291+17, results.newRecipes.Count);
    }

}