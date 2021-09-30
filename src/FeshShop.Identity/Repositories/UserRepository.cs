namespace FeshShop.Identity.Repositories
{
    using FeshShop.Common.Mongo.Contracts;
    using FeshShop.Identity.Domain;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> mongoRepository;

        public UserRepository(IMongoRepository<User> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task<User> GetAsync(string email) 
            => await this.mongoRepository.GetAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user) => await mongoRepository.AddAsync(user);
    }
}
