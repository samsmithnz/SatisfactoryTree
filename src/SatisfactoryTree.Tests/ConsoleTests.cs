using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Console;
using System.Collections.Generic;
using System.Threading.Tasks;
using SatisfactoryTree.Console.Interfaces;

namespace SatisfactoryTree.Tests;

[TestClass]
public class RecipesTests
{

    private FinalData results;

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
    public void RecipeCountTest()
    {
        //Arrange
        

        //Act

        //Assert
        Assert.AreEqual(1, results.Items.Parts.Count);
    }

}