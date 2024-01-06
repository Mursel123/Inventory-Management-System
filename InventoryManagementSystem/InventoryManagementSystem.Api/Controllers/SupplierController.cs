using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/selectlist", Name = "ReadAllSupplierSelectListAsync")]
        public async Task<ActionResult<List<SupplierSelectListDto>>> ReadAllSupplierSelectListAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadSupplierSelectListQuery(), ct);
            return Ok(dto);
        }
    }
}
