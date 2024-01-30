using InventoryManagementSystem.Application.Commands.Prices;
using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Application.Queries.Prices.ReadPriceList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PriceController(IMediator mediator) => _mediator = mediator;


        [HttpDelete("{id}", Name = "DeletePriceAsync")]
        public async Task<ActionResult> DeletePriceAsync(Guid id, CancellationToken ct)
        {
            var priceId = await _mediator.Send(new DeletePriceCommand(id), ct);

            return Ok(priceId);

        }

        [HttpGet("{id}", Name = "ReadPriceListByIngredientId")]
        public async Task<ActionResult<List<PriceListDto>>> ReadPriceListByIngredientId(Guid id, CancellationToken ct)
        {
            var prices = await _mediator.Send(new ReadPriceListByIngredientIdQuery(id), ct);

            return Ok(prices);
        }
    }
}
