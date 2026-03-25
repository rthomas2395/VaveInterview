using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VaveInterview.Core.Services;
using VaveInterview;
using VaveInterview.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl", string.Empty);

builder.Services.AddKeyedSingleton("api", (sp, key) => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl),
    DefaultRequestHeaders =
    {
        { "Accept", "application/json" }
    }
});

builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();
