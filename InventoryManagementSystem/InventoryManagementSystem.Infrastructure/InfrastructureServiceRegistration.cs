using InventoryManagementSystem.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableDetailedErrors();
                options.ConfigureWarnings(warningAction =>
                {
                    warningAction.Log(
                        CoreEventId.FirstWithoutOrderByAndFilterWarning,
                        CoreEventId.RowLimitingOperationWithoutOrderByWarning
                    );
                });

                options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString"), options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 3);
                });

            });

            services.AddScoped<IDbContext, AppDbContext>();

            // Database initialization logic
            /*using (var serviceProvider = services.BuildServiceProvider())
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }*/

            return services;
        }
    }
}
