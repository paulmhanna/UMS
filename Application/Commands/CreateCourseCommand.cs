using MediatR;
using NpgsqlTypes;

namespace Application.Commands;

public class CreateCourseCommand : IRequest<bool>
{
    public string Name { get; set; }
    public int MaxStudentsNumber { get; set; }
    public DateOnly EnrolmentDateFrom { get; set; }
    public DateOnly EnrolmentDateTo { get; set; }
}