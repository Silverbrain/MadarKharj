using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PersonalFinance.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IAccountService, ClientAccountService>();

await builder.Build().RunAsync();
