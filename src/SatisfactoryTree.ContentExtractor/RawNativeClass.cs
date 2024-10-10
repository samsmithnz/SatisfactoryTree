using System.Text.Json.Serialization;

namespace SatisfactoryTree.ContentExtractor
{
    internal class RawNativeClass
    {
        [JsonPropertyName("NativeClass")]
        public string? NativeClassName { get; set; }

        [JsonPropertyName("Classes")]
        public List<RawItem>? Classes { get; set; }
    }
}
