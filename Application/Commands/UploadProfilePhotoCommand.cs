using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands;

public class UploadProfilePhotoCommand : IRequest<bool>
{
    public int Id { get; set; }
    public IFormFile profilePhoto { get; set; }
}