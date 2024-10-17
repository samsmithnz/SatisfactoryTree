using Newtonsoft.Json;
using SatisfactoryTree.Models;
using System.Diagnostics;

namespace SatisfactoryTree.ContentExtractor
{

    public class ProcessedResult
    {
        public List<NewItem> Items = new();
        public List<NewRecipe> Recipes = new();
    }

    // Read in the Satisfaction JSON file and extract the relevant (items, recipes, buildings) data into a cleaner format
    public class JsonExtraction
    {
        public static NewContent ExtractJsonFile()
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
            string jsonString = File.ReadAllText(projectContentFile);
            List<RawNativeClass>? rawJSONDoc = System.Text.Json.JsonSerializer.Deserialize<List<RawNativeClass>>(jsonString);
            if (rawJSONDoc == null)
            {
                throw new Exception("Failed to deserialize JSON file");
            }

            // Process the content into an object list
            NewContent processedResult = new();
            List<RawItem> rawItems = new();
            List<string> itemList = new();
            List<NewItem> items = new();
            foreach (RawNativeClass nativeClass in rawJSONDoc)
            {
                if (nativeClass != null && nativeClass.Classes != null &&
                    nativeClass.NativeClassName == "/Script/CoreUObject.Class'/Script/FactoryGame.FGResourceDescriptor'")
                {
                    foreach (RawItem rawItem in nativeClass.Classes)
                    {
                        string result2 = ($"DisplayName: {rawItem.DisplayName}, ClassName: {rawItem.ClassName}, StackSize: {rawItem.StackSize}");
                        items.Add(new NewItem(rawItem.ClassName, rawItem.DisplayName, rawItem.Description, GetStackSizeQuantity(rawItem.StackSize), rawItem.PingColor, rawItem.FluidColor, rawItem.ResourceSinkPoints));
                        itemList.Add(result2);
                    }
                }
                else if (nativeClass != null && nativeClass.Classes != null)
                {
                    rawItems.AddRange(nativeClass.Classes);
                }
            }

