using Application.Commands;
using Application.Services;
using Firebase.Auth;
using MediatR;
using Infrastructure.Services;
using User = Persistence.Entities.User;

namespace Application.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IFirebaseAuthService _authService;
    private readonly IUserService _userService;

    public RegisterUserHandler(IFirebaseAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.IsEmailTaken(request.Email))
        {
            throw new Exception("Email is already taken");
        }
        var token = await _authService.RegisterUserAsync(request.Email, request.Password);
        
        var firebaseId  = _authService.GetUserUUID(request.Email);
        await _userService.RegisterUser(new User
        {
            Name = request.Name,
            Email = request.Email,
            RoleId = request.RoleId,
            FirebaseId = firebaseId
        });
        var role = await _userService.GetRole(request.RoleId);
        var claims = new Dictionary<string, object>()
        {
            { "role",  role},
        };
        await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(firebaseId, claims);
        return token;
    }

}