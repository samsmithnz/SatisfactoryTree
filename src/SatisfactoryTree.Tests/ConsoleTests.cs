using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Console;
using SatisfactoryTree.Console.Interfaces;
using System.Threading.Tasks;

namespace SatisfactoryTree.Tests;

[TestClass]
public class RecipesTests
{

    private FinalData? results = null;

    [TestInitialize]
    public async Task Initialize()
    {
        //arrange
        Processor processor = new();
        processor.UpdateContent();
        string inputFile = processor.InputFile;
        string outputFile = processor.OutputFile;

        //act
        results = await Processor.ProcessFileAsync(inputFile, outputFile);
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
    public void RecipesCountTest()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.Recipes)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.DisplayName);
        //}
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
        Assert.AreEqual(12, results.buildings.Count);
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

}