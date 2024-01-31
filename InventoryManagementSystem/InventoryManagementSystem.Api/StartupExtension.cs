using InventoryManagementSystem.Api.Middlewares;
using InventoryManagementSystem.Application;
using InventoryManagementSystem.Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Api
{
    public static class StartupExtension
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7158")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowCredentials();


                    });
            });

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            //Important so Nswag can generate the enums correctly to the UI.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.UseAllOfToExtendReferenceSchemas();
            });
            builder.Services.AddScoped<ExceptionHandlerMiddleware>();
            
            return builder.Build();
        }

        public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app, string[] args)
        {
            if (args.Contains("/seed"))
            {
                await SeedData.EnsurePopulatedAsync(app);
                return app;
            }

            app.UseCors(MyAllowSpecificOrigins);
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
