using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using InventoryManagementSystem.Domain.StaticData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Infrastructure
{
    public class SeedData
    {
        private static AppDbContext _context;
        public async static Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            #region Data
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
                Id = Guid.NewGuid(),
                Name = "Castor Oil",
                MlUsage = 0.3m,
                MlTotal = 100,
                Prices = new() { price}
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

            var productExtra = new Product
            {
                Id = Guid.NewGuid(),
                Amount = 5, 
                Price = 4.05m,
                Supplier = supplier,
                ProductTypes = new() { productTypePurchased },
                Name = "Bottle 100ml"
            };

            var productSell = new Product
            {
                Id = Guid.NewGuid(),
                Amount = 1,
                Price = 10.05m,
                Document = image,
                Name = "Beard Oil Smooth 100ml",
                Ingredients = new() { ingredient },
                ProductTypes = new() { productTypeSales },
                SubProducts = new() { productExtra }

            };

            var productSell2 = new Product
            {
                Id = Guid.NewGuid(),
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
                Product = productExtra,
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

            if (!_context.Product.Any())
            {
                await _context.Product.AddRangeAsync(productExtra, productSell, productSell2);
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
    }
}
