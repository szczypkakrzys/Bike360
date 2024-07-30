using Bike360.Application.Features.Courses.Commands.CreateCourse;
using Bike360.Application.Features.Courses.Commands.DeleteCourse;
using Bike360.Application.Features.Courses.Commands.UpdateCourse;
using Bike360.Application.Features.Courses.Queries.GetAllCourseParticipants;
using Bike360.Application.Features.Courses.Queries.GetAllCourses;
using Bike360.Application.Features.Courses.Queries.GetCourseDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bike360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : Controller
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
    {
        var courses = await _mediator.Send(new GetCoursesQuery());
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDetailsDto>> Get(int id)
    {
        var courseDetails = await _mediator.Send(new GetCourseDetailsQuery(id));
        return Ok(courseDetails);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post(CreateCourseCommand course)
    {
        var response = await _mediator.Send(course);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateCourseCommand course)
    {
        await _mediator.Send(course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCourseCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}/participants")]
    public async Task<ActionResult<IEnumerable<CourseParticipantDto>>> GetTourParticipants(int id)
    {
        var participants = await _mediator.Send(new GetCourseParticipantsQuery(id));
        return Ok(participants);
    }
}
