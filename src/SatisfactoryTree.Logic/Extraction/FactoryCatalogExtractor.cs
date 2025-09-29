using SatisfactoryTree.Logic.Calculations;
using SatisfactoryTree.Logic.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;

namespace SatisfactoryTree.Logic.Extraction
{
    public class FactoryCatalogExtractor
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

            // Only copy if the file doesn't exist or if the files are different
            if (File.Exists(contentPath) && Directory.Exists(projectContentPath))
            {
                bool shouldCopy = !File.Exists(projectContentFile) || !FilesAreEqual(contentPath, projectContentFile);

                if (shouldCopy)
                {
                    Debug.WriteLine("Copying file to " + projectContentPath);
                    CopyFileWithRetry(contentPath, projectContentFile);
                }
                else
                {
                    Debug.WriteLine("File already exists and is identical, skipping copy.");
                }
            }

            InputFile = projectContentFile;
            OutputFile = Path.Combine(projectContentPath, "gameData.json");
            return true;
        }

        private static bool FilesAreEqual(string filePath1, string filePath2)
        {
            try
            {
                // First check file sizes - if different, files are definitely different
                FileInfo fileInfo1 = new(filePath1);
                FileInfo fileInfo2 = new(filePath2);

                if (fileInfo1.Length != fileInfo2.Length)
                {
                    return false;
                }

                // If sizes are the same, compare checksums
                using SHA256 sha256 = System.Security.Cryptography.SHA256.Create();

                byte[] hash1, hash2;
                using (FileStream stream1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    hash1 = sha256.ComputeHash(stream1);
                }

                using (FileStream stream2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    hash2 = sha256.ComputeHash(stream2);
                }

                return hash1.SequenceEqual(hash2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error comparing files: {ex.Message}");
                return false; // Assume files are different if we can't compare them
            }
        }

        private static void CopyFileWithRetry(string source, string destination, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using var sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var destinationStream = new FileStream(destination, FileMode.Create, FileAccess.Write);
                    sourceStream.CopyTo(destinationStream);
                    return; // Success
                }
                catch (IOException ex) when (i < maxRetries - 1)
                {
                    Debug.WriteLine($"Copy attempt {i + 1} failed: {ex.Message}. Retrying in 1 second...");
                    Thread.Sleep(1000); // Wait 1 second before retry
                }
            }
        }

        public static async Task<FactoryCatalog> ProcessGameFile()
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

            //Find the unpackage oil recipe and make it an alternate recipe
            recipes.FirstOrDefault(r => r.Name == "UnpackageOil")!.IsAlternate = true;
            recipes.FirstOrDefault(r => r.Name == "PackagedCrudeOil")!.IsAlternate = true;

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
                    Name = recipe.Name,
                    DisplayName = recipe.DisplayName,
                    Ingredients = recipe.Ingredients,
                    Products = recipe.Products,
                    Building = recipe.Building,
                    IsAlternate = recipe.IsAlternate,
                    IsFicsmas = recipe.IsFicsmas,
                    UsesSAMOre = recipe.UsesSAMOre
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
                    Name = recipe.id,
                    DisplayName = recipe.displayName,
                    Ingredients = ingredients,
                    Products = products,
                    Building = recipe.building,
                    IsAlternate = false,
                    IsFicsmas = false,
                    UsesSAMOre = usesSAMOre
                });
            }
            //sort the new recipes list by id
            newRecipes = newRecipes.OrderBy(r => r.Name).ToList();

            //Get the parts lookup
            List<LookupItem> partsLookup = Lookups.GetParts(items.Parts);

            // Construct the final JSON object
            FactoryCatalog factoryCatalog = new(
                buildings,
                items,
                partsLookup,
                recipes,
                powerGenerationRecipes);

            // Write the output to the file
            JsonSerializerOptions options = new() { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            string outputJson = System.Text.Json.JsonSerializer.Serialize(factoryCatalog, options);
            await File.WriteAllTextAsync(OutputFile, outputJson);
            stopwatch.Stop();

            System.Console.WriteLine($"Processed {items.Parts.Count} parts, {buildings.Count} buildings, and {recipes.Count} recipes, all written to {OutputFile}.");
            System.Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            return factoryCatalog;
            //}
            //catch (Exception ex)
            //{
            //    System.Console.Error.WriteLine($"Error processing file: {ex.Message}");
            //    return null;
            //}
        }

        public static async Task<FactoryCatalog?> LoadDataFromFile()
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

                FactoryCatalog? factoryCatalog = JsonSerializer.Deserialize<FactoryCatalog>(jsonContent, options);

                if (factoryCatalog == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the configuration file");
                }

                System.Console.WriteLine($"Successfully loaded data from {targetFile}");
                System.Console.WriteLine($"Loaded {factoryCatalog.Parts?.Count ?? 0} parts, {factoryCatalog.Buildings?.Count ?? 0} buildings, {factoryCatalog.Recipes?.Count ?? 0} recipes, and {factoryCatalog.PowerGenerationRecipes?.Count ?? 0} power generation recipes");

                return factoryCatalog;
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"Error loading data from file: {ex.Message}");
                return null;
            }
        }
    }
}
