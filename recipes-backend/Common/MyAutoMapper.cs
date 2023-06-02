using AutoMapper;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.Filter;
using recipes_backend.Operations.Recipe.FilterIngredient;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Services.GoogleOAuthServiceModels;
using System.Runtime;

namespace SOAPZ.Common
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<UserProfile, User>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Mail, act => act.MapFrom(src => src.Mail));


            CreateMap<RecipeIngredient, CollectedIngredient>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Measurement, act => act.MapFrom(src => src.Measurement.Name));

            CreateMap<RecipeStep, CollectedRecipeStep>();

            CreateMap<Comment, CollectedComment>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.Name));

            CreateMap<Recipe, RecipeView>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.FoodType, act => act.MapFrom(src => src.FoodType.Name))
                .ForMember(dest => dest.DishType, act => act.MapFrom(src => src.DishType.Name))
                .ForMember(dest => dest.PreparationTips, act => act.MapFrom(src => src.PreparationTips.Select(y => y.Description)))
                .ForMember(dest => dest.AdditionalTips, act => act.MapFrom(src => src.AdditionalTips.Select(y => y.Description)))
                .ForMember(dest => dest.MenuTypes, act => act.MapFrom(src => src.MenuTypeLists.Select(y => y.MenuType.Name)))
                .ForMember(dest => dest.Images, act => act.MapFrom(src => src.RecipeImages.Select(y => y.Image)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps));

            CreateMap<Recipe, RecipeShort>()
                .ForMember(dest => dest.RecipeId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.RecipeImages.FirstOrDefault()))
                .ForMember(dest => dest.Author, act => act.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.AuthorId, act => act.MapFrom(src => src.UserId));

            CreateMap<DishType, IdItem>();

            CreateMap<MenuType, IdItem>();

            CreateMap<FoodType, IdItem>();

            CreateMap<Ingredient, IdIngredient>();
        }
    }
}