namespace SatisfactoryTree.Models
{
    public class NewRecipe(string? className, string? displayName, string? ingredients, string? products, string? producedIn, string? manufactoringDuration, bool isAlternateRecipe)
    {
        //Common properties
        public string? ClassName { get; set; } = className;
        public string? DisplayName { get; set; } = displayName;

        //Recipe properties
        public string? Ingredients { get; set; } = ingredients;
        public string? Products { get; set; } = products;
        public string? ProducedIn { get; set; } = producedIn;
        public string? ManufactoringDuration { get; set; } = manufactoringDuration;
        public bool IsAlternateRecipe { get; set; } = isAlternateRecipe;
    }
}
