using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.DeleteRecipe;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.UpdateRecipeInfo
{
    public class DeleteRecipeOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteRecipeOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteRecipeResponse> Execute(DeleteRecipeRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new DeleteRecipeResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var recipe = await db.Recipes.Where(x => x.Id == request.recipeId).FirstOrDefaultAsync();
                if(user.Id == recipe.UserId)
                {
                    db.Remove(recipe);
                    await db.SaveChangesAsync();
                    return new DeleteRecipeResponse();
                }
                else
                {
                    return new DeleteRecipeResponse { Code = 401, Message = "You have no acces to this recipe" };
                }
            }
            else
            {
                return new DeleteRecipeResponse() { Code = 401, Message = "You have no acces to this recipe" };
            }
        }

        public async Task<ValidateResult> Validate(DeleteRecipeRequest request)
        {
            return new ValidateResult();
        }
    }
}
