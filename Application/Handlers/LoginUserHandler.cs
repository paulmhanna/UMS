using Application.Commands;
using Application.Services;
using Infrastructure.Services;
using MediatR;

namespace Application.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IFirebaseAuthService _authService;
    private readonly IUserService _userService;

    public LoginUserHandler(IFirebaseAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginUserAsync(request.Email, request.Password);

    }
}