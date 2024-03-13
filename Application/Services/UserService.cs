using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly MyDbContext _context;

    public UserService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetRole(long roleId)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        return role.Name;
    }

    public Task<bool> IsEmailTaken(string email)
    {
        return Task.FromResult(_context.Users.Any(u => u.Email == email));
    }

    public async Task RegisterUser(User newUser)
    {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UploadPhoto(int id, IFormFile profilePicture)
    {
        try
        {
            var filePath = "";
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var fileName = id + Path.GetExtension(profilePicture.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilepictures", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.ProfilePicture = filePath;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}