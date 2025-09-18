using SatisfactoryTree.Logic.Extraction;

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
            foreach (Factory factory in Factories)
            {
                factory.ComponentParts = calculator.CalculateFactoryProduction(factoryCatalog, factory);
            }
        }

    }
}