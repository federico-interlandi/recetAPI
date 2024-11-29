using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class Seed
{
    public static async Task SeedEntity<T>(DataContext context, string filePath, List<string>? relationsId = null)  where T : class
    {
        var dbSet = context.Set<T>();
        if (await dbSet.AnyAsync()) return;

        var data = await File.ReadAllTextAsync(filePath);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var entities = JsonSerializer.Deserialize<List<T>>(data, options);

        if (entities == null) return;

        var random = new Random();

        foreach (var entity in entities)
        {
            if (entity is User user)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
            }

            if(entity is Ingredient ing && relationsId != null){
                // set the relationship in recipes for every ingredients
                var recipeId = relationsId[random.Next(relationsId.Count)];

                ing.RecipeId = recipeId;
            }

            if(entity is Recipe recipe && relationsId != null){
                // set the relationship in recipes for every ingredients
                var userEmail = relationsId[random.Next(relationsId.Count)];

                recipe.UserEmail = userEmail;
            }
            dbSet.Add(entity);
        }

        await context.SaveChangesAsync();
    }
}

