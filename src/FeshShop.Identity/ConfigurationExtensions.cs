namespace FeshShop.Identity
{
    using FeshShop.Common.Mongo;
    using FeshShop.Common.Mongo.Contracts;
    using FeshShop.Identity.Domain;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
            => services
                .AddScoped(typeof(IMongoRepository<User>), typeof(MongoRepository<User>))
                .AddScoped(typeof(IMongoRepository<RefreshToken>), typeof(MongoRepository<RefreshToken>));
    }
}
