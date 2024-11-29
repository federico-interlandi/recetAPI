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
        CreateMap<User, UserDto>()
                .ForMember(dest => dest.LocalId, opt => opt.MapFrom(src => src.Id));
    }
}