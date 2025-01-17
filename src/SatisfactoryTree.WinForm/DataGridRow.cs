namespace SatisfactoryTree.WinForm
{
    public class DataGridRow
    {
        public string? PartId { get; set; }
        public string? RecipeId { get; set; }
        public int? Quantity { get; set; }

        public DataGridRow(string? partId, string? recipeId, int? quantity)
        {
            PartId = partId;
            RecipeId = recipeId;
            Quantity = quantity;
        }
    }
}
