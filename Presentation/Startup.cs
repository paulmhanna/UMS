using Application.Configuration;
using Infrastructure;
using Infrastructure.Configuration;
using Persistence.Configuration;

namespace Presentation;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Application")));
        FirebaseInitializer.Initialize();
        services.AddPersistenceConfiguration();
        services.AddInfrastructureConfiguration();
        services.AddApplicationConfiguration();
        services.AddMemoryCache();
    }
}