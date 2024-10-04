using Bike360.Api.SwaggerExamples;
using Bike360.Api.SwaggerExamples.Exceptions;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.Application.Features.Reservations.Commands.DeleteReservations;
using Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;
using Bike360.Application.Features.Reservations.Queries.GetReservationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]

public class ReservationsController : Controller
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationDetailsDto>> Get(int id)
    {
        var reservationDetails = await _mediator.Send(new GetReservationDetailsQuery(id));

        return Ok(reservationDetails);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateReservationCommand reservation)
    {
        var response = await _mediator.Send(reservation);

        var uri = Url.Action(nameof(Get), new { id = response });
        return Created(uri, new { id = response });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteReservationCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch(UpdateReservationStatusCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
