using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.Queries.ProductTypeList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "ReadAllProductTypesAsync")]
        public async Task<ActionResult<List<ProductTypeDto>>> ReadAllProductTypesAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadProductTypeListQuery(), ct);
            return Ok(dto);
        }
    }
}
