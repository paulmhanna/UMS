using MediatR;

namespace Application.Commands;

public class CreateTeacherCourseCommand : IRequest<bool>
{
     public int CourseId { get; set; }
}