using InventoryManagementSystem.Application.Commands.Prices;
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
            var PriceId = await _mediator.Send(new DeletePriceCommand(id), ct);

            return Ok(PriceId);

        }
    }
}
