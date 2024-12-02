using System.Text.Json.Serialization;

namespace API.Entities;

public class Recipe : BaseEntity{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public List<Ingredient>? Ingredients { get; set; }
    
    [JsonIgnore]
    public string? UserEmail  { get; set; }


    public required string ImagePath { get; set; }

    public bool? IsFavorite { get; set; } = false;
}