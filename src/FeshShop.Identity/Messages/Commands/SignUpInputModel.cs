namespace FeshShop.Identity.Messages.Commands
{
    using Newtonsoft.Json;
    using System;

    public class SignUpInputModel
    {
        [JsonConstructor]
        public SignUpInputModel(Guid id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
        }

        public Guid Id { get; }

        public string Email { get; }

        public string Password { get; }        
    }
}
