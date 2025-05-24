namespace FeshShop.Identity.Repositories.Contracts;

using Domain;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> GetAsync(string email);

    Task AddAsync(User user);
}