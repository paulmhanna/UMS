using MediatR;

namespace Application.Commands;

public class CourseToTimeslotCommand : IRequest<bool>
{
    public long CourseId { get; set; }
    public long TimeSlotId { get; set; }
}