using System.Text.Json.Serialization;

namespace SatisfactoryTree.ContentExtractor
{
    public class RawItem
    { 
        //Common properties
        [JsonPropertyName("ClassName")]
        public string? ClassName { get; set; }
        [JsonPropertyName("mDisplayName")]
        public string? DisplayName { get; set; }
        [JsonPropertyName("mDescription")]
        public string? Description { get; set; }

        //Item properties
        [JsonPropertyName("mStackSize")]
        public string? StackSize { get; set; }
        [JsonPropertyName("mFluidColor")]
        public string? FluidColor { get; set; }
        [JsonPropertyName("mResourceSinkPoints")]
        public string? ResourceSinkPoints { get; set; }

        //Recipe properties
        [JsonPropertyName("mIngredients")]
        public string? Ingredients { get; set; }
        [JsonPropertyName("mProduct")]
        public string? Products { get; set; } // Not a typo - there can be multiple products produced
        [JsonPropertyName("mProducedIn")]
        public string? ProducedIn { get; set; }
        [JsonPropertyName("mManufactoringDuration")]
        public string? ManufactoringDuration { get; set; }
        public bool IsAlternateRecipe
        {
            get
            {
                if (ClassName != null && ClassName.StartsWith("Recipe_Alternate_"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
