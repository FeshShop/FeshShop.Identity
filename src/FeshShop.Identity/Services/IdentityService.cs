namespace FeshShop.Identity.Services
{
    using FeshShop.Identity.Domain;
    using FeshShop.Identity.Repositories;
    using FeshShop.Identity.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string role = Role.User) 
        {
            var user = await userRepository.GetAsync(email);

            if (user != null)
            {
                throw new Exception($"Email: '{email}' is already in use.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }

            user = new User(id, email, role);
            user.SetPassword(password, passwordHasher);
            await userRepository.AddAsync(user);
        }
    }
}
