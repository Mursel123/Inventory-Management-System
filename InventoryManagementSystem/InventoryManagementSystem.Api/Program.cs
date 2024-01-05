using InventoryManagementSystem.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();

await app.ConfigurePipelineAsync(args);