            //Get all recipes that are not Christmas or BuildGun or WorkshopComponent
            List<RawItem> rawRecipes = new();
            List<NewBuilding> buildings = new();
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
                    rawRecipes.Add(rawItem);
                }
                string producedIn = rawItem.ProducedIn;
                if (!string.IsNullOrEmpty(rawItem.ProducedIn))
                {
                    producedIn = GetProcessedProducedIn(rawItem.ProducedIn);
                }
                if (rawItem != null && rawItem.ClassName != null &&
                    rawItem.ClassName.StartsWith("Recipe_") &&
                    !string.IsNullOrEmpty(producedIn) &&
                    !producedIn.Contains("BP_BuildGun_C") &&
                    !producedIn.Contains("FGBuildGun") &&
                    !producedIn.Contains("BP_WorkshopComponent_C") &&
                    !buildings.Contains(new(producedIn)))
                {
                    buildings.Add(new NewBuilding(producedIn));
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
                            items.Add(new NewItem(rawItem.ClassName, rawItem.DisplayName, rawItem.Description, GetStackSizeQuantity(rawItem.StackSize), rawItem.PingColor, rawItem.FluidColor, rawItem.ResourceSinkPoints));
                            itemList.Add(result2);
                        }
                        else if (rawItem.DisplayName == "Iron Ore" ||
                            rawItem.DisplayName == "Copper Ore" ||
                            rawItem.DisplayName == "Coal" ||
                            rawItem.DisplayName == "SAM" ||
                            rawItem.DisplayName == "Cat Ore" ||
                            rawItem.DisplayName == "AL Ore")
                        {
                            items.Add(new NewItem(rawItem.ClassName, rawItem.DisplayName, rawItem.Description, GetStackSizeQuantity(rawItem.StackSize), rawItem.PingColor, rawItem.FluidColor, rawItem.ResourceSinkPoints));
                            itemList.Add(result2);
                        }
                    }
                }
            }

            //Order the list alphabetically
            itemList.Sort();

            //processedResult.ItemList = itemList;
            processedResult.Items = items;
            foreach (RawItem recipe in rawRecipes)
            {
                if (recipe != null)
                {
                    decimal manufactoringDuration = 0;
                    decimal.TryParse(recipe.ManufactoringDuration, out manufactoringDuration);
                    List<KeyValuePair<string?, decimal>>? ingredients = ProcessJSONList(recipe.Ingredients);
                    List<KeyValuePair<string?, decimal>>? products = ProcessJSONList(recipe.Products);
                    processedResult.Recipes.Add(new NewRecipe(recipe.ClassName, recipe.DisplayName, ingredients, products, GetProcessedProducedIn(recipe.ProducedIn), manufactoringDuration, recipe.IsAlternateRecipe));
                }
            }
            processedResult.Buildings = buildings;

            string jsonStringToSave = JsonConvert.SerializeObject(processedResult, Newtonsoft.Json.Formatting.Indented);
            SaveJSON(projectContentPath, jsonStringToSave);

            return processedResult;
        }

        private static List<KeyValuePair<string?, decimal>>? ProcessJSONList(string? list)
        {
            List<KeyValuePair<string?, decimal>>? result = new();
            if (list != null)
            {
                // Remove outer parentheses
                string listString = list.Trim('(', ')');

                // Split by "),(" to get individual strings
                string[] listPairs = listString.Split(new string[] { "),(" }, StringSplitOptions.None);

                // Loop through the pairs
                foreach (string pair in listPairs)
                {
                    // Split by "," to get ItemClass and Amount
                    string[] keyValue = pair.Split(',');
                    string itemClass = GetTextAfterLastDot(keyValue[0].Split('=')[1].Trim('"'));
                    string amount = keyValue[1].Split('=')[1];
                    //Console.WriteLine($"ItemClass: {itemClass}, Amount: {amount}");
                    result.Add(new(itemClass, decimal.Parse(amount)));
                }
            }
            return result;
        }

        private static string GetTextAfterLastDot(string input)
        {
            int lastDotIndex = input.LastIndexOf('.');
            if (lastDotIndex == -1)
            {
                return string.Empty; // or handle the case where there's no dot in the string
            }
            return input.Substring(lastDotIndex + 1).Trim('\'');
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

        private static string GetProcessedProducedIn(string producedIn)
        {
            producedIn = producedIn.Replace(",\"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkBenchComponent.BP_WorkBenchComponent_C\"", "");
            producedIn = producedIn.Replace(",\"/Script/FactoryGame.FGBuildableAutomatedWorkBench\"", "");
            producedIn = producedIn.Replace(",\"/Game/FactoryGame/Buildable/Factory/AutomatedWorkBench/Build_AutomatedWorkBench.Build_AutomatedWorkBench_C\"", "");
            switch (producedIn)
            {
                case "(\"/Game/FactoryGame/Buildable/Factory/ConstructorMk1/Build_ConstructorMk1.Build_ConstructorMk1_C\")":
                    return "ConstructorMk1";
                case "(\"/Game/FactoryGame/Buildable/Factory/SmelterMk1/Build_SmelterMk1.Build_SmelterMk1_C\")":
                    return "SmelterMk1";
                case "(\"/Game/FactoryGame/Buildable/Factory/AssemblerMk1/Build_AssemblerMk1.Build_AssemblerMk1_C\")":
                    return "AssemblerMk1";
                case "(\"/Game/FactoryGame/Buildable/Factory/FoundryMk1/Build_FoundryMk1.Build_FoundryMk1_C\")":
                    return "FoundryMk1";
                case "(\"/Game/FactoryGame/Buildable/Factory/Blender/Build_Blender.Build_Blender_C\")":
                    return "Blender";
                case "(\"/Game/FactoryGame/Buildable/Factory/OilRefinery/Build_OilRefinery.Build_OilRefinery_C\")":
                    return "Refinery";
                case "(\"/Game/FactoryGame/Buildable/Factory/Packager/Build_Packager.Build_Packager_C\")":
                    return "Packager";
                case "(\"/Game/FactoryGame/Buildable/Factory/ManufacturerMk1/Build_ManufacturerMk1.Build_ManufacturerMk1_C\")":
                    return "ManufacturerMk1";
                case "(\"/Game/FactoryGame/Buildable/Factory/HadronCollider/Build_HadronCollider.Build_HadronCollider_C\")":
                    return "HadronCollider";
                case "(\"/Game/FactoryGame/Buildable/Factory/QuantumEncoder/Build_QuantumEncoder.Build_QuantumEncoder_C\")":
                    return "QuantumEncoder";
                case "(\"/Game/FactoryGame/Buildable/Factory/Converter/Build_Converter.Build_Converter_C\")":
                    return "Converter";
                default:
                    return producedIn;
            }
        }
    }
}
