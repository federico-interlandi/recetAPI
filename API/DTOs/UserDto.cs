using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class UserDto
{

    public required string Email { get; set; }

    public required string LocalId {get; set;}

    public string? IdToken { get; set; }

    public string? ExpiresIn { get; set; }

}