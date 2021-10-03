namespace FeshShop.Identity.Repositories
{
    using FeshShop.Common.Mongo.Contracts;
    using FeshShop.Identity.Domain;
    using System.Threading.Tasks;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> mongoRepository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task AddAsync(RefreshToken token)
            => await mongoRepository.AddAsync(token);        
    }
}
