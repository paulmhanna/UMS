using Application.Queries;
using Application.Services;
using MediatR;
using Persistence.Entities;

namespace Application.Handlers;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, Course>
{
    private readonly ICourseService _courseService;

    public GetCourseQueryHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public async Task<Course> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        return await _courseService.GetCourseById(request.Id);
    }
}