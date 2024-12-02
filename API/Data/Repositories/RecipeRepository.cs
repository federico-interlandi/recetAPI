using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class RecipeRepository(DataContext context) : IRecipeRepository
{
    public void AddIngredients(List<Ingredient> ingredients, string recipeId)
    {
        foreach (var ing in ingredients)
        {
            ing.RecipeId = recipeId;
            context.Ingredients.Add(ing);
        }
    }

    public async Task<bool> AddRecipeAsync(Recipe recipe)
    {
        if(recipe.Ingredients?.Count > 0){
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.RecipeId = recipe.Id;
            }
        }
        await context.Recipes.AddAsync(recipe);
        
        return await context.SaveChangesAsync() > 0;
    }

    public void DeleteRecipe(Recipe recipe)
    {
        context.Recipes.Remove(recipe);
    }

    public async Task<Recipe?> GetRecipeByIdAsync(string recipeId)
    {
        return await context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Id == recipeId);
    }

    public async Task<IEnumerable<Recipe>> GetRecipesByUserEmailAsync(string userEmail)
    {
        return await context.Recipes.Where(r => r.UserEmail == userEmail).Include(r => r.Ingredients).ToListAsync();
    }

    public void RemoveIngredients(List<Ingredient> ingredients)
    {
        foreach (var ing in ingredients)
        {
            context.Ingredients.Remove(ing);
        }

    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async void UpdateIngredients(List<Ingredient> ingredients)
    {
        foreach (var ing in ingredients)
        {
            var ingrToUpdate = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == ing.Id) ?? throw new Exception("Cannot get ingredient");
            ingrToUpdate.Name = ing.Name;
            ingrToUpdate.Amount = ing.Amount;
        }
        
    }
} 