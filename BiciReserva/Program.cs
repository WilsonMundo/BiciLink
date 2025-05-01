using BiciReserva;
using BiciReserva.Client.Pages;
using BiciReserva.Components;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");




startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, app.Environment);


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BiciReserva.Client._Imports).Assembly);

app.Run();
