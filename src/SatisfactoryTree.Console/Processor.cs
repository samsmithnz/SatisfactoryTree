using SatisfactoryTree.Console.OldModels;
using System.Diagnostics;
using System.Text.Json;

namespace SatisfactoryTree.Console
{
    public class Processor
    {

        public static async Task<FinalData> ProcessFileAsync(string inputFile, string outputFile)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            //try
            //{
            //Read file contexts from text file
            string fileContent = File.ReadAllText(inputFile);
            List<dynamic>? rawData = JsonSerializer.Deserialize<List<dynamic>>(fileContent);
            List<JsonElement> data = new();
            List<JsonElement> rawResourcesData = new();
            if (rawData != null)
            {
                foreach (JsonElement entry in rawData)
                {
                    string? nativeClass = entry.TryGetProperty("NativeClass", out JsonElement nativeClassElement) ? nativeClassElement.GetString() : string.Empty;
                    if (entry.TryGetProperty("Classes", out JsonElement classesElement) && classesElement.ValueKind == JsonValueKind.Array)
                    {
                        data.AddRange(classesElement.EnumerateArray());
                    }
                }
            }

            // Get an array of all buildings that produce something
            List<string> producingBuildings = Buildings.GetProducingBuildings(data);

            // Get power consumption for the producing buildings
            Dictionary<string, double> buildings = Buildings.GetPowerConsumptionForBuildings(data, producingBuildings);

            // Pass the producing buildings with power data to getRecipes to calculate perMin and powerPerProduct
            List<Recipe> recipes = Recipes.GetProductionRecipes(data, buildings);

            // Get parts
            PartDataInterface items = Parts.GetItems(data, recipes);
            Parts.FixItemNames(items);
            Parts.FixTurbofuel(items, recipes);

            // IMPORTANT: The order here matters - don't run this before fixing the turbofuel.
            var powerGenerationRecipes = Recipes.GetPowerGeneratingRecipes(data, items, buildings);

            // Since we've done some manipulation of the items data, re-sort it
            Dictionary<string, Part> sortedItems = new();
            foreach (string? key in items.parts.Keys.OrderBy(k => k))
            {
                sortedItems[key] = items.parts[key];
            }
            items.parts = sortedItems;

            // Construct the final JSON object
            FinalData finalData = new FinalData(
                buildings,
                items,
                recipes,
                powerGenerationRecipes);

            // Write the output to the file
            JsonSerializerOptions options = new() { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            string outputJson = JsonSerializer.Serialize(finalData, options);
            await File.WriteAllTextAsync(outputFile, outputJson);
            stopwatch.Stop();

            System.Console.WriteLine($"Processed {items.parts.Count} parts, {buildings.Count} buildings, and {recipes.Count} recipes, all written to {outputFile}.");
            System.Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            return finalData;
            //}
            //catch (Exception ex)
            //{
            //    System.Console.Error.WriteLine($"Error processing file: {ex.Message}");
            //    return null;
            //}
        }

        public string? InputFile { get; set; }
        public string? OutputFile { get; set; }
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
