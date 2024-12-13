namespace SatisfactoryTree.Console.Interfaces
{
    public class Ingredient
    {
        public string part { get; set; }
        public double amount { get; set; }
        public double perMin { get; set; }
    }

    public class Product
    {
        public string part { get; set; }
        public double amount { get; set; }
        public double perMin { get; set; }
        public bool? isByProduct { get; set; }
    }

    public class Recipe
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Product> products { get; set; }
        public Building building { get; set; }
        public bool isAlternate { get; set; }
        public bool isFicsmas { get; set; }
    }

    public class PowerGenerationRecipe
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Product> products { get; set; }
        public Building building { get; set; }
    }

    public class Fuel
    {
        public string primaryFuel { get; set; }
        public string supplementaryFuel { get; set; }
        public string byProduct { get; set; }
        public double byProductAmount { get; set; }
    }

    public class Building
    {
        public string name { get; set; }
        public double power { get; set; }
        public double? minPower { get; set; }
        public double? maxPower { get; set; }
    }
}
