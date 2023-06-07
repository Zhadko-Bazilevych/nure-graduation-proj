using AutoMapper;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.Filter;
using recipes_backend.Operations.Recipe.FilterIngredient;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Operations.Recipe.PatternList;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Operations.Recipe.UpdateRecipeInfo;
using recipes_backend.Operations.UserRecipes.AuthorInfo;
using recipes_backend.Operations.UserRecipes.AuthorSubscriptionList;
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
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.RecipeImages.FirstOrDefault().Image))
                .ForMember(dest => dest.Author, act => act.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.AuthorId, act => act.MapFrom(src => src.UserId));

            CreateMap<DishType, IdItem>();

            CreateMap<MenuType, IdItem>();

            CreateMap<FoodType, IdItem>();

            CreateMap<Ingredient, IdIngredient>();

            CreateMap<SearchPattern, IdItem>();

            CreateMap<PatternDishType, IdItem>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.DishTypeId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.DishType.Name));

            CreateMap<PatternFoodType, IdItem>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.FoodTypeId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.FoodType.Name));

            CreateMap<PatternMenuType, IdItem>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.MenuTypeId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.MenuType.Name));

            CreateMap<PatternIngredientList, IdItem>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.IngredientId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Ingredient.Name));

            CreateMap<SearchPattern, Pattern>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.FilterString))
                .ForMember(dest => dest.RequiredTimeMin, act => act.MapFrom(src => src.MinReqTime))
                .ForMember(dest => dest.RequiredTimeMax, act => act.MapFrom(src => src.MaxReqTime))
                .ForMember(dest => dest.IngredientId, act => act.MapFrom(src => src.PatternIngredientLists))
                .ForMember(dest => dest.DishTypeId, act => act.MapFrom(src => src.PatternDishTypes))
                .ForMember(dest => dest.FoodTypeId, act => act.MapFrom(src => src.PatternFoodTypes))
                .ForMember(dest => dest.MenuTypeId, act => act.MapFrom(src => src.PatternMenuTypes));

            CreateMap<User, AuthorShort>();

            CreateMap<User, AuthorInfo>();


            CreateMap<PreparationTip, IdItem>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Description));

            CreateMap<AdditionalTip, IdItem>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Description));

            CreateMap<MenuType, IdItem>();

            CreateMap<RecipeImage, IdItem>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Image));

            CreateMap<RecipeIngredient, CollectedUpdateIngredient>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Measurement, act => act.MapFrom(src => src.Measurement.Name));

            CreateMap<Recipe, RecipeData>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.FoodType, act => act.MapFrom(src => src.FoodType))
                .ForMember(dest => dest.DishType, act => act.MapFrom(src => src.DishType))
                .ForMember(dest => dest.PreparationTips, act => act.MapFrom(src => src.PreparationTips))
                .ForMember(dest => dest.AdditionalTips, act => act.MapFrom(src => src.AdditionalTips))
                .ForMember(dest => dest.MenuTypes, act => act.MapFrom(src => src.MenuTypeLists.Select(s=>s.MenuType)))
                .ForMember(dest => dest.Images, act => act.MapFrom(src => src.RecipeImages))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps));
        }
    }
}