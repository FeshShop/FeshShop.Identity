namespace FeshShop.Identity.Repositories.Contracts;

using Domain;
using System.Threading.Tasks;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
}