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
            id0[Miner Mk1] --Iron Ore<br>45 units/min--> id1
            id1[x1.5 Smeltor<br>Iron Ingot] --Iron Ingot<br>45 units/min--> id3
            id3[x1.5 Constructor<br>Iron Plate] --Iron Plate<br>30 units/min--> id4
            id4[30 Iron plate]";
            return View(model: graph);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}