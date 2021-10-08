namespace FeshShop.Identity.Repositories
{
    using FeshShop.Identity.Domain;
    using System.Threading.Tasks;

    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
    }
}
