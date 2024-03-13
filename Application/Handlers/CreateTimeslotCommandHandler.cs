using Application.Commands;
using Application.Services;
using MediatR;
using Persistence.Entities;

namespace Application.Handlers;

public class CreateTimeslotCommandHandler : IRequestHandler<CreateTimeslotCommand, bool>
{
    private readonly ITeacherCourseService _teacherCourseService;

    public CreateTimeslotCommandHandler(ITeacherCourseService teacherCourseService)
    {
        _teacherCourseService = teacherCourseService;
    }
    
    public async Task<bool> Handle(CreateTimeslotCommand request, CancellationToken cancellationToken)
    {
        return await _teacherCourseService.CreateTimeSlot(request.StartTime, request.EndTime);
    }
}