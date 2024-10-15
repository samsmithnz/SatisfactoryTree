using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SatisfactoryTree.Helpers;
using SatisfactoryTree.Models;
using SatisfactoryTree.Web.Models;
using System.Diagnostics;

namespace SatisfactoryTree.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            Response.Redirect("/home/production");

            //Build the DSP graph
            string filter = "";// "Gravity Matrix";
            SatisfactoryGraph dSPGraph = new(filter,
                ResearchType.Tier8);//,
            //true,
            //    true);

            //Convert the DSP graph to a D3 graph object
            Graph graph = new(dSPGraph.Items);

            //Convert to Json and return the result
            string json = JsonConvert.SerializeObject(graph);
            return View(model: json);
        }

        public IActionResult Production()
        {
            SatisfactoryProduction satisfactoryProduction = new();
            Item productionItem = ItemPoolTier1.Plastic();
            decimal productionQuantity = 20M;
            //Item productionItem = ItemPoolTier3.ReinforcedIronPlate();
            //decimal productionQuantity = 5M;

            //ProductionCalculation productionCalculation = satisfactoryProduction.BuildProductionPlan(new ProductionItem(productionItem, productionQuantity));
            //if (productionCalculation != null)
            //{
            //    string graph3 = satisfactoryProduction.ToMermaidString(true);
            //    //Debug.WriteLine(graph3);
            //    return View(model: graph3);
            //}

            string graph2 = @"flowchart LR
    miner1[""<div align=center><span style='min-width: 100px; display: block;'><img src=https://localhost:7015/Images/Buildings/MinerMk1_256.png style=max-width:100px alt=""Miner Mk1""></span><br>x1 Miner Mk1<br>(Iron Ore)</div>""] --""Iron Ore<br>(60 units/min)""--> Smelter1
    Smelter1[""<div align=center><img src=https://localhost:7015/Images/Buildings/SmelterMk1_256.png style=max-width:100px><br>x2 Smelter<br>(Iron Ingot)""</div>] --""Iron Ingot<br>(15 units/min)""--> constructor1
    Smelter1 --""<div align=center>Iron Ingot<br>(45 units/min)</div>""--> constructor2
    constructor1[""<div align=center><img src=https://localhost:7015/Images/Buildings/ConstructorMk1_256.png style=max-width:100px><br>x1 Constructor<br>(Iron Rod)""</div>] --""Iron Rod<br>(15 units/min)""--> constructor3
    constructor3[""<div align=center><img src=https://localhost:7015/Images/Buildings/ConstructorMk1_256.png style=max-width:100px><br>x1.5 Constructor<br>(Screw)""</div>] --""Screw<br>(60 units/min)""--> constructor4
    constructor2[""<div align=center><img src=https://localhost:7015/Images/Buildings/ConstructorMk1_256.png style=max-width:100px><br>x1.5 Constructor<br>(Iron Plate)""</div>] --""Iron Plate<br>(30 units/min)""--> constructor4
    constructor4[""<div align=center><img src=https://localhost:7015/Images/Buildings/AssemblerMk1_256.png style=max-width:100px><br>x1 Assembler<br>(Reinforced Plate)""</div>] --""Reinforced Plates<br>(5 units/min)""--> end1
    end1[""<div align=center><img src=https://localhost:7015/Images/Items/ReinforcedIronPlate_256.png style=max-width:100px><br>5 Reinforced Plate</div>""]
  ";

            //            string graph = @"
            //flowchart LR
            //    miner1[""Miner Mk1<br>(Iron Ore)""] --""Iron Ore<br>(60 units/min)""--> Smelter1
            //    Smelter1[""x2 Smelter<br>(Iron Ingot)""] --""Iron Ingot<br>(15 units/min)""--> constructor1
            //    Smelter1 --""Iron Ingot<br>(45 units/min)""--> constructor2
            //    constructor1[""x1 Constructor<br>(Iron Rod)""] --""Iron Rod<br>(15 units/min)""--> constructor3
            //    constructor3[""x1.5 Constructor<br>(Screw)""] --""Screw<br>(60 units/min)""--> constructor4
            //    constructor2[""x1.5 Constructor<br>(Iron Plate)""] --""Iron Plate<br>(30 units/min)""--> constructor4
            //    constructor4[""x1 Assembler<br>(Reinforced Plates)""] --""Reinforced Plates<br>(5 units/min)""--> end1
            //    end1[""5 Reinforced plates""]

            //";
            return View(model: graph2);
        }

        public IActionResult Dependency()
        {
            SatisfactoryDependencies satisfactoryDependencies = new();
            MermaidDotNet.Flowchart flowchart = satisfactoryDependencies.BuildDependencyPlan();

            return View(model: flowchart.CalculateFlowchart());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}