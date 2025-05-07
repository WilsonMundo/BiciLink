using BiciReserva;
using BiciReserva.Client.Pages;
using BiciReserva.Components;
using Serilog.Events;
using Serilog;
using System.Globalization;

Log.Logger = new LoggerConfiguration()
 .MinimumLevel.Warning()
 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
      .CreateLogger();
try
{


    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
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
         .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(BiciReserva.Client._Imports).Assembly);

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}


