using MediatR;
using Persistence.Entities;

namespace Application.Commands;

public class UpdateCourseCommand : IRequest<bool>
{
    public int Id { get; set; }
    
    public Course NewCourse { get; set; }
}