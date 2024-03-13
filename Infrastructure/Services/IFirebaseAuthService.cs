using Persistence.Entities;

namespace Infrastructure.Services;

public interface IFirebaseAuthService
{
    Task<string?> RegisterUserAsync(string email, string password);
    Task<string?> LoginUserAsync(string email, string password);
    void SignOut();
    string GetUserUUID(string email);
}