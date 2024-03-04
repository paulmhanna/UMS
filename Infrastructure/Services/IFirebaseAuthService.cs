namespace Infrastructure.Services;

public interface IFirebaseAuth
{
    Task<string> RegisterUserAsync(string email, string password);
    Task<string> LoginUserAsync(string email, string password);
}