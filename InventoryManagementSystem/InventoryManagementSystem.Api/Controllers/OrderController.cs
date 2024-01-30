using InventoryManagementSystem.Application.Commands.Orders.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateOrderAsync")]
        public async Task<ActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);

            return Ok(productId);

        }
    }
}
