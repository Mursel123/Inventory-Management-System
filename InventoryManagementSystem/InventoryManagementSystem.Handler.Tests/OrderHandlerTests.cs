using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.Application.Commands.Orders.Create;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using InventoryManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Handler.Tests
{
    public class OrderHandlerTests : IClassFixture<TestFixture>
    {
        private readonly CreateOrderCommandHandler _createOrderHandler;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderHandlerTests(TestFixture fixture)
        {
            _context = fixture._context;
            _mapper = fixture._mapper;
            _ = fixture.CreateSamples();
            _createOrderHandler = new(fixture._context, fixture._mapper, fixture._mediator);
            
        }

        [Fact]
        public async Task CreateOrderCommandHandler_Should_Decrement_Stock_With_OrderType_Sales() 
        {
            //Given
            var product = await _context.Product
                .Where(x => x.Id == Guid.Parse("9A522691-0E95-4CB7-ACFF-CDB22FBAC06D"))
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            var lines = new List<OrderLineDTO>()
            {
                new OrderLineDTO(){ Product = product, Quantity = 2 },

            };

            var command = new CreateOrderCommand(null, lines, OrderType.Sales);

            //When
            await _createOrderHandler.Handle(command, CancellationToken.None);

            var productUpdated = await _context.Product
                .SingleAsync(x => x.Id == Guid.Parse("9A522691-0E95-4CB7-ACFF-CDB22FBAC06D"));

            var ingredientUpdated = await _context.Ingredient
                .SingleAsync(x => x.Id == Guid.Parse("FBD1843E-AA5A-4425-8DC3-135A3AB5727E"));

            var productUtility = await _context.Product
                .SingleAsync(x => x.Id == Guid.Parse("FF9EFB3F-D98A-4073-9B5C-ADF0C1264C51"));

            //Then
            Assert.True(productUpdated.Amount == 0);
            Assert.True(ingredientUpdated.MlTotal == 99.4m);
            Assert.True(productUtility.Amount == 3);
        }

        [Fact]
        public async Task CreateOrderCommandHandler_Should_Increment_Product_Amount_With_OrderType_Purchased()
        {
            //Given
            var product = await _context.Product
                .Where(x => x.Id == Guid.Parse("FF9EFB3F-D98A-4073-9B5C-ADF0C1264C51"))
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            var lines = new List<OrderLineDTO>()
            {
                new OrderLineDTO(){ Product = product, Quantity = 2 }
            };

            var command = new CreateOrderCommand(null, lines, OrderType.Purchased);

            //When
            await _createOrderHandler.Handle(command, CancellationToken.None);

            var productUpdated = await _context.Product
                .SingleAsync(x => x.Id == Guid.Parse("FF9EFB3F-D98A-4073-9B5C-ADF0C1264C51"));

            //Then
            Assert.True(productUpdated.Amount == 7);

        }

        [Fact]
        public async Task CreateOrderCommandHandler_Should_Increment_Stock_OrderType_Purchased()
        {
            //Given
            var ingredient = await _context.Ingredient
                .Where(x => x.Id == Guid.Parse("FBD1843E-AA5A-4425-8DC3-135A3AB5727E"))
                .ProjectTo<IngredientListDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            var lines = new List<OrderLineDTO>()
            {
                new OrderLineDTO(){ Ingredient = ingredient, Quantity = 2 }
            };

            var command = new CreateOrderCommand(null, lines, OrderType.Ingredient);

            //When
            await _createOrderHandler.Handle(command, CancellationToken.None);

            var ingredientUpdated = await _context.Ingredient
                .SingleAsync(x => x.Id == Guid.Parse("FBD1843E-AA5A-4425-8DC3-135A3AB5727E"));

            //Then
            Assert.True(ingredientUpdated.MlTotal == 160);

        }
    }
}
