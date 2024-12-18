using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EdCom.Data;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddAppDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContextFactory<EdComDbContext, EdComDbContextFactory>();
        serviceCollection.AddScoped(f => f.GetRequiredService<IDbContextFactory<EdComDbContext>>().CreateDbContext());

        serviceCollection.AddDbContext<EdComDbContext>();

        return serviceCollection;
    }
}