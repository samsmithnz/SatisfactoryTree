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
            string graph = @"
flowchart LR
    miner1[(""Miner Mk1<br>(Iron Ore)"")] --""Iron Ore<br>(60 units/min)""--> smeltor1
    smeltor1[""x2 Smeltor<br>(Iron Ingot)""] --""Iron Ingot<br>(15 units/min)""--> constructor1
    smeltor1 --""Iron Ingot<br>(45 units/min)""--> constructor2
    constructor1[""x1 Constructor<br>(Iron Rod)""] --""Iron Rod<br>(15 units/min)""--> constructor3
    constructor3[""x1.5 Constructor<br>(Screw)""] --""Screw<br>(60 units/min)""--> constructor4
    constructor2[""x1.5 Constructor<br>(Iron Plate)""] --""Iron Plate<br>(30 units/min)""--> constructor4
    constructor4[""x1 Assembler<br>(Reinforced Plates)""] --""Reinforced Plates<br>(10 units/min)""--> end1
    end1{{10 Reinforced plates}}
";
            return View(model: graph);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}