using AutoMapper;
using recipes_backend.Operations.Recipe.Rate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.changeFavorite;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.changeFavorite
{
    public class changeFavoriteOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public changeFavoriteOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<changeFavoriteResponse> Execute(changeFavoriteRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new changeFavoriteResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var fav = await db.FavoriteRecipes.Where(x=>x.UserId==user.Id && x.RecipeId==request.recipeId).FirstOrDefaultAsync();
                var recipe = await db.Recipes.Where(x=>x.Id == request.recipeId).FirstOrDefaultAsync();
                if(fav != null)
                {
                    db.FavoriteRecipes.Remove(fav);
                    recipe.AmountOfFavorites--;
                }
                else
                {
                    db.FavoriteRecipes.Add(new FavoriteRecipe { RecipeId = request.recipeId, UserId = user.Id });
                    recipe.AmountOfFavorites = (recipe.AmountOfFavorites ?? 0) + 1;
                }
            }
            else
            {
                return new changeFavoriteResponse() { Code = 401, Message = "User not found" };
            }
            await db.SaveChangesAsync();
            return new changeFavoriteResponse();
        }

        public async Task<ValidateResult> Validate(changeFavoriteRequest request)
        {
            return new ValidateResult();
        }
    }
}
