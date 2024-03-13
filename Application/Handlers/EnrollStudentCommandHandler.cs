using System.IdentityModel.Tokens.Jwt;
using Application.Commands;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Entities;

namespace Application.Handlers;

public class EnrollStudentCommandHandler : IRequestHandler<EnrollStudentCommand, bool>
{
    private readonly IStudentService _studentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EnrollStudentCommandHandler(IStudentService studentService, IHttpContextAccessor httpContextAccessor)
    {
        _studentService = studentService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        var parts = jwtToken.ToString().Split(' ');
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(parts[1]);
        var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "user_id");
        var userId = userIdClaim?.Value;
        var studentId = await _studentService.GetStudentId(userId);
        var course = new ClassEnrollment
        {   
            ClassId = request.ClassId,
            StudentId = studentId
        };
        return await _studentService.EnrollStudent(course);
    }
}