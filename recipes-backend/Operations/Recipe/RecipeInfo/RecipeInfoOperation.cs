using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using System.Security.Claims;

namespace recipes_backend.Operations.Recipe.RecipeInfo
{
    public class RecipeInfoOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeInfoOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RecipeInfoResponse> Execute(int id)
        {
            var validate = await Validate(id);
            if (validate.Code != 200)
            {
                return new RecipeInfoResponse { Code = validate.Code, Message = validate.Message };
            }
            
            var recipe = await db.Recipes.Include(y => y.AdditionalTips)
                                .Include(y => y.PreparationTips)
                                .Include(y => y.DishType)
                                .Include(y => y.FoodType)
                                .Include(y => y.RecipeIngredients).ThenInclude(y => y.Ingredient)
                                .Include(y => y.RecipeIngredients).ThenInclude(y => y.Measurement)
                                .Include(y => y.User)
                                .Include(y => y.Comments)
                                .Include(y => y.RecipeSteps)
                                .Include(y => y.RecipeImages)
                                .Include(y => y.MenuTypeLists).ThenInclude(y => y.MenuType)
                                .FirstOrDefaultAsync();

            var result = _mapper.Map<RecipeView>(recipe);

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                result.isFavorite = await db.FavoriteRecipes.AnyAsync(x => x.UserId == user.Id && x.RecipeId == recipe.Id);
                var RateInfo = await db.RecipeRatings.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RecipeId == recipe.Id);
                if (RateInfo != null)
                {
                    result.UserRate = RateInfo.Rate;
                }
                foreach (var comment in result.Comments)
                {
                    if (comment.UserId == user.Id)
                    {
                        comment.isAuthor = true;
                    }
                }
            }
            return new RecipeInfoResponse() { Recipe = result };
        }

        public async Task<ValidateResult> Validate(int request)
        {
            if (await db.Recipes.AnyAsync(x => x.Id == request))
                return new ValidateResult();
            return new ValidateResult() { Code = 404 };

        }
    }
}
