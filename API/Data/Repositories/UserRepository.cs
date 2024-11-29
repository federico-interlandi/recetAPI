using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<int> AddUserAsync(User user)
    {
        context.Add<User>(user);
        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUserAsync()
    {
         return await context.Users
        .ToListAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    
}