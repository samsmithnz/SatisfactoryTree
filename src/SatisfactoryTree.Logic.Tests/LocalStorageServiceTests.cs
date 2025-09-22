using Microsoft.JSInterop;
using Moq;
using SatisfactoryTree.Logic.Models;
using SatisfactoryTree.Web.Services;

namespace SatisfactoryTree.Logic.Tests
{
    [TestClass]
    public class LocalStorageServiceTests
    {
        private Mock<IJSRuntime> _mockJSRuntime = null!;
        private LocalStorageService _localStorageService = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockJSRuntime = new Mock<IJSRuntime>();
            _localStorageService = new LocalStorageService(_mockJSRuntime.Object);
        }

        [TestMethod]
        public async Task GetItemAsync_WhenItemExists_ReturnsDeserializedObject()
        {
            // Arrange
            var testPlan = new Plan();
            testPlan.Factories.Add(new Factory(1, "Test Factory"));
            var expectedJson = System.Text.Json.JsonSerializer.Serialize(testPlan, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            
            _mockJSRuntime.Setup(js => js.InvokeAsync<string?>(
                "localStorage.getItem",
                It.IsAny<object[]>()))
                .ReturnsAsync(expectedJson);

            // Act
            var result = await _localStorageService.GetItemAsync<Plan>("test-key");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Factories.Count);
            Assert.AreEqual("Test Factory", result.Factories[0].Name);
        }

        [TestMethod]
        public async Task GetItemAsync_WhenItemDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockJSRuntime.Setup(js => js.InvokeAsync<string?>(
                "localStorage.getItem",
                It.IsAny<object[]>()))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _localStorageService.GetItemAsync<Plan>("test-key");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task SetItemAsync_CallsJSRuntimeWithCorrectParameters()
        {
            // Arrange
            var testPlan = new Plan();
            testPlan.Factories.Add(new Factory(1, "Test Factory"));

            // Act
            await _localStorageService.SetItemAsync("test-key", testPlan);

            // Assert
            _mockJSRuntime.Verify(js => js.InvokeAsync<object>(
                "localStorage.setItem",
                It.Is<object[]>(args => 
                    args.Length == 2 && 
                    args[0].ToString() == "test-key" && 
                    args[1].ToString()!.Contains("Test Factory"))), 
                Times.Once);
        }

        [TestMethod]
        public async Task ContainsKeyAsync_WhenItemExists_ReturnsTrue()
        {
            // Arrange
            _mockJSRuntime.Setup(js => js.InvokeAsync<string?>(
                "localStorage.getItem",
                It.IsAny<object[]>()))
                .ReturnsAsync("some-value");

            // Act
            var result = await _localStorageService.ContainsKeyAsync("test-key");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ContainsKeyAsync_WhenItemDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockJSRuntime.Setup(js => js.InvokeAsync<string?>(
                "localStorage.getItem",
                It.IsAny<object[]>()))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _localStorageService.ContainsKeyAsync("test-key");

            // Assert
            Assert.IsFalse(result);
        }
    }
}