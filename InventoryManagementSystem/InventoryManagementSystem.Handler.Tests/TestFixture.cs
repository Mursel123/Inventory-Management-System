using AutoMapper;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using InventoryManagementSystem.Domain.StaticData;
using InventoryManagementSystem.Infrastructure;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Handler.Tests
{
    public class TestFixture
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly DbConnection _connection;
        public AppDbContext _context { get; private set; }
        public IMapper _mapper { get; private set; }
        public IMediator _mediator { get; private set; }
        public TestFixture()
        {
            _connection = new SqliteConnection($"DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(_options);

            var configuration = new MapperConfiguration(cfg =>
            {
                var profiles = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t)));
                cfg.AddMaps(profiles);
            });

            _mapper = configuration.CreateMapper();

            var services = new ServiceCollection();
            services.AddMediatR(Assembly.Load("InventoryManagementSystem.Application")); // Replace with your assembly
            services.AddSingleton(_options);
            services.AddScoped<IDbContext, AppDbContext>();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();

            _context.Database.EnsureCreated();
            _ = CreateSamples();
        }


        internal async Task CreateSamples()
        {

            #region SeedData
            var documentPdf = new Document
            {
                Id = Guid.NewGuid(),
                Type = DocumentType.Pdf,
                FileData = Convert.FromBase64String("JVBERi0xLjMKJcfs")
            };

            var image = new Document
            {
                Id = Guid.NewGuid(),
                Type = DocumentType.Image,
                FileData = Convert.FromBase64String("JVBERi0xLjMKJcfs")
            };

            var price = new Price
            {
                Id = Guid.NewGuid(),
                IngredientPrice = 8.06m,
                Ml = 30,
                WebsiteLink = "https://test.com"
            };

            var ingredient = new Ingredient
            {
                Id = Guid.Parse("FBD1843E-AA5A-4425-8DC3-135A3AB5727E"),
                Name = "Castor Oil",
                MlUsage = 0.3m,
                MlTotal = 100,
                Prices = new() { price }
            };

            var productTypePurchased = new ProductType
            {
                Id = Guid.NewGuid(),
                Type = ProductTypeData.PurchasedInventory
            };

            var productTypeSales = new ProductType
            {
                Id = Guid.NewGuid(),
                Type = ProductTypeData.SalesInventory
            };



            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                Name = "Sample Supplier",
                Email = "supplier@example.com",
                Phone = "123-456-7890"
            };

            var productExtra = new SubProduct
            {
                Id = Guid.Parse("FF9EFB3F-D98A-4073-9B5C-ADF0C1264C51"),
                Amount = 5,
                Price = 4.05m,
                Supplier = supplier,
                Name = "Bottle 100ml"
            };

            var productSell = new Product
            {
                Id = Guid.Parse("9A522691-0E95-4CB7-ACFF-CDB22FBAC06D"),
                Amount = 2,
                Price = 10.05m,
                Document = image,
                Name = "Beard Oil Smooth 100ml",
                Ingredients = new() { ingredient },
                ProductTypes = new() { productTypeSales },
                SubProducts = new() { productExtra }

            };

            var productSell2 = new Product
            {
                Id = Guid.Parse("6362AC0B-F357-482D-9A9E-281D2845A472"),
                Amount = 3,
                Price = 9.05m,
                Document = image,
                Name = "Oil Smooth 100",
                Ingredients = new() { ingredient },
                ProductTypes = new() { productTypeSales },
                SubProducts = new() { productExtra }

            };

            var orderlinesIngredient = new OrderLine
            {
                Ingredient = ingredient,
                Quantity = 1

            };

            var orderlinesExtra = new OrderLine
            {
                SubProduct = productExtra,
                Quantity = 1
            };

            var orderlinesSell = new OrderLine
            {
                Product = productSell,
                Quantity = 1
            };

            var orderIngredient = new Order()
            {
                Id = Guid.NewGuid(),
                OrderNumber = "9485960286",
                Document = documentPdf,
                OrderLines = new() { orderlinesIngredient },
                Type = OrderType.Ingredient
            };

            var orderSell = new Order()
            {
                Id = Guid.NewGuid(),
                OrderNumber = "9563967319",
                Document = documentPdf,
                OrderLines = new() { orderlinesSell },
                Type = OrderType.Sales
            };

            var orderExtra = new Order()
            {
                Id = Guid.NewGuid(),
                OrderNumber = "9672487992",
                Document = documentPdf,
                OrderLines = new() { orderlinesExtra },
                Type = OrderType.Purchased
            };

            var settings = new Settings()
            {
                Id = Guid.NewGuid(),
                AtLeastIngredientMLTotal = 10,
                AtLeastProductAmount = 1
            };

            #endregion
            await ClearTablesAsync();

            if (!_context.Document.Any())
            {
               
                await _context.Document.AddRangeAsync(documentPdf, image);
                await _context.SaveChangesAsync();
            }

            if (!_context.Price.Any())
            {
                await _context.Price.AddAsync(price);
                await _context.SaveChangesAsync();
            }

            if (!_context.Ingredient.Any())
            {
                await _context.Ingredient.AddAsync(ingredient);
                await _context.SaveChangesAsync();
            }

            if (!_context.Supplier.Any())
            {
                await _context.Supplier.AddAsync(supplier);
                await _context.SaveChangesAsync();
            }

            if (!_context.ProductType.Any())
            {
                await _context.ProductType.AddRangeAsync(productTypeSales, productTypePurchased);
                await _context.SaveChangesAsync();
            }
            if (!_context.SubProduct.Any())
            {
                await _context.SubProduct.AddAsync(productExtra);
                await _context.SaveChangesAsync();
            }
            if (!_context.Product.Any())
            {
                await _context.Product.AddRangeAsync(productSell, productSell2);
                await _context.SaveChangesAsync();
            }

            if (!_context.OrderLine.Any())
            {
                await _context.OrderLine.AddRangeAsync(orderlinesExtra, orderlinesIngredient, orderlinesSell);
                await _context.SaveChangesAsync();
            }

            if (!_context.Order.Any())
            {
                await _context.Order.AddRangeAsync(orderExtra, orderIngredient, orderSell);
                await _context.SaveChangesAsync();
            }

            if (!_context.Settings.Any())
            {
                await _context.Settings.AddAsync(settings);
                await _context.SaveChangesAsync();
            }
        }

        private async Task ClearTablesAsync()
        {
            await ClearTableAsync<Document>();
            await ClearTableAsync<Price>();
            await ClearTableAsync<Ingredient>();
            await ClearTableAsync<Supplier>();
            await ClearTableAsync<ProductType>();
            await ClearTableAsync<Product>();
            await ClearTableAsync<OrderLine>();
            await ClearTableAsync<Order>();
            await ClearTableAsync<Settings>();
        }

        private async Task ClearTableAsync<T>() where T : class
        {
            var table = _context.Set<T>();
            if (table.Any())
            {
                table.RemoveRange(table);
                await _context.SaveChangesAsync();
            }
        }
    }
}
