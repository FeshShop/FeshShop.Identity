namespace FeshShop.Identity.Domain
{
    using FeshShop.Common.Mongo.Attributes;
    using FeshShop.Common.Types;
    using Microsoft.AspNetCore.Identity;
    using System;

    [BsonCollection("refresh-tokens")]
    public class RefreshToken : IIdentifiable
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string Token { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool Revoked => this.RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            this.Id = Guid.NewGuid();
            this.UserId = user.Id;
            this.CreatedAt = DateTime.UtcNow;
            this.Token = CreateToken(user, passwordHasher);
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}
