namespace FeshShop.Identity.Repositories;

using Contracts;
using FeshShop.Common.Mongo.Contracts;
using Domain;
using System.Threading.Tasks;

public class RefreshTokenRepository(IMongoRepository<RefreshToken> mongoRepository) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken token)
        => await mongoRepository.AddAsync(token);
}