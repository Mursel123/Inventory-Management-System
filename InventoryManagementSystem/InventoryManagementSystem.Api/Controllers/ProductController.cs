using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.Commands.Products.DeleteProduct;
using InventoryManagementSystem.Application.Commands.Products.UpdateProduct;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.Queries.Products.ReadProductById;
using InventoryManagementSystem.Application.Queries.Products.ReadProductList;
using InventoryManagementSystem.Application.Queries.Products.ReadProductByType;
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

        [HttpGet("all/selectlist/{productType}", Name = "ReadProductListByTypeAsync")]
        public async Task<ActionResult<List<ProductSelectListDto>>> ReadProductListByTypeAsync(
            string productType, CancellationToken ct )
        {
            var dto = await _mediator.Send(new ReadProductListByTypeQuery(productType), ct);
            return Ok(dto);
        }

        [HttpGet("{id}", Name = "ReadProductByIdAsync")]
        public async Task<ActionResult<ProductDto>> ReadProductByIdAsync(Guid id, CancellationToken ct)
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

        [HttpPut(Name = "UpdateProductAsync")]
        public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);

            return Ok(productId);

        }

        [HttpDelete(Name = "DeleteProductAsync")]
        public async Task<ActionResult> DeleteProductAsync([FromBody] DeleteProductCommand command, CancellationToken ct)
        {
            var productId = await _mediator.Send(command, ct);

            return Ok(productId);

        }
    }
}
