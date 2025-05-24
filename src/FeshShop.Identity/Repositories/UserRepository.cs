namespace FeshShop.Identity.Repositories;

using Contracts;
using FeshShop.Common.Mongo.Contracts;
using Domain;
using System.Threading.Tasks;

public class UserRepository(IMongoRepository<User> mongoRepository) : IUserRepository
{
    public async Task<User> GetAsync(string email)
        => await mongoRepository.GetAsync(x => x.Email.Equals(email, System.StringComparison.InvariantCultureIgnoreCase));

    public async Task AddAsync(User user) => await mongoRepository.AddAsync(user);
}