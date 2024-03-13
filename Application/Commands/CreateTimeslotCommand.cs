using MediatR;

namespace Application.Commands;

public class CreateTimeslotCommand : IRequest<bool>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}