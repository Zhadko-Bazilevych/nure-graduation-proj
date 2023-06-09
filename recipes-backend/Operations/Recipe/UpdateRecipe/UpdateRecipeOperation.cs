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

namespace recipes_backend.Operations.Recipe.UpdateRecipe
{
    public class UpdateRecipeOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateRecipeOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateRecipeResponse> Execute(UpdateRecipeRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new UpdateRecipeResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var tempRecipeData = await db.Recipes.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (user.Id == tempRecipeData.UserId)
                {






                    return new UpdateRecipeResponse();
                }
                else
                {
                    return new UpdateRecipeResponse { Code = 401, Message = "You have no acces to this recipe" };
                }
            }
            else
            {
                return new UpdateRecipeResponse() { Code = 401, Message = "You have no acces to this recipe" };
            }
        }

        public async Task<ValidateResult> Validate(UpdateRecipeRequest request)
        {
            return new ValidateResult();
        }
    }
}
