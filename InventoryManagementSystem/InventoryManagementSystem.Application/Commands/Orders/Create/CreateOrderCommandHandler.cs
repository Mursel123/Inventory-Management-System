using AutoMapper;
using InventoryManagementSystem.Domain.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Commands.Orders.Create
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.OrderNumber = GenerateRandomOrderNumber();

            if (order.OrderLines.Any())
            {
                foreach (var orderline in order.OrderLines)
                {
                    if (order.Type == OrderType.Purchased || order.Type == OrderType.Sales)
                    {
                        var product = await _context.Set<Product>().AsTracking().Include(x => x.Ingredients).SingleAsync(x => x.Id == orderline.Product.Id);

                        var productUpdated = await UpdateProductAmountAsync(product, orderline, cancellationToken);

                        orderline.Product = productUpdated;
                    }
                    else if (order.Type == OrderType.Ingredient)
                    {
                        var ingredient = await _context.Set<Ingredient>().AsTracking().SingleAsync(x => x.Id == orderline.Ingredient.Id);

                        var ingredientUpdated = await UpdateIngredientMlAsync(ingredient, orderline, cancellationToken);

                        orderline.Ingredient = ingredientUpdated;

                    }
                    
                }
            }

            _context.Set<Order>().Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

        private string GenerateRandomOrderNumber()
        {
            Random random = new Random();
            StringBuilder sb = new();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(random.Next(10));
            }

            if (!IsRandomNumberUnique(sb.ToString()))
            {
                GenerateRandomOrderNumber();
            }

            return sb.ToString();
        }

        private bool IsRandomNumberUnique(string number)
        {
            return !_context.Set<Order>().Where(x => x.OrderNumber == number).Any();
        }

        private async Task<Ingredient> UpdateIngredientMlAsync(Ingredient ingredient, OrderLine orderline, CancellationToken ct)
        {
            ingredient.MlTotal = orderline.Ingredient.MlTotal;

            _context.Set<Ingredient>().Update(ingredient);
            await _context.SaveChangesAsync(ct);

            return ingredient;
        }

        private async Task<Ingredient> UpdateIngredientMlInProductAsync(Ingredient ingredient, OrderLine orderline, CancellationToken ct)
        {
            var ingredientUpdated = orderline.Product.Ingredients.Where(x => x.Id == ingredient.Id).Single();
            ingredient.MlTotal = ingredientUpdated.MlTotal;

            _context.Set<Ingredient>().Update(ingredient);
            await _context.SaveChangesAsync(ct);

            return ingredient;
        }

        private async Task<Product> UpdateProductAmountAsync(Product product, OrderLine orderline, CancellationToken ct)
        {
            foreach (var ingredient in product.Ingredients)
            {
                await UpdateIngredientMlInProductAsync(ingredient, orderline, ct);
            }

            product.Amount = orderline.Product.Amount;
            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync(ct);

            return product;
        }
    }
}
