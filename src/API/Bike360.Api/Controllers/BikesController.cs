using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Bikes.Commands.DeleteBike;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BikesController : Controller
{
    private readonly IMediator _mediator;

    public BikesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post(CreateBikeCommand bike)
    {
        var response = await _mediator.Send(bike);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBikeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateBikeCommand bike)
    {
        await _mediator.Send(bike);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BikeDto>>> Get()
    {
        var bikes = await _mediator.Send(new GetAllBikesQuery());
        return Ok(bikes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BikeDetailsDto>> Get(int id)
    {
        var bikeDetails = await _mediator.Send(new GetBikeDetailsQuery(id));
        return Ok(bikeDetails);
    }
}
