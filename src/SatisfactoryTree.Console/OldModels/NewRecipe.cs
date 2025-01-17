namespace SatisfactoryTree.Console.OldModels
{
    //public class Ingredient
    //{
    //    public string part { get; set; }
    //    public double amount { get; set; }
    //    public double perMin { get; set; }
    //}

    //public class Product
    //{
    //    public string part { get; set; }
    //    public double amount { get; set; }
    //    public double perMin { get; set; }
    //    public bool? isByProduct { get; set; }
    //}

    //public class PowerIngredient
    //{
    //    public string part { get; set; }
    //    public double perMin { get; set; }
    //    public double? mwPerItem { get; set; }
    //    public double? supplementalRatio { get; set; }
    //}

    //public class PowerProduct
    //{
    //    public string part { get; set; }
    //    public double perMin { get; set; }
    //    //public double supplementalRatio { get; set; }
    //}

    public class NewRecipe
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Product> products { get; set; }
        public Building building { get; set; }
        public bool isAlternate { get; set; }
        public bool isFicsmas { get; set; }
    }

    //public class PowerGenerationRecipe
    //{
    //    public string id { get; set; }
    //    public string displayName { get; set; }
    //    public List<PowerIngredient> ingredients { get; set; }
    //    public PowerProduct byproduct { get; set; }
    //    public Building building { get; set; }
    //}

    //public class Fuel
    //{
    //    public string primaryFuel { get; set; }
    //    public string supplementaryFuel { get; set; }
    //    public string byProduct { get; set; }
    //    public double byProductAmount { get; set; }
    //    public double byProductAmountPerMin { get; set; }
    //    public double burnDurationInS { get; set; }
    //}

    //public class Building
    //{
    //    public string name { get; set; }
    //    public double power { get; set; }
    //    public double? minPower { get; set; }
    //    public double? maxPower { get; set; }
    //}
}
