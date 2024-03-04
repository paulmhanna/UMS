namespace Infrastructure;

public class FirebaseInitializer
{
    public static void Initialize()
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("path/to/serviceAccountKey.json")
        });
    }
}