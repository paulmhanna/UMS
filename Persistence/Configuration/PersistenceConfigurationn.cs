using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence.Configuration;

public static class PersistenceConfigurationn
{
    public static void AddPersistenceConfiguration(this IServiceCollection serviceCollection )
    {
        serviceCollection.AddDbContext<MyDbContext>(options =>
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=mysecretpassword;"));
    }
}