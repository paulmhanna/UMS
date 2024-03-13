using Microsoft.AspNetCore.Http;
using Persistence.Entities;

namespace Application.Services;

public interface IUserService
{
    Task<string> GetRole(long roleId);

    Task<bool> IsEmailTaken(string username);
    Task RegisterUser(User newUser);
    Task<bool> UploadPhoto(int id, IFormFile profile);
}