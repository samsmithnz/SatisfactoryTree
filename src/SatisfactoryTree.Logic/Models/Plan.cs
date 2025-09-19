namespace SatisfactoryTree.Logic.Models
{
    public class Plan
    {
        public List<Part> Parts { get; set; } = new();
        public List<Recipe> Recipes { get; set; } = new();
        public List<Building> Buildings { get; set; } = new();
        public List<Factory> Factories { get; set; } = new();

        public void UpdatePlanCalculations(FactoryCatalog factoryCatalog)
        {
            Calculator calculator = new();
            // complete inital calculations
            foreach (Factory factory in Factories)
            {
                factory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, factory);
            }

            // check that we produce all requested imported products
            // if we don't produce enough, reduce the imported amount, and not that there is not enough.
            // if an exported product is allocated, note that too, it's it's destination
        }

    }
}