namespace FeshShop.Identity.Services
{
    using FeshShop.Identity.Domain;
    using FeshShop.Identity.Repositories;
    using FeshShop.Identity.Services.Contracts;
    using System;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository userRepository;

        public IdentityService(IUserRepository userRepository) => this.userRepository = userRepository;

        public async Task SignUpAsync(Guid id, string email, string password) 
        {
            var user = await userRepository.GetAsync(email);

            if (user != null)
            {
                throw new Exception($"Email: '{email}' is already in use.");
            }

            user = new User(id, email);
            await userRepository.AddAsync(user);
        }
    }
}
