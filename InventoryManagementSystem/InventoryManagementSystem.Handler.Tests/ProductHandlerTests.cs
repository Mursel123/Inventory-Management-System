using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.Products.ReadProductById;
using InventoryManagementSystem.Application.Queries.Products.ReadProductList;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.StaticData;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Handler.Tests
{
    public class ProductHandlerTests : IClassFixture<TestFixture>
    {
        private readonly CreateProductCommandHandler _createHandler;
        private readonly ReadProductListQueryHandler _readProductListQueryHandler;
        private readonly ReadProductByIdQueryHandler _readProductByIdQueryHandler;
        public ProductHandlerTests(TestFixture fixture)
        {
            var context = fixture._context;
            var mapper = fixture._mapper;
            _createHandler = new(context, mapper);
            _readProductListQueryHandler = new(mapper, context);
            _readProductByIdQueryHandler = new(mapper, context);
            _ = fixture.CreateSamples();
        }

        [Fact]
        public async Task ReadProductListQueryHandler_Should_Return_Type_ProducListDTO_When_Succes()
        {
            // Given
            var query = new ReadProductListQuery();

            // When
            var list = await _readProductListQueryHandler.Handle(query, CancellationToken.None);

            // Then
            Assert.IsType<List<ProductListDto>>(list);
            Assert.True(list.TrueForAll(item => !item.IsDeleted));
        }

        [Theory]
        [InlineData(ProductTypeData.PurchasedInventory)]
        [InlineData(ProductTypeData.SalesInventory)]
        public async Task ReadProductListQueryHandler_Should_Return_ProducListDTO_With_ProductType_When_Succes(string type)
        {
            // Given
            var query = new ReadProductListQuery() { ProductType = type};

            // When
            var list = await _readProductListQueryHandler.Handle(query, CancellationToken.None);

            // Then
            Assert.True(list.TrueForAll(item => !item.IsDeleted));
            foreach (var item in list)
            {
                Assert.True(item.ProductTypes.Exists(x => x.Type == type));
                
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ReadProductListQueryHandler_Should_Return_ProducListDTO_With_Empty_ProductType_When_Succes(string type)
        {
            // Given
            var query = new ReadProductListQuery() { ProductType = type };

            // When
            var list = await _readProductListQueryHandler.Handle(query, CancellationToken.None);

            // Then
            Assert.True(list.TrueForAll(item => !item.IsDeleted));
        }

    }
}
