using MediatR;
using Persistence.Entities;

namespace Application.Queries;

public class GetCourseQuery : IRequest<Course>
{
    public int Id { get; set; }
}