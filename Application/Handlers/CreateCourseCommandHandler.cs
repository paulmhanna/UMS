using Application.Commands;
using Application.Services;
using MediatR;
using NpgsqlTypes;
using Persistence.Entities;

namespace Application.Handlers;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
{
    private readonly ICourseService _courseService;

    public CreateCourseCommandHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var dateFrom = request.EnrolmentDateFrom;
        var dateTo = request.EnrolmentDateTo;
        return await _courseService.CreateCourse(new Course
        {
            Name = request.Name,
            MaxStudentsNumber = request.MaxStudentsNumber,
            EnrolmentDateRange = new NpgsqlRange<DateOnly>(request.EnrolmentDateFrom, request.EnrolmentDateTo) 
        });
        
    }
}