namespace FeshShop.Identity.Messages.Commands
{
    using Newtonsoft.Json;

    public class SignInInputModel
    {
        [JsonConstructor]
        public SignInInputModel(string email, string password)
        {            
            this.Email = email;
            this.Password = password;            
        }        

        public string Email { get; }

        public string Password { get; }       
    }
}
