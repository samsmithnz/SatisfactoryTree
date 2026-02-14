using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SatisfactoryTree.Web;
using SatisfactoryTree.Web.Services;
using SatisfactoryTree.Logic.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register plan service directly
builder.Services.AddScoped<PlanService>();

// Register logic services
builder.Services.AddScoped<PartLookupService>();

await builder.Build().RunAsync();
