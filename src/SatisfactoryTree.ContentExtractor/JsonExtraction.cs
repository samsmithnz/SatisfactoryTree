using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

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
            //DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            //string projectContentPath = Path.Combine(currentDir.Parent.Parent.Parent.Parent.Parent.FullName, "content");
            DirectoryInfo? currentDir = new(Directory.GetCurrentDirectory());
            DirectoryInfo? parentDir = currentDir.Parent?.Parent?.Parent?.Parent?.Parent;
            if (parentDir == null)
            {
                throw new Exception("Parent directory structure is not as expected.");
            }
            string projectContentPath = Path.Combine(parentDir.FullName, "content");
            string projectContentFile = Path.Combine(projectContentPath, "en-US.json");

            // If the file exists, copy it to the content folder, that is located in the content folder in the root of the project
            if (System.IO.File.Exists(contentPath) &&
                System.IO.Directory.Exists(projectContentPath))
            {
                //Get the current directory
                Debug.WriteLine("Copying file to " + projectContentPath);
                System.IO.File.Copy(contentPath, projectContentFile, true);
            }
            string jsonString = File.ReadAllText(projectContentFile);
            List<RawNativeClass>? rawJSONDoc = System.Text.Json.JsonSerializer.Deserialize<List<RawNativeClass>>(jsonString);
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
                            items.Add(new Item(rawItem.ClassName, rawItem.DisplayName, rawItem.Description, GetStackSizeQuantity(rawItem.StackSize), rawItem.FluidColor, rawItem.ResourceSinkPoints));
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

            string jsonStringToSave = JsonConvert.SerializeObject(processedResult, Newtonsoft.Json.Formatting.Indented);
            SaveJSON(projectContentPath, jsonStringToSave);

            return processedResult;
        }

        private static bool SaveJSON(string path, string jsonString)
        {
            // Save the JSON string to a file.
            string pathToJSON = Path.Combine(path, "output.json");
            File.WriteAllText(pathToJSON, jsonString);
            return true;
        }

        private static int GetStackSizeQuantity(string stackSize)
        {
            if (stackSize == "SS_HUGE")
            {
                return 500;
            }
            else if (stackSize == "SS_BIG")
            {
                return 200;
            }
            else if (stackSize == "SS_MEDIUM")
            {
                return 100;
            }
            else if (stackSize == "SS_SMALL")
            {
                return 50;
            }
            else if (stackSize == "SS_FLUID")
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
}

}
