using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Infrastructure;

public class FirebaseInitializer
{
    public static void Initialize()
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("../Infrastructure/credentials.json")
        });
    }
}