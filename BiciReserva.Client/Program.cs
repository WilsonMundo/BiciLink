using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Shared.Auth;
using Shared.RepositoryShared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
ConfigureServices(builder.Services);
builder.Logging.SetMinimumLevel(LogLevel.None);

await builder.Build().RunAsync();



static void ConfigureServices(IServiceCollection services)
{
    
    services.AddAuthorizationCore();
    services.AddRadzenComponents();
    services.AddCascadingAuthenticationState();    
    services.AddScoped<IRepository, Repository>();
    services.AddScoped<ProveedorAutenticacionJWT>();
    services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>
               (provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

    services.AddScoped<ILoginService, ProveedorAutenticacionJWT>
                (provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

}