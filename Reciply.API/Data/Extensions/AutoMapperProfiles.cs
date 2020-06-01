using AutoMapper;
using Reciply.API.Dtos;
using Reciply.API.Models;

namespace Reciply.API.Data.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListingDto>();
            CreateMap<User, UserForDetailsDto>();
            CreateMap<Recipe, RecipeForUserListingDto>();
            CreateMap<Recipe, RecipeForDetailsDto>();
            CreateMap<Ingredient, IngredientForDetailsDto>();
        }
    }
}