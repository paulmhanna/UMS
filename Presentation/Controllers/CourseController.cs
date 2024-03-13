using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Persistence.Context;
using Persistence.Entities;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize]
public class CourseController : ODataController
{
    private readonly IMediator _mediator;
    private readonly MyDbContext _context;
    public CourseController(IMediator mediator, MyDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    
    [HttpGet]
    [EnableQuery]
    public IQueryable<Course> Get()
    {
        return _context.Courses;
    }

    [HttpGet("GetCourseById")]
    public async Task<IActionResult> GetCourseById([FromQuery] GetCourseQuery query)
    {
        try
        {
            var course = await _mediator.Send(query);
            return Ok(course);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // [HttpGet]
    // [EnableQuery]
    // public async Task<IActionResult> Get()
    // {
    //    
    // }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok(result);
            return BadRequest("Create Course Failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse(UpdateCourseCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok(result);
            return BadRequest("Update Course Failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCourse([FromQuery] DeleteCourseCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok(result);
            return BadRequest("Delete Course Failed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}