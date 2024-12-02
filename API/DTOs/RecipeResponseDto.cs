namespace API.DTOs;
public class RecipeResponseDto
{
    public string? _id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public bool IsFavorite { get; set; }
    public IEnumerable<IngredientResponseDto>? Ingredients { get; set; }
}