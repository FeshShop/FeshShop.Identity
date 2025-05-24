namespace FeshShop.Identity.Messages.Commands;

using Newtonsoft.Json;
using System;

public class SignUpInputModel
{
    [JsonConstructor]
    public SignUpInputModel(Guid id, string email, string password, string role)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
    }

    public Guid Id { get; }

    public string Email { get; }

    public string Password { get; }

    public string Role { get; set; }
}