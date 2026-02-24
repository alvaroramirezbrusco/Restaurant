using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderCreateReponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.OrderNumber }, result); // 201
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? status)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(from, to, status));
            return Ok(result);
        }

        [HttpPatch("{id}/item/{itemId}")]
        [ProducesResponseType(typeof(OrderUpdateReponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(long id, long itemId, [FromBody] OrderItemUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderItemCommand(id, itemId, request));
            return Ok(result);
        }
    }
}
