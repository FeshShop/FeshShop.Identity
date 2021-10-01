namespace FeshShop.Identity.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password);
    }
}
