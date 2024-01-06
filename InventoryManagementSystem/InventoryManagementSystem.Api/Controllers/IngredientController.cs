using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientSelectList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/selectlist", Name = "ReadAllIngredientSelectListAsync")]
        public async Task<ActionResult<List<IngredientSelectListDto>>> ReadAllIngredientSelectListAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadIngredientSelectListQuery(), ct);
            return Ok(dto);
        }
    }
}
