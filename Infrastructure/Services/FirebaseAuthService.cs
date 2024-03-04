namespace Infrastructure.Services;

public class FirebaseAuth
{
    public async Task<string> RegisterUserAsync(string email, string password)
    {
        // Use Firebase Authentication SDK to register a new user
        var user = await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password);
        return user.User.Uid; // Return user ID
    }

    public async Task<string> LoginUserAsync(string email, string password)
    {
        // Use Firebase Authentication SDK to sign in user
        var user = await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);
        return user.User.Uid; // Return user ID
    }
}