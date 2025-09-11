using System.Text.Json.Serialization;

namespace SatisfactoryTree.Logic.Extraction
{
    internal class RawNativeClass
    {
        [JsonPropertyName("NativeClass")]
        public string? NativeClassName { get; set; }

        [JsonPropertyName("Classes")]
        public List<RawItem>? Classes { get; set; }
    }
}
