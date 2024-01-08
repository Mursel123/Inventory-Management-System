using AutoMapper;
using InventoryManagementSystem.Application.Contracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace InventoryManagementSystem.Application.Commands.Orders.Create
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CreateOrderCommandHandler(IDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.OrderNumber = GenerateRandomOrderNumber();

            foreach (var orderline in order.OrderLines)
            {
                if (order.Type == OrderType.Purchased || order.Type == OrderType.Sales)
                {
                    var product = await _context.Set<Product>()
                        .AsTracking()
                        .Include(x => x.SubProducts)
                        .Include(x => x.Ingredients)
                        .SingleAsync(x => x.Id == orderline.Product.Id);

                    ProcessProduct(product, orderline, request.Type);

                }
                else if (order.Type == OrderType.Ingredient)
                {
                    var ingredient = await _context.Set<Ingredient>()
                        .AsTracking()
                        .SingleAsync(x => x.Id == orderline.Ingredient.Id);

                    UpdateIngredientMlTotal(ingredient, orderline, true);

                }

            }

            await _context.Set<Order>().AddAsync(order, cancellationToken);
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

        private void UpdateIngredientMlTotal(Ingredient ingredient, OrderLine orderline, bool isIncrement)
        {
            if (isIncrement)
            {
                ingredient.MlTotal += orderline.Quantity * orderline.Ingredient.Prices.Single().Ml;
            }
            else
            {
                ingredient.MlTotal -= orderline.Quantity * orderline.Product.Ingredients.Where(x => x.Id == ingredient.Id).Select(x => x.MlUsage).Single();
            }

            orderline.Ingredient = ingredient;
        }



        private void UpdateProductAmount(Product product, OrderLine orderline, bool isIncrement)
        {
            if (isIncrement)
            {
                product.Amount += orderline.Quantity;
            }
            else
            {
                product.Amount -= orderline.Quantity;
            }

            orderline.Product = product;




        }
        private void ProcessProduct(Product product, OrderLine orderline, OrderType? type)
        {
            //Decreasing the ingredients ml for making the actual product to sell
            foreach (var ingredient in product.Ingredients)
            {
                UpdateIngredientMlTotal(ingredient, orderline, false);
            }

            //Decreasing the products amount for making the actual product to sell
            foreach (var productItem in product.SubProducts)
            {
                UpdateProductAmount(productItem, orderline, false);
            }

            //When purchasing products (main product amount)
            UpdateProductAmount(product, orderline, type == OrderType.Purchased);
        }
    }
}
