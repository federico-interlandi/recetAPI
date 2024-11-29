namespace API.Entities;

public class Ingredient : BaseEntity{
    public required string Name { get; set; } = string.Empty;

    public required int Amount { get; set; } = 0;

    public string? RecipeId {get; set;}
}