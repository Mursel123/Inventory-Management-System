﻿using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientById;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList;
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

        [HttpGet("all", Name = "ReadAllIngredientsAsync")]
        public async Task<ActionResult<IReadOnlyList<IngredientListDto>>> ReadAllIngredientsAsync(CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadIngredientListQuery(), ct);
            return Ok(dto);
        }

        [HttpGet("{id}", Name = "ReadIngredientByIdAsync")]
        public async Task<ActionResult<IngredientDto>> ReadIngredientByIdAsync(Guid id, CancellationToken ct)
        {
            var dto = await _mediator.Send(new ReadIngredientByIdQuery(id), ct);
            return Ok(dto);
        }
    }
}
