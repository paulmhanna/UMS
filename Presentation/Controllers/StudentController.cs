using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Roles = "Student")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("EnrollStudent")]
    public async Task<IActionResult> EnrollStudent(EnrollStudentCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result) return Ok("Enrolled successfully");
            return BadRequest("Enrollment failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}