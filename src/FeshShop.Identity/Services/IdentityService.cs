namespace FeshShop.Identity.Services
{
    using FeshShop.Common.Authentication;
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
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IClaimsProvider claimsProvider;
        private readonly IJwtHandler jwtHandler;

        public IdentityService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IRefreshTokenRepository refreshTokenRepository,
            IClaimsProvider claimsProvider,
            IJwtHandler jwtHandler)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.refreshTokenRepository = refreshTokenRepository;
            this.claimsProvider = claimsProvider;
            this.jwtHandler = jwtHandler;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string role = Role.User) 
        {
            var user = await this.userRepository.GetAsync(email);

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

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await this.userRepository.GetAsync(email);
            if (user == null || !user.ValidatePassword(password, this.passwordHasher))
            {
                throw new Exception("Invalid credentials.");
            }

            var claims = await this.claimsProvider.GetAsync(user.Id);
            var jwt = jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
            var refreshToken = new RefreshToken(user, this.passwordHasher);
            jwt.RefreshToken = refreshToken.Token;
                        
            await refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }
    }
}
