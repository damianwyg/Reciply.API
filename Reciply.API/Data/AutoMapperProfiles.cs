using AutoMapper;
using Reciply.API.Dtos;
using Reciply.API.Models;

namespace Reciply.API.Data.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListingDto>()
                .ForMember(dest => dest.RecipesCount, opt =>
                {
                    opt.MapFrom(src => src.Recipes.Count);
                });
            CreateMap<User, UserForDetailsDto>();
            CreateMap<Recipe, RecipeForUserListingDto>();
            CreateMap<Recipe, RecipeForDetailsDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Ingredient, IngredientForRecipeDetailsDto>();
            CreateMap<RecipeForAddDto, Recipe>();
            CreateMap<IngredientForRecipeAddDto, Ingredient>();
            CreateMap<RecipeForUpdateDto, Recipe>();
            CreateMap<Comment, CommentForRecipeDetailsDto>()
                .ForMember(dest => dest.UserId, opt =>
                {
                    opt.MapFrom(src => src.User.UserId);
                }); ;
            }
    }
}