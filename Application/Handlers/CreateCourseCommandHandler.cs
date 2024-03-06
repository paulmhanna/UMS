using Application.Commands;
using Application.Services;
using MediatR;

namespace Application.Handlers;

public class CourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
{
    private readonly ICourseService _courseService;

    public CourseCommandHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        _courseService.CreateCourse()
        _context.Courses.Add(new Course
        {
            Name = course.Name,
            MaxStudentsNumber = course.MaxStudentsNumber,
            EnrolmentDateRange = course.EnrolmentDateRange
        });
    }
}