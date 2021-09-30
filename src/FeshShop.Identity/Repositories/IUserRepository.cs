namespace FeshShop.Identity.Repositories
{
    using FeshShop.Identity.Domain;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetAsync(string email);

        Task AddAsync(User user);
    }
}
