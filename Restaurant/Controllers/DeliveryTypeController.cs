using Application.Features.DeliveryTypes.Queries;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private IMediator _mediator;

        public DeliveryTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDeliveryTypesQuery());
            return Ok(result);
        }
    }
}
