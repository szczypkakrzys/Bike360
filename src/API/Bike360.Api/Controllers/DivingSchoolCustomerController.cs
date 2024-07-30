using Bike360.Application.Features.DivingSchoolCustomers.Commands.AssignCustomerToCourse;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.DeleteDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.RemoveCustomerFromCourse;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateCustomerCoursePayment;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllCustomerCourses;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetCustomerCourseDetails;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/DivingSchool/Customers/")]
[ApiController]
public class DivingSchoolCustomerController : Controller
{
    private readonly IMediator _mediator;

    public DivingSchoolCustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DivingSchoolCustomerDto>>> Get()
    {
        var customers = await _mediator.Send(new GetDivingSchoolCustomersQuery());
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DivingSchoolCustomerDetailsDto>> Get(int id)
    {
        var customerDetails = await _mediator.Send(new GetDivingSchoolCustomerDetailsQuery(id));
        return Ok(customerDetails);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post(CreateDivingSchoolCustomerCommand customer)
    {
        var response = await _mediator.Send(customer);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateDivingSchoolCustomerCommand customer)
    {
        await _mediator.Send(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteDivingSchoolCustomerCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}/courses")]
    public async Task<ActionResult<IEnumerable<CustomerCourseDto>>> GetAllCustomerCourses(int id)
    {
        var customerCourses = await _mediator.Send(new GetCustomerCoursesQuery(id));
        return Ok(customerCourses);
    }

    [HttpGet("{id}/course/{courseId}")]
    public async Task<ActionResult<CustomerCourseDetailsDto>> GetCustomerCourseDetails(int id, int courseId)
    {
        var customerCourseDetails = await _mediator.Send(new GetCustomerCourseDetailsQuery(id, courseId));
        return Ok(customerCourseDetails);
    }

    [HttpPost("{id}/course/{courseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AssignCustomerToCourse(AssignCustomerToCourseCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/course/{courseId}/payment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateCustomerPayment(UpdateCustomerCoursePaymentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}/course/{courseId}/")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> RemoveCustomerFromCourse(RemoveCustomerFromCourseCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
