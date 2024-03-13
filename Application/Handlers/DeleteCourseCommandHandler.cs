using Application.Commands;
using Application.Services;
using MediatR;

namespace Application.Handlers;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseService _courseService;

    public DeleteCourseCommandHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        return await _courseService.DeleteCourse(request.Id);
    }
}