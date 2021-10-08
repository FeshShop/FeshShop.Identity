namespace FeshShop.Identity.Messages.Commands
{
    using Newtonsoft.Json;
    using System;

    public class SignUpInputModel
    {
        [JsonConstructor]
        public SignUpInputModel(Guid id, string email, string password, string role)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }

        public Guid Id { get; }

        public string Email { get; }

        public string Password { get; }

        public string Role { get; set; }
    }
}
