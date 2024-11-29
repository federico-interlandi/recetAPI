using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RecipeRepository(DataContext context) : IRecipeRepository
{
    public async Task<IEnumerable<Recipe>> GetRecipesByUserEmail(string userEmail)
    {
        return await context.Recipes.Where(r => r.UserEmail == userEmail).Include(r => r.Ingredients).ToListAsync();
    }
} 