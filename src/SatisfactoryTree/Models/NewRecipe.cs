namespace SatisfactoryTree.Models
{
    public class NewRecipe(string? className, string? displayName, KeyValuePair<string?, decimal>? ingredients, KeyValuePair<string?, decimal>? products, string? producedIn, decimal manufactoringDuration, bool isAlternateRecipe)
    {
        //Common properties
        public string? ClassName { get; set; } = className;
        public string? DisplayName { get; set; } = displayName;

        //Recipe properties
        public KeyValuePair<string?,decimal>? Ingredients { get; set; } = ingredients;
        public KeyValuePair<string?, decimal>? Products { get; set; } = products;
        public string? ProducedIn { get; set; } = producedIn;
        public decimal ManufactoringDuration { get; set; } = manufactoringDuration;
        public bool IsAlternateRecipe { get; set; } = isAlternateRecipe;
    }
}
