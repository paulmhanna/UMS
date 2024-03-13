using Firebase.Auth;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static void AddInfrastructureConfiguration(this IServiceCollection serviceCollection)
    {
        // serviceCollection.AddScoped<FirebaseAuthConfig>();
        // serviceCollection.AddScoped<FirebaseAuthClient>();
        serviceCollection.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
    }
}