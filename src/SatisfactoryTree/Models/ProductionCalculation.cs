using MermaidDotNet.Models;

namespace SatisfactoryTree.Models
{
    public class ProductionCalculation
    {
        public ProductionCalculation()
        {
            ProductionItems = [];
        }

        public List<ProductionItem> ProductionItems { get; set; }

        private decimal _powerConsumption;
        public decimal PowerConsumption
        {
            get
            {
                return Math.Round(_powerConsumption, 2);
            }
            set
            {
                _powerConsumption = value;
            }
        }

        public MermaidDotNet.Flowchart Flowchart
        {
            get
            {
                MermaidDotNet.Flowchart flowchart = new("LR", new(), new()); ;
                foreach (ProductionItem item in ProductionItems)
                {
                    flowchart.Nodes.Add(new(item.Name, item.Name));
                    //foreach (KeyValuePair<string, decimal> dependency in item.Dependencies)
                    //{
                    //    flowchart.AddNode(dependency.Key);
                    //    flowchart.AddLink(item.Name, dependency.Key, dependency.Value.ToString());
                    //}
                }
                return flowchart;
            }
        }
    }
}