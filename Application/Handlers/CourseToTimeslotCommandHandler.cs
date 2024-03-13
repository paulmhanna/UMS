using Application.Commands;
using Application.Services;
using MediatR;
using Persistence.Entities;

namespace Application.Handlers;

public class CourseToTimeslotCommandHandler : IRequestHandler<CourseToTimeslotCommand, bool>
{
    private readonly ITeacherCourseService _teacherCourseService;

    public CourseToTimeslotCommandHandler(ITeacherCourseService teacherCourseService)
    {
        _teacherCourseService = teacherCourseService;
    }
    
    public async Task<bool> Handle(CourseToTimeslotCommand request, CancellationToken cancellationToken)
    {
        var courseToTimeslot = new TeacherPerCoursePerSessionTime
        {
            TeacherPerCourseId = request.CourseId,
            SessionTimeId = request.TimeSlotId
        };
        return await _teacherCourseService.CourseToTimeslot(courseToTimeslot);

    }
}