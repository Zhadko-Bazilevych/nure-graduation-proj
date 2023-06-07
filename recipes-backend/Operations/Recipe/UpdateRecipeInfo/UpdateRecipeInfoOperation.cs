using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.UpdateRecipeInfo
{
    public class UpdateRecipeInfoOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateRecipeInfoOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateRecipeInfoResponse> Execute(UpdateRecipeInfoRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new UpdateRecipeInfoResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var tempRecipeData = await db.Recipes.Where(x => x.Id == request.recipeId).FirstOrDefaultAsync();
                if(user.Id == tempRecipeData.UserId)
                {
                    var recipe = await db.Recipes.Include(y => y.AdditionalTips)
                                .Include(y => y.PreparationTips)
                                .Include(y => y.DishType)
                                .Include(y => y.FoodType)
                                .Include(y => y.RecipeIngredients).ThenInclude(y => y.Ingredient)
                                .Include(y => y.RecipeIngredients).ThenInclude(y => y.Measurement)
                                .Include(y => y.User)
                                .Include(y => y.RecipeSteps)
                                .Include(y => y.RecipeImages)
                                .Include(y => y.MenuTypeLists).ThenInclude(y => y.MenuType)
                                .Where(x => x.Id == request.recipeId)
                                .FirstOrDefaultAsync();

                    var result = _mapper.Map<RecipeData>(recipe);
                    return new UpdateRecipeInfoResponse { Recipe = result };
                }
                else
                {
                    return new UpdateRecipeInfoResponse { Code = 401, Message = "You have no acces to this recipe" };
                }
            }
            else
            {
                return new UpdateRecipeInfoResponse() { Code = 401, Message = "You have no acces to this recipe" };
            }
        }

        public async Task<ValidateResult> Validate(UpdateRecipeInfoRequest request)
        {
            return new ValidateResult();
        }
    }
}
