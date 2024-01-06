using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.Products.ReadProductById;
using InventoryManagementSystem.Application.Queries.Products.ReadProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "ReadAllProductsAsync")]
        public async Task<ActionResult<IReadOnlyList<ProductListDto>>> ReadAllProductsAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadProductListQuery(), ct);
            return Ok(dto);
        }

        [HttpGet("{id}", Name = "ReadProductByIdAsync")]
        public async Task<ActionResult<ProductDto>> ReadPRoductByIdAsync(Guid id, CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadProductByIdQuery(id), ct);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateProductAsync")]
        public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);

            return Ok(productId);

        }
    }
}
