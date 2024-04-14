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
                List<MermaidDotNet.Models.Node> nodes = [];
                List<MermaidDotNet.Models.Link> links = [];
                foreach (ProductionItem item in ProductionItems)
                {
                    string buildingName = "none";
                    if (item.Building != null && item.Building.Name != null)
                    {
                        buildingName = item.Building.Name;
                    }
                    string buildingImage = "";
                    if (item.Building != null && item.Building.Image != null)
                    {
                        buildingImage = item.Building.Image;
                    }
                    string itemText = "\"<div align=center><span style='min-width:100px;display:block;'><img src='https://localhost:7015/Images/Buildings/" + buildingImage + "' style='max-width:100px' alt='" + buildingName + "'></span><br> x" + item.BuildingQuantityRequired + " " + buildingName + "<br>(" + item.Name + ")</div>\"";
                    MermaidDotNet.Models.Node node = new(item.Name, itemText, MermaidDotNet.Models.Node.ShapeType.Rounded);
                    nodes.Add(node);
                    foreach (KeyValuePair<string, decimal> dependency in item.Dependencies)
                    {
                        MermaidDotNet.Models.Link link = new(dependency.Key, item.Name, item.Name + "<br>(" + dependency.Value.ToString() + " units/min)");
                        links.Add(link);
                    }
                    if (item.OutputItem == true)
                    {
                        nodes.Add(new(item.Name + "Output", "\"<div align=center><img src='https://localhost:7015/Images/Items/" + item.Item.Image + "' style='max-width:100px' alt='" + item.Name + "'><br>" + item.Quantity.ToString() + " " + item.Name + "</div>\"", MermaidDotNet.Models.Node.ShapeType.Hexagon));
                        MermaidDotNet.Models.Link link = new(item.Name, item.Name + "Output", item.Name + "<br>(" + item.Quantity.ToString() + " units/min)");
                        links.Add(link);
                    }
                }
                MermaidDotNet.Flowchart flowchart = new("LR", nodes, links);
                return flowchart;
            }
        }
    }
}