using API.Entities;

namespace API.Interfaces;
public interface IRecipeRepository {
    Task<IEnumerable<Recipe>> GetRecipesByUserEmailAsync(string userEmail);

    Task<bool> AddRecipeAsync(Recipe recipe);

    Task<Recipe?> GetRecipeByIdAsync(string recipeId);

    Task<bool> SaveAllAsync();

    void DeleteRecipe(Recipe recipe);
}