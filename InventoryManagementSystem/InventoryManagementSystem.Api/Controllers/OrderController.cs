using InventoryManagementSystem.Application.Commands.Orders.Create;
using InventoryManagementSystem.Application.DTOs.Order;
using InventoryManagementSystem.Application.Queries.Orders.ReadOrderList;
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

        [HttpGet("all", Name = "ReadOrdersAsync")]
        public async Task<ActionResult<IReadOnlyList<OrderListDto>>> ReadOrdersAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadOrderListQuery(), ct);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateOrderAsync")]
        public async Task<ActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);

            return Ok(productId);

        }
    }
}
