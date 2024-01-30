using InventoryManagementSystem.Application.DTOs.Price;
using MediatR;

namespace InventoryManagementSystem.Application.Queries.Prices.ReadPriceList
{
    public class ReadPriceListByIngredientIdQuery : IRequest<List<PriceListDto>>
    {
        public Guid Id { get; set; }

        public ReadPriceListByIngredientIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
