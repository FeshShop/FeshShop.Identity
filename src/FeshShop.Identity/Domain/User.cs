namespace FeshShop.Identity.Domain
{
    using FeshShop.Common.Mongo.Attributes;
    using FeshShop.Common.Types;
    using Microsoft.AspNetCore.Identity;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Text.RegularExpressions;

    [BsonCollection("users")]
    public class User : IIdentifiable
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        [BsonId]
        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        protected User()
        {
        }

        public User(Guid id, string email)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new Exception($"Invalid email: '{email}'.");
            }
            
            this.Id = id;
            this.Email = email.ToLowerInvariant();
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }

            this.PasswordHash = passwordHasher.HashPassword(this, password);
        }
    }
}
