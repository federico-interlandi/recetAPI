using API.Entities;

namespace API.Interfaces;
public interface IRecipeRepository {
    Task<IEnumerable<Recipe>> GetRecipesByUserEmail(string userEmail);

}