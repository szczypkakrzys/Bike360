using Bike360.Api.SwaggerExamples;
using Bike360.Api.SwaggerExamples.Exceptions;
using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Bikes.Commands.DeleteBike;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;
using Bike360.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BikesController : Controller
{
    private readonly IMediator _mediator;

    public BikesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateBikeCommand bike)
    {
        var response = await _mediator.Send(bike);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBikeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(UpdateBikeCommand bike)
    {
        await _mediator.Send(bike);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BikeDto>>> Get()
    {
        var bikes = await _mediator.Send(new GetAllBikesQuery());
        return Ok(bikes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BikeDetailsDto>> Get(int id)
    {
        var bikeDetails = await _mediator.Send(new GetBikeDetailsQuery(id));
        return Ok(bikeDetails);
    }

    /// <summary>
    /// Retrieves date ranges during which the bike is reserved within the specified time period.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeStart">The start of the time period to search, as a DateTime value.</param>
    /// <param name="timeEnd">The end of the time period to search, as a DateTime value.</param>
    /// <remarks>
    /// Example request:
    /// 
    /// GET /api/bikes/1/reserved-days?timeStart=2024-10-01T00:00:00&amp;timeEnd=2024-10-30T23:59:59
    /// 
    /// </remarks>
    [HttpGet("{id}/reserved-days")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<DateRange>>> GetBikeReservedDays(
        [FromRoute] int id,
        [FromQuery] DateTime timeStart,
        [FromQuery] DateTime timeEnd)
    {
        var query = new GetBikeReservedTimeQuery(id, timeStart, timeEnd);
        var reservedDateRanges = await _mediator.Send(query);
        return Ok(reservedDateRanges);
    }

}
