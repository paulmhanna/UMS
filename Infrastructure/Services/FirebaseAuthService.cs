using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAuth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using FirebaseAuthException = FirebaseAdmin.Auth.FirebaseAuthException;
using User = Persistence.Entities.User;

namespace Infrastructure.Services;

public class FirebaseAuthService : IFirebaseAuthService
{
    private readonly FirebaseAuthClient _firebaseAuth;

    public FirebaseAuthService(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
    }
    public async Task<string?> RegisterUserAsync(string email, string password)
    {
        var userCredentials = await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
        return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
    }
    
    public async Task<string?> LoginUserAsync(string email, string password)
    {
        var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
        return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
    }
    
    public void SignOut() => _firebaseAuth.SignOut(); 
    
    public string GetUserUUID(string email)
    {
        return FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email).Result.Uid;
    }
}