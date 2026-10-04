using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration configuration)
    {
        string connectionStringTemplate = configuration.GetConnectionString("MongoDb")!;
        connectionStringTemplate.Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGO_HOST)")).Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGO_PORT"));
        services.AddSingleton<IMongoClient, MongoClient>(provider => new MongoClient(connectionStringTemplate));
        services.AddScoped<IMongoDatabase>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase("OrdersDatabase");
        });
        return services;
    }
}
