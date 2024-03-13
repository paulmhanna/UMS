using MediatR;

namespace Application.Commands;

public class DeleteCourseCommand : IRequest<bool>
{
    public int Id { get; set; }
}