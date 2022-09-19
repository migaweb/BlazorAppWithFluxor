using BlazorAppWithFluxor.Client;
using Blazored.LocalStorage;
using Blazored.Toast;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Using polly to retry failed network calls.
builder.Services
    .AddHttpClient("Default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
    {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(5)
    }));

// Create default HttpClient 
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("Default"));

builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

builder.Services.AddBlazoredToast();

builder.Services.AddBlazoredLocalStorage(config =>
{
  config.JsonSerializerOptions.WriteIndented = true;
});

await builder.Build().RunAsync();
