using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<string, DateOnly>().ConvertUsing(s => DateOnly.Parse(s));
        CreateMap<Recipe,RecipeResponseDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.Id));
        CreateMap<Ingredient, IngredientResponseDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.Id));
        CreateMap<User, UserDto>()
                .ForMember(dest => dest.LocalId, opt => opt.MapFrom(src => src.Id));
        CreateMap<RecipeUpdateDto, Recipe>();
    }
}