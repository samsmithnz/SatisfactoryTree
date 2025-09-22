using Microsoft.JSInterop;
using System.Text.Json;

namespace SatisfactoryTree.Web.Services
{
    public interface ILocalStorageService
    {
        Task<T?> GetItemAsync<T>(string key) where T : class;
        Task SetItemAsync<T>(string key, T value) where T : class;
        Task RemoveItemAsync(string key);
        Task<bool> ContainsKeyAsync(string key);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly JsonSerializerOptions _jsonOptions;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        public async Task<T?> GetItemAsync<T>(string key) where T : class
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }

                return JsonSerializer.Deserialize<T>(json, _jsonOptions);
            }
            catch (Exception)
            {
                // If deserialization fails, return null
                return null;
            }
        }

        public async Task SetItemAsync<T>(string key, T value) where T : class
        {
            try
            {
                var json = JsonSerializer.Serialize(value, _jsonOptions);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
            }
            catch (Exception)
            {
                // Silently fail if storage is not available
            }
        }

        public async Task RemoveItemAsync(string key)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (Exception)
            {
                // Silently fail if storage is not available
            }
        }

        public async Task<bool> ContainsKeyAsync(string key)
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
                return !string.IsNullOrEmpty(json);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}