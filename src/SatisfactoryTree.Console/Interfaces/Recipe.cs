namespace SatisfactoryTree.Console.Interfaces
{
    public class Ingredient
    {
        public string Part { get; set; }
        public double Amount { get; set; }
        public double PerMin { get; set; }
    }

    public class Product
    {
        public string Part { get; set; }
        public double Amount { get; set; }
        public double PerMin { get; set; }
        public bool? IsByProduct { get; set; }
    }

    public class Recipe
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Product> Products { get; set; }
        public Building Building { get; set; }
        public bool IsAlternate { get; set; }
        public bool IsFicsmas { get; set; }
    }

    public class PowerGenerationRecipe
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Product> Products { get; set; }
        public Building Building { get; set; }
    }

    public class Fuel
    {
        public string PrimaryFuel { get; set; }
        public string SupplementalResource { get; set; }
        public string ByProduct { get; set; }
        public double ByProductAmount { get; set; }
    }

    public class Building
    {
        public string Name { get; set; }
        public double Power { get; set; }
        public double? MinPower { get; set; }
        public double? MaxPower { get; set; }
    }
}
