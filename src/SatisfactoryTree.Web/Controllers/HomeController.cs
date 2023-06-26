using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            //Build the DSP graph
            string filter = "";// "Gravity Matrix";
            SatisfactoryGraph dSPGraph = new(filter,
                ResearchType.Tier8,
                true,
                true);

            //Convert the DSP graph to a D3 graph object
            Graph graph = new(dSPGraph.Items);

            //Convert to Json and return the result
            string json = JsonConvert.SerializeObject(graph);
            return View(model: json);
        }

        public IActionResult Production()
        {
            string graph2 = @"flowchart LR
    miner1[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/MinerMk3_256.png?v=1662619375 style=max-width:100px><br>Miner Mk1<br>(Iron Ore)""</div>] --""Iron Ore<br>(60 units/min)""--> smeltor1
    smeltor1[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/SmelterMk1_256.png?v=1662619375 style=max-width:100px><br>x2 Smeltor<br>(Iron Ingot)""</div>] --""Iron Ingot<br>(15 units/min)""--> constructor1
    smeltor1 --""Iron Ingot<br>(45 units/min)""--> constructor2
    constructor1[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/ConstructorMk1_256.png?v=1662619375 style=max-width:100px><br>x1 Constructor<br>(Iron Rod)""</div>] --""Iron Rod<br>(15 units/min)""--> constructor3
    constructor3[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/ConstructorMk1_256.png?v=1662619375 style=max-width:100px><br>x1.5 Constructor<br>(Screw)""</div>] --""Screw<br>(60 units/min)""--> constructor4
    constructor2[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/ConstructorMk1_256.png?v=1662619375 style=max-width:100px><br>x1.5 Constructor<br>(Iron Plate)""</div>] --""Iron Plate<br>(30 units/min)""--> constructor4
    constructor4[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/AssemblerMk1_256.png?v=1662619375 style=max-width:100px><br>x1 Assembler<br>(Reinforced Plates)""</div>] --""Reinforced Plates<br>(5 units/min)""--> end1
    end1[""<div align=center><img src=https://static.satisfactory-calculator.com/img/gameUpdate6/IconDesc_ReinforcedIronPlates_256.png?v=1668514886 style=max-width:100px><br>5 Reinforced plates</div>""]
  ";

//            string graph = @"
//flowchart LR
//    miner1[""Miner Mk1<br>(Iron Ore)""] --""Iron Ore<br>(60 units/min)""--> smeltor1
//    smeltor1[""x2 Smeltor<br>(Iron Ingot)""] --""Iron Ingot<br>(15 units/min)""--> constructor1
//    smeltor1 --""Iron Ingot<br>(45 units/min)""--> constructor2
//    constructor1[""x1 Constructor<br>(Iron Rod)""] --""Iron Rod<br>(15 units/min)""--> constructor3
//    constructor3[""x1.5 Constructor<br>(Screw)""] --""Screw<br>(60 units/min)""--> constructor4
//    constructor2[""x1.5 Constructor<br>(Iron Plate)""] --""Iron Plate<br>(30 units/min)""--> constructor4
//    constructor4[""x1 Assembler<br>(Reinforced Plates)""] --""Reinforced Plates<br>(5 units/min)""--> end1
//    end1[""5 Reinforced plates""]
  
//";
            return View(model: graph2);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}