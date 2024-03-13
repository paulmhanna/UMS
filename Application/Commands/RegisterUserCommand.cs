using MediatR;

namespace Application.Commands;

public class RegisterUserCommand : IRequest<string> 
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}