using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
public class TeacherCourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherCourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTeacherCourse")]
    public async Task<IActionResult> CreateTeacherCourse(CreateTeacherCourseCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result) return Ok("You have been assigned to a course");
            return BadRequest("Course assignment failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("CreateTimeSlot")]
    public async Task<IActionResult> CreateTimeslot(CreateTimeslotCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result) return Ok("Timeslot created successfully");
            return BadRequest("Timeslot creation failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AssignToTimeslot")]
    public async Task<IActionResult> AssignCourseToTimeslot(CourseToTimeslotCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result) return Ok("Course assigned to timeslot successfully");
            return BadRequest("Course timeslot assignment failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}