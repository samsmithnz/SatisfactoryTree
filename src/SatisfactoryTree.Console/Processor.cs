using SatisfactoryTree.Console.Interfaces;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SatisfactoryTree.Console
{
    public class Processor
    {
        // Function to detect if the file is UTF-16
        public static async Task<bool> IsUtf16(string inputFile)
        {
            byte[] buffer = new byte[2];
            using (FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                await fs.ReadAsync(buffer, 0, 2);
            }
            bool bomLE = buffer[0] == 0xFF && buffer[1] == 0xFE;
            bool bomBE = buffer[0] == 0xFE && buffer[1] == 0xFF;
            return bomLE || bomBE;
        }

        // Function to read UTF-16 and convert to UTF-8
        public static async Task<string> ReadFileAsUtf8(string inputFile)
        {
            bool isUtf16Encoding = await IsUtf16(inputFile);
            string content;
            if (isUtf16Encoding)
            {
                byte[] buffer = await File.ReadAllBytesAsync(inputFile);
                content = Encoding.Unicode.GetString(buffer);
            }
            else
            {
                content = await File.ReadAllTextAsync(inputFile, Encoding.UTF8);
            }
            // Remove BOM if it exists
            if (content.Length > 0 && content[0] == '\uFEFF')
            {
                content = content.Substring(1);
            }
            return NormalizeLineEndings(content);
        }

        // Helper function to normalize line endings
        public static string NormalizeLineEndings(string content)
        {
            return content.Replace("\r\n", "\n");
        }

        // Function to clean up the input file to make it valid JSON
        public static string CleanInput(string input)
        {
            string cleaned = input.Replace("\r\n", "\n");
            cleaned = Regex.Replace(cleaned, @",\s*([\]}])", "$1");
            return cleaned;
        }

        public static void RemoveRubbishItems(PartDataInterface items, List<Recipe> recipes)
        {
            // Create a HashSet to store all product keys from recipes
            var recipeProducts = new HashSet<string>();

            // Loop through each recipe to collect all product keys
            foreach (var recipe in recipes)
            {
                foreach (var product in recipe.Products)
                {
                    recipeProducts.Add(product.Part);
                }
                foreach (var ingredient in recipe.Ingredients)
                {
                    recipeProducts.Add(ingredient.Part);
                }
            }

            // Loop through each item in items.Parts and remove any entries that do not exist in recipeProducts
            var partsToRemove = items.Parts.Keys.Where(part => !recipeProducts.Contains(part)).ToList();

            foreach (var part in partsToRemove)
            {
                items.Parts.Remove(part);
            }
        }

        public static async Task<FinalData> ProcessFileAsync(string inputFile, string outputFile)
        {
            //try
            //{
            //Read file contexts from text file
            string fileContent = File.ReadAllText(inputFile);
            //string fileContent = await ReadFileAsUtf8(inputFile);
            //string cleanedContent = CleanInput(fileContent);
            List<dynamic>? data = JsonSerializer.Deserialize<List<dynamic>>(fileContent);

            // Get parts
            PartDataInterface items = Parts.GetItems(data);
            Parts.FixItemNames(items);

            //// Get an array of all buildings that produce something
            //var producingBuildings = Buildings.GetProducingBuildings(data);

            //// Get power consumption for the producing buildings
            //var buildings = Buildings.GetPowerConsumptionForBuildings(data, producingBuildings);

            //// Pass the producing buildings with power data to getRecipes to calculate perMin and powerPerProduct
            //var recipes = Recipes.GetProductionRecipes(data, buildings);
            //RemoveRubbishItems(items, recipes);
            //Parts.FixTurbofuel(items, recipes);

            //// IMPORTANT: The order here matters - don't run this before fixing the turbofuel.
            //var powerGenerationRecipes = Recipes.GetPowerGeneratingRecipes(data, items, buildings);

            // Since we've done some manipulation of the items data, re-sort it
            var sortedItems = new Dictionary<string, Part>();
            foreach (var key in items.Parts.Keys.OrderBy(k => k))
            {
                sortedItems[key] = items.Parts[key];
            }
            items.Parts = sortedItems;

            // Construct the final JSON object
            FinalData finalData = new FinalData(
                null,// buildings, 
                items,
                null,//                    recipes, 
                null);//             powerGenerationRecipes);


            // Write the output to the file
            var options = new JsonSerializerOptions { WriteIndented = true };
            var outputJson = JsonSerializer.Serialize(finalData, options);
            await File.WriteAllTextAsync(outputFile, outputJson);
            System.Console.WriteLine($"Processed parts, buildings, and recipes have been written to {outputFile}.");

            return finalData;
            //}
            //catch (Exception ex)
            //{
            //    System.Console.Error.WriteLine($"Error processing file: {ex.Message}");
            //    return null;
            //}
        }

        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public void UpdateContent()
        {
            // Load the content file
            string contentPath = @"C:\Program Files (x86)\Steam\steamapps\common\Satisfactory\CommunityResources\Docs\en-US.json";
            DirectoryInfo? currentDir = new(Directory.GetCurrentDirectory());
            DirectoryInfo? parentDir = currentDir.Parent?.Parent?.Parent?.Parent?.Parent;
            if (parentDir == null)
            {
                throw new Exception("Parent directory structure is not as expected.");
            }
            string projectContentPath = Path.Combine(parentDir.FullName, "content");
            string projectContentFile = Path.Combine(projectContentPath, "en-US.json");

            // If the file exists, copy it to the content folder, that is located in the content folder in the root of the project
            if (File.Exists(contentPath) &&
                Directory.Exists(projectContentPath))
            {
                //Get the current directory
                Debug.WriteLine("Copying file to " + projectContentPath);
                File.Copy(contentPath, projectContentFile, true);
            }
            InputFile = projectContentFile;
            OutputFile = Path.Combine(projectContentPath, "gameData.json");
        }
    }
}
