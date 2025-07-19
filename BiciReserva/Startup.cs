using BiciReserva.Services.DataBaseValidate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Radzen;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Interface.DataAcces;
using DataAccess.Repository.Seguridad;
using DataAccess.Repository.Utils;
using BusinessLogic.Services.User;
using BusinessLogic.Interface;
using BiciReserva.Services;
using BiciReserva.Middelwares;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using BiciReserva.Components;
using DataAccess.Repository.RBicicleta;
using DataAccess.Repository.REstacion;
using BusinessLogic.Services.SBicicleta;
using BusinessLogic.Services.SStation;
using DataAccess.Repository.RMaintenance;
using BusinessLogic.Services.SMaintenance;
using Domain.Interface;

using BusinessLogic.Services.SReserva;
using DataAccess.Repository.RReserva;

namespace BiciReserva
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            string CadenaConexion = Configuration["SQLConnection"] ??throw new Exception("Cadena de conexion Vacia");

            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            services.AddScoped<DatabaseConnectionManager>();
            services.AddDbContext<RegisterDBContext>((serviceProvider, options) =>
            {
                var connectionManager = serviceProvider.GetRequiredService<DatabaseConnectionManager>();
                var connectionString = connectionManager.ValidateConnectionString(CadenaConexion, "UserBiciLink");
                options.UseSqlServer(connectionString);
            });
            services.AddDbContext<SystemDBContext>((serviceProvider, options) =>
            {
                var connectionManager = serviceProvider.GetRequiredService<DatabaseConnectionManager>();
                var connectionString = connectionManager.ValidateConnectionString(CadenaConexion, "BiciLink");
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<RegisterDBContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                        ClockSkew = TimeSpan.Zero

                    }; options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["AuthToken"];
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        },
                    };
                });
            services.AddApiAuthorization();
            services.AddCascadingAuthenticationState();
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(kvp => kvp.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var errorResponse = new ResultAPI<Dictionary<string, string[]>>
                    {
                        error = true,
                        message = "Validacion fallida",
                        code = StatusHttpResponse.BadRequest,
                        result = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = " Bici Link", Version = "v1" });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = " Bici Link ", Version = "v2" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorizacion",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.RelativePath is null)
                        return false;
                    
                    return apiDesc.RelativePath.StartsWith($"api/{docName}");
                });


            });
            services.AddRadzenComponents();
            services.AddScoped<ResponseService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUtilsRepository, UtilsRepository>();
            services.AddScoped<IIAuthService, IAuthService>();
            services.AddScoped<IEstadoBicicletaRepository, EstadoBicicletaRepository>();
            services.AddScoped<IBicicletaRepository, BicicletaRepository>();
            services.AddScoped<IEstacionRepository, EstacionRepository>();
            services.AddScoped<IBicycleService, BicycleService>();
            services.AddScoped<StateBicycleService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IStateMaintenanceRepository, StateMaintenanceRepository>();
            services.AddScoped<IStateMaintenanceService, StateMaintenanceService>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IMaintenanceService,  MaintenanceService>();
            services.AddScoped<IEstadoReservaRepository, EstadoReservaRepository>();
            services.AddScoped<IStateReservaService, StateReservaService>();
            services.AddScoped<IReservaRepository,  ReservaRepository>();
            services.AddScoped<IReservaService,  ReservaService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware<DecodificaUrl>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bici Link  V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Bici Link  V2");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
               
            });
        }
    }
}
