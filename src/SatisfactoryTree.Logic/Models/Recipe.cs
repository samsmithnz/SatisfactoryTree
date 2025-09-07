namespace SatisfactoryTree.Logic.Models
{
    public class Recipe(string? className,
        string? displayName,
        List<KeyValuePair<string?, decimal>>? ingredients,
        List<KeyValuePair<string?, decimal>>? products,
        string? producedIn,
        decimal manufactoringDuration,
        bool isAlternateRecipe)
    {
        //Common properties
        public string? ClassName { get; set; } = className;
        public string? DisplayName { get; set; } = displayName;

        //Recipe properties
        public List<KeyValuePair<string?, decimal>>? Ingredients { get; set; } = ingredients;
        public List<KeyValuePair<string?, decimal>>? Products { get; set; } = products;
        public string? ProducedIn { get; set; } = producedIn;
        public decimal ManufactoringDuration { get; set; } = manufactoringDuration;
        public bool IsAlternateRecipe { get; set; } = isAlternateRecipe;
    }
}
