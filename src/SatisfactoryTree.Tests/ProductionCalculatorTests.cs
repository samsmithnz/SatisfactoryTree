using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SatisfactoryTree.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryTree.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    [TestClass]
    public class ProductionCalculatorTests
    {
        private SatisfactoryTree.Logic.Extraction.ExtractionModels.FinalData? finalData = null;
        private ProductionCalculator _calculator;

        [TestInitialize]
        public async Task Initialize()
        {
            //arrange
            var json = await File.ReadAllTextAsync(Path.Combine("content", "gameData.json"));
            finalData = JsonSerializer.Deserialize<SatisfactoryTree.Logic.Extraction.ExtractionModels.FinalData>(json);

            //Substitute.GetFromJsonAsync
            //finalData = JsonSerializer.Deserialize<SatisfactoryTree.Logic.Extraction.ExtractionModels.FinalData>(json);

            ////GetFromJsonAsync
            //var httpMessageHandler = Substitute.ForPartsOf<HttpMessageHandler>();
            //httpMessageHandler
            //    .When(x => x.SendAsync(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()))
            //    .DoNotCallBase()
            //    .Returns(Task.FromResult(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.OK,
            //        Content = new StringContent(json)
            //    }));

            //var httpClientFactory = Substitute.For<IHttpClientFactory>();
            //httpClientFactory.CreateClient(Arg.Any<string>()).Returns(new HttpClient(httpMessageHandler)
            //{
            //    BaseAddress = new Uri("http://localhost")
            //});

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json)
            };

            var mockHttpMessageHandler = new MockHttpMessageHandler(responseMessage);
            var httpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            _calculator = new ProductionCalculator(httpClient);
            _calculator._finalData = finalData;

            //var productionCalculator = new ProductionCalculator(httpClientFactory.CreateClient(""));


            DataFileExtraction processor = new();
            processor.GetContentFiles();
            if (processor != null)
            {
                string inputFile = processor.InputFile;
                string outputFile = processor.OutputFile;

                //act
                finalData = await DataFileExtraction.ExtractDataFile(inputFile, outputFile);

                //assert
                if (finalData == null)
                {
                    Assert.Fail("Final data is null");
                }
            }
        }

        [TestMethod]
        public void IronIngotsCalculationTest()
        {
            //Arrange
            string partName = "IronIngot";
            double quantity = 30;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            List<Item>? results = _calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);

            Assert.AreEqual("IronIngot", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("smeltermk1", results[0].Building);
            Assert.AreEqual(1, results[0].BuildingQuantity);

            Assert.AreEqual("OreIron", results[1].Name);
            Assert.AreEqual(30, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual(null, results[1].Building);
            Assert.AreEqual(0, results[1].BuildingQuantity);
        }

        [TestMethod]
        public void IronPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlate";
            double quantity = 30;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            List<Item>? results = _calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("IronPlate", results[0].Name);
            Assert.AreEqual(30, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("constructormk1", results[0].Building);
            Assert.AreEqual(1.5, results[0].BuildingQuantity);

            Assert.AreEqual("IronIngot", results[1].Name);
            Assert.AreEqual(45, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("smeltermk1", results[1].Building);
            Assert.AreEqual(1.5, results[1].BuildingQuantity);

            Assert.AreEqual("OreIron", results[2].Name);
            Assert.AreEqual(45, results[2].Quantity);
            Assert.AreEqual(3, results[2].Counter);
            Assert.AreEqual(null, results[2].Building);
            Assert.AreEqual(0, results[2].BuildingQuantity);
        }

        [TestMethod]
        public void ReinforcedPlatesCalculationTest()
        {
            //Arrange
            string partName = "IronPlateReinforced";
            double quantity = 1;
            if (finalData == null)
            {
                Assert.Fail("Final data is null");
            }

            //Act
            List<Item>? results = _calculator.CalculateProduction(finalData, partName, quantity);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("IronPlateReinforced", results[0].Name);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(1, results[0].Counter);
            Assert.AreEqual("IronPlate", results[1].Name);
            Assert.AreEqual(6, results[1].Quantity);
            Assert.AreEqual(2, results[1].Counter);
            Assert.AreEqual("IronScrew", results[2].Name);
            Assert.AreEqual(12, results[2].Quantity);
            Assert.AreEqual(2, results[2].Counter);
            Assert.AreEqual("IronIngot", results[3].Name);
            Assert.AreEqual(12, results[3].Quantity);
            Assert.AreEqual(3, results[3].Counter);
            Assert.AreEqual("IronRod", results[4].Name);
            Assert.AreEqual(3, results[4].Quantity);
            Assert.AreEqual(3, results[4].Counter);
            Assert.AreEqual("OreIron", results[5].Name);
            Assert.AreEqual(12, results[5].Quantity);
            Assert.AreEqual(4, results[5].Counter);
        }
    }
}
