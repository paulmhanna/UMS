using Application.Commands;
using Application.Services;
using MediatR;

namespace Application.Handlers;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
{
    private readonly ICourseService _courseService;

    public UpdateCourseCommandHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        
        return await _courseService.UpdateCourse(request.Id, request.NewCourse);
    }
}