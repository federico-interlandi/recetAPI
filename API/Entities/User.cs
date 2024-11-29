using System.Text.Json.Serialization;

namespace API.Entities;

public class User : BaseEntity
{
    private string _email = string.Empty;  

    public required string Email
    {
        get => _email;  
        set => _email = value.ToLower();  
    }
    

    [JsonIgnore]
    public byte[]? PasswordHash { get; set; }

    [JsonIgnore]
    public byte[]? PasswordSalt { get; set; }

}