using API.Data;
using API.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace API.Services;
public class DatabaseInitializer(DataContext context, ILogger<DatabaseInitializer> logger)
{
    private readonly DataContext _context = context;
    private readonly ILogger<DatabaseInitializer> _logger = logger;

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
            await Seed.SeedEntity<User>(_context, "Data/Seed/UsersSeedData.json");
            var userEmails = await _context.Users.Select(r => r.Email).ToListAsync();
            await Seed.SeedEntity<Recipe>(_context, "Data/Seed/RecipeSeedData.json", userEmails);
            var recipesId = await _context.Recipes.Select(r => r.Id).ToListAsync();
            await Seed.SeedEntity<Ingredient>(_context, "Data/Seed/IngredientsSeedData.json", recipesId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during migration and seeding");
            throw;
        }
    }
}
