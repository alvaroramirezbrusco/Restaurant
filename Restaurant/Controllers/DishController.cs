using Application.Features.Dishes.Commands;
using Application.Features.Dishes.Queries;
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
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] DishRequest request)
        {
            var dish = await _mediator.Send(new CreateDishCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish); // 201
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dish = await _mediator.Send(new GetDishByIdQuery(id));
            return Ok(dish);
        }
    }
}
