using API.Entities;

namespace API.DTOs;

public class RecipeUpdateDto{
    public string? Name { get; set; }

    public string?  Description { get; set; }

    public string? ImagePath { get; set; }

    public List<Ingredient>? Ingredients { get; set; }

    public bool? IsFavorite { get; set; }

}