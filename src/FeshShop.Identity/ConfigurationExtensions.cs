namespace FeshShop.Identity;

using Common.Mongo;
using Common.Mongo.Contracts;
using Domain;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        => services
            .AddScoped<IMongoRepository<User>, MongoRepository<User>>()
            .AddScoped<IMongoRepository<RefreshToken>, MongoRepository<RefreshToken>>();
}