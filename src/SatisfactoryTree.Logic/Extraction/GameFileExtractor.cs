using SatisfactoryTree.Logic.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SatisfactoryTree.Logic.Extraction
{
    public class GameFileExtractor
    {
        public static string InputFile { get; set; } = "";
        public static string OutputFile { get; set; } = "";

        public static bool GetContentFiles()
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
            return true;
        }

        public static async Task<ExtractedData> ProcessFileOldModel()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            //try
            //{
            //Read file contexts from text file
            GetContentFiles();
            string fileContent = File.ReadAllText(InputFile);
            List<dynamic>? rawData = System.Text.Json.JsonSerializer.Deserialize<List<dynamic>>(fileContent);
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
            List<string> producingBuildings = ProcessRawBuildings.GetProducingBuildings(data);

            // Get power consumption for the producing buildings
            Dictionary<string, double> buildings = ProcessRawBuildings.GetPowerConsumptionForBuildings(data, producingBuildings);

            // Pass the producing buildings with power data to getRecipes to calculate perMin and powerPerProduct
            List<Recipe> recipes = ProcessRawRecipes.GetProductionRecipes(data, buildings);

            // Get parts
            RawPartsAndRawMaterials items = ProcessRawParts.GetItems(data, recipes);
            ProcessRawParts.FixItemNames(items);
            ProcessRawParts.FixTurbofuel(items, recipes);

            // IMPORTANT: The order here matters - don't run this before fixing the turbofuel.
            List<PowerGenerationRecipe> powerGenerationRecipes = ProcessRawRecipes.GetPowerGeneratingRecipes(data, items, buildings);

            // Since we've done some manipulation of the items data, re-sort it
            Dictionary<string, Part> sortedItems = new();
            foreach (string? key in items.Parts.Keys.OrderBy(k => k))
            {
                sortedItems[key] = items.Parts[key];
            }
            items.Parts = sortedItems;

            //Build the new recipe collection
            List<Recipe> newRecipes = new();
            foreach (Recipe recipe in recipes)
            {
                newRecipes.Add(new()
                {
                    id = recipe.id,
                    displayName = recipe.displayName,
                    ingredients = recipe.ingredients,
                    products = recipe.products,
                    building = recipe.building,
                    isAlternate = recipe.isAlternate,
                    isFicsmas = recipe.isFicsmas,
                    usesSAMOre = recipe.usesSAMOre
                });
            }
            //Now add the power generation recipes
            foreach (PowerGenerationRecipe recipe in powerGenerationRecipes)
            {
                List<Ingredient> ingredients = new();
                foreach (PowerIngredient ingredient in recipe.ingredients)
                {
                    ingredients.Add(new Ingredient()
                    {
                        part = ingredient.part,
                        amount = ingredient.perMin,
                        perMin = ingredient.perMin,
                        mwPerItem = ingredient.mwPerItem,
                    });
                }
                List<Product> products = new();
                if (recipe.byproduct != null)
                {
                    products.Add(
                        new Product()
                        {
                            part = recipe.byproduct.part,
                            amount = recipe.byproduct.perMin,
                            perMin = recipe.byproduct.perMin,
                            isByProduct = true
                        }
                    );
                }

                // Check if any ingredient is SAMIngot to set usesSAMOre flag
                bool usesSAMOre = ingredients.Any(ingredient => ingredient.part == "SAMIngot");

                newRecipes.Add(new()
                {
                    id = recipe.id,
                    displayName = recipe.displayName,
                    ingredients = ingredients,
                    products = products,
                    building = recipe.building,
                    isAlternate = false,
                    isFicsmas = false,
                    usesSAMOre = usesSAMOre
                });
            }
            //sort the new recipes list by id
            newRecipes = newRecipes.OrderBy(r => r.id).ToList();

            // Construct the final JSON object
            ExtractedData extractedData = new(
                buildings,
                items,
                recipes,
                powerGenerationRecipes);

            // Write the output to the file
            JsonSerializerOptions options = new() { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            string outputJson = System.Text.Json.JsonSerializer.Serialize(extractedData, options);
            await File.WriteAllTextAsync(OutputFile, outputJson);
            stopwatch.Stop();

            System.Console.WriteLine($"Processed {items.Parts.Count} parts, {buildings.Count} buildings, and {recipes.Count} recipes, all written to {OutputFile}.");
            System.Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            return extractedData;
            //}
            //catch (Exception ex)
            //{
            //    System.Console.Error.WriteLine($"Error processing file: {ex.Message}");
            //    return null;
            //}
        }

        public static async Task<ExtractedData?> LoadDataFromFile()
        {
            try
            {
                GetContentFiles();
                string targetFile = OutputFile;             

                if (!File.Exists(targetFile))
                {
                    throw new FileNotFoundException($"Configuration file not found: {targetFile}");
                }

                string jsonContent = await File.ReadAllTextAsync(targetFile);
                
                JsonSerializerOptions options = new() 
                { 
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull 
                };
                
                ExtractedData? extractedData = JsonSerializer.Deserialize<ExtractedData>(jsonContent, options);
                
                if (extractedData == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the configuration file");
                }

                System.Console.WriteLine($"Successfully loaded data from {targetFile}");
                System.Console.WriteLine($"Loaded {extractedData.parts?.Count ?? 0} parts, {extractedData.buildings?.Count ?? 0} buildings, {extractedData.recipes?.Count ?? 0} recipes, and {extractedData.powerGenerationRecipes?.Count ?? 0} power generation recipes");
                
                return extractedData;
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"Error loading data from file: {ex.Message}");
                return null;
            }
        }
    }
}
