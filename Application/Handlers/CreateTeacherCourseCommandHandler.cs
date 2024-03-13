using System.IdentityModel.Tokens.Jwt;
using Application.Commands;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Entities;

namespace Application.Handlers;

public class CreateTeacherCourseCommandHandler : IRequestHandler<CreateTeacherCourseCommand, bool>
{
    private readonly ITeacherCourseService _teacherCourseService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateTeacherCourseCommandHandler(ITeacherCourseService teacherCourseService, IHttpContextAccessor httpContextAccessor)
    {
        _teacherCourseService = teacherCourseService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<bool> Handle(CreateTeacherCourseCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        var parts = jwtToken.ToString().Split(' ');
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(parts[1]);
        var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "user_id");
        var userId = userIdClaim?.Value;
        var teacherId = await _teacherCourseService.GetTeacherId(userId);
        var teacherCourse = new TeacherPerCourse
        {
            TeacherId = teacherId,
            CourseId = request.CourseId,
        };
        return await _teacherCourseService.CreateTeacherCourse(teacherCourse);

    }
}