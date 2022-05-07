using DSPTree.Models;

namespace DSPTree
{
    public static class RawMaterials
    {
        public static Dictionary<string, decimal> CalculateRawMaterials(List<Item> items, Item item)
        {
            //For the primary recipe, look at the inputs.
            //If the input is not a gathered input, look at those items inputs,
            //adding together all of the resource totals until we find the gatherer.
            //For the initial raw materials, we always want to use a factor of 1, as it's already part of the outputs calculation
            int count = 1;
            Dictionary<string, decimal> rawMaterials = GetRawMaterials(items, item, count);

            return rawMaterials;
        }

        private static Dictionary<string, decimal> GetRawMaterials(List<Item> items, Item item, decimal count)
        {
            Dictionary<string, decimal> rawMaterials = new();

            foreach (Recipe recipe in item.Recipes)
            {
                //get each items materials recursively, summing up all of the items
                if (recipe.PrimaryMethodOfManufacture == true)
                {
                    foreach (KeyValuePair<string, decimal> input in recipe.Inputs)
                    {
                        Item? inputItem = FindItem(items, input.Key);
                        if (inputItem != null)
                        {
                            foreach (Recipe inputItemRecipe in inputItem.Recipes)
                            {
                                if (inputItemRecipe.PrimaryMethodOfManufacture == true)
                                {
                                    //We don't want to include veins
                                    if (inputItem.Level == 1)
                                    {
                                        if (rawMaterials.ContainsKey(input.Key))
                                        {
                                            rawMaterials[input.Key] += input.Value;
                                        }
                                        else
                                        {
                                            rawMaterials.Add(input.Key, input.Value);
                                        }
                                    }
                                    else
                                    {
                                        //We need to dig deeper
                                        Item? detailedItem = FindItem(items, input.Key);
                                        if (detailedItem != null)
                                        {
                                            decimal detailedCount = 0;
                                            foreach (Recipe detailedRecipe in detailedItem.Recipes)
                                            {
                                                //get each items materials recursively, summing up all of the items
                                                if (detailedRecipe.PrimaryMethodOfManufacture == true)
                                                {
                                                    detailedCount = recipe.Inputs[detailedItem.Name];
                                                }
                                            }
                                            Dictionary<string, decimal>? rawMaterialsNextLevel = GetRawMaterials(items, detailedItem, detailedCount);
                                            foreach (KeyValuePair<string, decimal> rawMaterialNextLevel in rawMaterialsNextLevel)
                                            {
                                                if (rawMaterials.ContainsKey(rawMaterialNextLevel.Key))
                                                {
                                                    rawMaterials[rawMaterialNextLevel.Key] += rawMaterialNextLevel.Value;
                                                }
                                                else
                                                {
                                                    rawMaterials.Add(rawMaterialNextLevel.Key, rawMaterialNextLevel.Value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Now apply the counts
            foreach (KeyValuePair<string, decimal> rawMaterial in rawMaterials)
            {
                rawMaterials[rawMaterial.Key] *= count;
            }
            return rawMaterials;
        }

        private static Item? FindItem(List<Item> items, string name)
        {
            Item? item = null;
            if (items != null)
            {
                item = items.Where(x => x.Name == name).FirstOrDefault();
            }
            return item;
        }

    }
}
