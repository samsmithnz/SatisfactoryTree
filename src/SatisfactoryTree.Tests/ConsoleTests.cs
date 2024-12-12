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
        //foreach (var item in results.Items.Parts)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.Key);
        //}
        Assert.AreEqual(179, results.Items.Parts.Count);
    }

    [TestMethod]
    public void RecipesCountTest()
    {
        //Arrange


        //Act

        //Assert
        Assert.IsNotNull(results);
        //foreach (var item in results.Items2.Parts)
        //{
        //    System.Diagnostics.Debug.WriteLine(item.Key);
        //}
        Assert.AreEqual(314, results.Recipes.Count);
    }

}