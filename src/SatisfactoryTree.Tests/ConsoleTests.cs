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
        foreach (var item in results.Items.Parts)
        {
            System.Diagnostics.Debug.WriteLine(results.Items.Parts[item.Key].Name);
        }
        Assert.AreEqual(168, results.Items.Parts.Count);
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
        Assert.AreEqual(291, results.Recipes.Count);
    }

}