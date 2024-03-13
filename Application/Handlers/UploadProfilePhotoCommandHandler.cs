using Application.Commands;
using Application.Services;
using FirebaseAdmin;
using Infrastructure.Services;
using MediatR;

namespace Application.Handlers;

public class UploadProfilePhotoCommandHandler : IRequestHandler<UploadProfilePhotoCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IFirebaseAuthService _authService;

    public UploadProfilePhotoCommandHandler(IUserService userService, IFirebaseAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }
    public async Task<bool> Handle(UploadProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
        return await _userService.UploadPhoto(request.Id, request.profilePhoto);
    }
}