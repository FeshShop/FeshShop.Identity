namespace FeshShop.Identity.Services.Contracts
{
    using FeshShop.Common.Authentication;
    using System;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role);

        Task<JsonWebToken> SignInAsync(string email, string password);
    }
}
