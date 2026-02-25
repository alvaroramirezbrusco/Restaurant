using Application.Features.Dishes.Commands;
using Application.Features.Dishes.Queries;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] DishRequest request)
        {
            var result = await _mediator.Send(new CreateDishCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result); // 201
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDishByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] int? category, [FromQuery] SortDirection? sortByPrice, [FromQuery] bool onlyActive = true)
        {
            var result = await _mediator.Send(new GetAllDishesQuery(name, category, sortByPrice, onlyActive));
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid id, [FromBody] DishUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateDishCommand(id, request));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteDishCommand(id));
            return Ok(result);
        }
    }
}
