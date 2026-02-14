using SatisfactoryTree.Logic.Calculations;

namespace SatisfactoryTree.Logic.Models
{
    public class Factory2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Item> Ingredients { get; set; }
        public Dictionary<int, ImportedItem> ImportedParts { get; set; }
        public readonly FactoryCatalog FactoryCatalog;

        public Factory2(int id, string name, FactoryCatalog factoryCatalog)
        {
            Id = id;
            Name = name;
            Ingredients = new();
            ImportedParts = new();
            FactoryCatalog = factoryCatalog;
        }

        public void AddIngredient(string name, double quantity, Recipe? recipe)
        {
            Item item = new()
            {
                Name = name,
                DisplayName = Lookups.GetPartDisplayName(FactoryCatalog, name),
                Quantity = quantity,
                Recipe = recipe,
            };

            //If no recipe was provided, get the default recipe for the part
            if (recipe == null)
            {
                List<Recipe> recipes = Lookups.GetRecipes(FactoryCatalog, name);
                recipe = recipes.FirstOrDefault();
                if (recipe == null)
                {
                    throw new Exception("No recipe found for: " + name + ", " + quantity);
                }
            }
            if (recipe.Products == null || recipe.Products.Count == 0)
            {
                throw new Exception("Recipe has no products: " + name + ", " + quantity);
            }
            else
            {
                double ingredientRatio = quantity / recipe.Products[0].perMin;
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    double ingredientAmount = ingredient.perMin * ingredientRatio;
                    item.Ingredients.Add(new()
                    {
                        Name = ingredient.part,
                        DisplayName = Lookups.GetPartDisplayName(FactoryCatalog, ingredient.part),
                        Quantity = ingredientAmount,
                        Recipe = recipe
                    });
                }

                // If there are byproducts (assuming there can only be 1 byproduct)
                if (recipe.Products.Count > 1)
                {
                    foreach (Product byProduct in recipe.Products)
                    {
                        if (byProduct.isByProduct)
                        {
                            item.ByProductName = byProduct.part;
                            item.ByProductDisplayName = Lookups.GetPartDisplayName(FactoryCatalog, byProduct.part);
                            item.ByProductQuantity = byProduct.perMin * ingredientRatio;
                            break;
                        }
                    }
                }

                //Add building details
                item.Building = item.Recipe.Building.Name;
                item.BuildingDisplayName = GetBuildingName(item.Recipe.Building.Name);
                item.BuildingImagePath = GetBuildingImagePath(item.Recipe.Building.Name);
                item.BuildingQuantity += ingredientRatio;
                item.BuildingPowerUsage += Lookups.GetBuildingPower(FactoryCatalog, item.Recipe.Building.Name, ingredientRatio);
            }

            Ingredients.Add(item);
        }

        public string GetBuildingImagePath(string buildingName)
        {
            // Handle building name mappings to match image files
            string imageName = buildingName switch
            {
                "smeltermk1" => "SmelterMk1_256.png",
                "foundrymk1" => "Foundry_256.png",
                "constructormk1" => "ConstructorMk1_256.png",
                "assemblermk1" => "AssemblerMk1_256.png",
                "manufacturermk1" => "Manufacturer_256.png",
                "refinery" or "oilrefinery" => "OilRefinery_256.png",
                "packager" => "Packager_256.png",
                "blender" => "Blender_256.png",
                "hadronCollider" => "ParticleAccelerator_256.png",
                "generatorcoal" => "CoalGenerator_256.png",
                "generatorfuel" => "FuelGenerator_256.png",
                "generatornuclear" => "NuclearPowerplant_256.png",
                "generatorbiomass" => "BiomassBurner_256.png",
                "generatorgeothermal" => "GeothermalPowerGenerator_256.png",
                "minermk1" => "MinerMk1_256.png",
                "minermk2" => "MinerMk2_256.png",
                "minermk3" => "MinerMk3_256.png",
                "oilpump" => "OilExtractor_256.png",
                "waterpump" => "WaterExtractor_256.png",
                "frackingextractor" => "ResourceWellExtractor_256.png",
                "frackingsmasher" => "ResourceWellPressurizer_256.png",
                "resourcesink" => "ResourceSink_256.png",
                _ => $"{buildingName}_256.png" // Default: use building name as-is
            };

            return $"images/buildings/{imageName}";
        }

        public string GetBuildingName(string buildingName)
        {
            string name = buildingName switch
            {
                "smeltermk1" => "Smelter",
                "foundrymk1" => "Foundry",
                "constructormk1" => "Constructor",
                "assemblermk1" => "Assembler",
                "manufacturermk1" => "Manufacturer",
                "refinery" or "oilrefinery" => "Refinery",
                "packager" => "Packager",
                "blender" => "Blender",
                "hadronCollider" => "Particle Accelerator",
                "generatorcoal" => "Coal Generator",
                "generatorfuel" => "Fuel Generator",
                "generatornuclear" => "Nuclear Power Plant",
                "generatorbiomass" => "Biomass Burner",
                "generatorgeothermal" => "Geothermal Power Generator",
                "minermk1" => "Miner Mk1",
                "minermk2" => "Miner Mk2",
                "minermk3" => "Miner Mk3",
                "oilpump" => "Oil Extractor",
                "waterpump" => "Water Extractor",
                "frackingextractor" => "Resource Well Extractor",
                "frackingsmasher" => "Resource Well Pressurizer",
                "resourcesink" => "Resource Sink",
                _ => $"{buildingName}" // Default: use building name as-is
            };
            return name;
        }

        public bool HasBuildingImage(string buildingName)
        {
            // Check if we have a specific image mapping for this building
            return buildingName switch
            {
                "smeltermk1" or "foundrymk1" or "constructormk1" or "assemblermk1" or "manufacturermk1" or
                "oilrefinery" or "refinery" or "packager" or "blender" or "hadronCollider" or
                "generatorcoal" or "generatorfuel" or "generatornuclear" or
                "generatorbiomass" or "generatorgeothermal" or
                "minermk1" or "minermk2" or "minermk3" or
                "oilpump" or "waterpump" or "frackingextractor" or
                "frackingsmasher" or "resourcesink" => true,
                _ => false
            };
        }
    }
}
