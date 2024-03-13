using MediatR;

namespace Application.Commands;

public class EnrollStudentCommand : IRequest<bool>
{
    public long ClassId { get; set; }
}