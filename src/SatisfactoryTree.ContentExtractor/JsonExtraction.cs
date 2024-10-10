using System.Linq;
using System.Text.Json;

namespace SatisfactoryTree.ContentExtractor
{

    public class ProcessedResult
    {
        public List<Item> Items = new();
        public List<string> ItemList = new();
        public List<Recipe> Recipes = new();
        public List<string> RecipeList = new();
    }

    public class JsonExtraction
    {
        public static ProcessedResult ExtractJsonFile()
        {
            // Load the content file
            string contentPath = @"C:\Program Files (x86)\Steam\steamapps\common\Satisfactory\CommunityResources\Docs\en-US.json";
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            string projectContentPath = Path.Combine(currentDir.Parent.Parent.FullName, "content");

            // If the file exists, copy it to the content folder, that is located in the content folder in the root of the project
            if (System.IO.File.Exists(contentPath) && 
                System.IO.Directory.Exists(projectContentPath))
            {
                //Get the current directory
                System.IO.File.Copy(contentPath, projectContentPath, true);
            }
            string jsonString = File.ReadAllText(projectContentPath);
            List<RawNativeClass>? rawJSONDoc = JsonSerializer.Deserialize<List<RawNativeClass>>(jsonString);
            if (rawJSONDoc == null)
            {
                throw new Exception("Failed to deserialize JSON file");
            }

            // Process the content into an object list
            ProcessedResult processedResult = new();
            List<RawItem> rawItems = new();
            foreach (RawNativeClass nativeClass in rawJSONDoc)
            {
                if (nativeClass != null && nativeClass.Classes != null)
                {
                    rawItems.AddRange(nativeClass.Classes);
                }
            }

            //Get all recipes that are not Christmas or BuildGun or WorkshopComponent
            List<string> itemList = new();
            List<Item> items = new();
            List<RawItem> rawRecipes = new();
            foreach (RawItem rawItem in rawItems)
            {
                if (rawItem != null && rawItem.ClassName != null &&
                    rawItem.ClassName.StartsWith("Recipe_") &&
                    !string.IsNullOrEmpty(rawItem.Ingredients) &&
                    !rawItem.Ingredients.Contains("Christmas") &&
                    rawItem.ProducedIn != null &&
                    !rawItem.ProducedIn.Contains("BP_BuildGun_C") &&
                    !rawItem.ProducedIn.Contains("BP_WorkshopComponent_C"))
                {
                    //string recipe = ($"ClassName: {item.ClassName}, DisplayName: {item.DisplayName}, Ingredients: {item.Ingredients}");
                    rawRecipes.Add(rawItem);
                }

            }

            //Now loop through the items checking if they have a recipe
            foreach (RawItem rawItem in rawItems)
            {
                if (rawItem != null && rawItem.ClassName != null &&
                    rawItem.ClassName.StartsWith("Desc_"))
                {
                    foreach (RawItem recipe in rawRecipes)
                    {
                        string result2 = ($"DisplayName: {rawItem.DisplayName}, ClassName: {rawItem.ClassName}, StackSize: {rawItem.StackSize}");
                        if (rawItem.DisplayName != "" &&
                            rawItem.StackSize != null &&
                            recipe.Products != null &&
                            recipe.Products.Contains(rawItem.ClassName) &&
                            itemList.Contains(result2) == false &&
                            recipe.ProducedIn != null &&
                            !recipe.ProducedIn.Contains("BP_BuildGun_C") &&
                            !recipe.ProducedIn.Contains("BP_WorkshopComponent_C"))
                        {
                            items.Add(new Item(rawItem.ClassName, rawItem.DisplayName, rawItem.Description, rawItem.StackSize, rawItem.FluidColor, rawItem.ResourceSinkPoints));
                            itemList.Add(result2);
                        }
                    }
                }
            }

            //Order the list alphabetically
            itemList.Sort();

            processedResult.ItemList = itemList;
            processedResult.Items = items;
            foreach (RawItem recipe in rawRecipes)
            {
                if (recipe != null)
                {
                    processedResult.RecipeList.Add($"ClassName: {recipe.ClassName}, DisplayName: {recipe.DisplayName}, IsAlt: {recipe.IsAlternateRecipe}, Ingredients: {recipe.Ingredients}");
                    processedResult.Recipes.Add(new Recipe(recipe.ClassName, recipe.DisplayName, recipe.Description, recipe.Ingredients, recipe.Products, recipe.ProducedIn, recipe.ManufactoringDuration, recipe.IsAlternateRecipe));
                }
            }

            return processedResult;


        }
    }

}
