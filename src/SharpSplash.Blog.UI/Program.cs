using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SharpSplash.Blog.UI;
using SharpSplash.Blog.UI.Infrastructure.Configuration;
using SharpSplash.Blog.UI.Services;
using SharpSplash.Blog.UI.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
    
builder.Services.AddSingleton(provider => 
{
    var config = provider.GetService<IConfiguration>();
    
    return config != null 
        ? config.GetSection("App").Get<CosmicOptions>() 
        : new CosmicOptions();
});

builder.Services.AddMudServices();

builder.Services.AddSingleton<ThemeProvider>();
builder.Services.AddTransient<ICosmicService, CosmicService>();

await builder.Build().RunAsync();
