using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService) : BaseApiController
{
    [HttpPost("signup")]
    public async Task<ActionResult<UserDto>> SignUp(RegisterDto registerDto)
    {
        if (await userRepository.GetUserByEmailAsync(registerDto.Email.ToLower()) != null) return BadRequest("Email allready exist");

        using var hmac = new HMACSHA512();

        var user = mapper.Map<User>(registerDto);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;


        if (await userRepository.AddUserAsync(user) > 0)
        {
            var userResponse = await userRepository.GetUserByEmailAsync(user.Email);
            if (userResponse != null)
            {
                var userDto = mapper.Map<UserDto>(userResponse);
                userDto.IdToken = tokenService.CreateToken(userResponse);
                userDto.ExpiresIn = tokenService.GetTokenExpiration(userDto.IdToken);
                return userDto;
            }
        }

        return StatusCode(500, new { message = "Error saving data to db" });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(RegisterDto registerDto)
    {
        var user = await userRepository.GetUserByEmailAsync(registerDto.Email.ToLower());

        if (user == null) return Unauthorized("Invalid email");

        using var hmac = new HMACSHA512(user.PasswordSalt!);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash![i]) return Unauthorized("Invalid Password");
        }

        var userDto = mapper.Map<UserDto>(user);
        userDto.IdToken = tokenService.CreateToken(user);
        userDto.ExpiresIn = tokenService.GetTokenExpiration(userDto.IdToken);
        return userDto;
    }
}