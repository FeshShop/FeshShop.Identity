namespace FeshShop.Identity.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}
