namespace FeshShop.Identity.Services;

using Repositories.Contracts;
using Common.Authentication;
using Domain;
using Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

public class IdentityService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IRefreshTokenRepository refreshTokenRepository,
    IClaimsProvider claimsProvider,
    IJwtHandler jwtHandler)
    : IIdentityService
{
    public async Task SignUpAsync(Guid id, string email, string password, string role = Role.User)
    {
        var user = await userRepository.GetAsync(email);

        if (user != null)
            throw new Exception($"Email: '{email}' is already in use.");

        if (string.IsNullOrWhiteSpace(role))
            role = Role.User;

        user = new User(id, email, role);
        user.SetPassword(password, passwordHasher);
        await userRepository.AddAsync(user);
    }

    public async Task<JsonWebToken> SignInAsync(string email, string password)
    {
        var user = await userRepository.GetAsync(email);

        if (user == null || !user.ValidatePassword(password, passwordHasher))
            throw new Exception("Invalid credentials.");

        var claims = await claimsProvider.GetAsync(user.Id);
        var jwt = jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
        var refreshToken = new RefreshToken(user, passwordHasher);
        jwt.RefreshToken = refreshToken.Token;

        await refreshTokenRepository.AddAsync(refreshToken);

        return jwt;
    }
}