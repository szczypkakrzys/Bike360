﻿using Bike360.Api.SwaggerExamples;
using Bike360.Api.SwaggerExamples.Exceptions;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.Customers.Commands.DeleteCustomer;
using Bike360.Application.Features.Customers.Commands.UpdateCustomer;
using Bike360.Application.Features.Customers.Queries.GetAllCustomers;
using Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
using Bike360.Application.Features.Customers.Queries.GetCustomerReservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CustomersController : Controller
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
    {
        var customers = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(customers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailsDto>> Get(int id)
    {
        var customerDetails = await _mediator.Send(new GetCustomerDetailsQuery(id));
        return Ok(customerDetails);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateCustomerCommand customer)
    {
        var response = await _mediator.Send(customer);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(UpdateCustomerCommand customer)
    {
        await _mediator.Send(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCustomerCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}/reservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ReservationDto>>> GetCustomerReservations(int id)
    {
        var customerReservations = await _mediator.Send(new GetCustomerReservationsQuery(id));
        return Ok(customerReservations);
    }
}
