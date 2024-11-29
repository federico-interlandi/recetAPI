using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{

    Task<bool> SaveAllAsync();

    Task<IEnumerable<User>> GetUserAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByEmailAsync(string email);

    Task<int> AddUserAsync(User user);
}