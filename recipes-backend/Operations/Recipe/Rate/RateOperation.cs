using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.Rate
{
    public class RateOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RateOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RateResponse> Execute(RateRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new RateResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var recipe = await db.Recipes.Where(x => x.Id == request.recipeId).FirstOrDefaultAsync();
                var oldRate = await db.RecipeRatings.Where(x=>x.UserId == user.Id && x.RecipeId==request.recipeId).FirstOrDefaultAsync();
                if(oldRate != null)
                {
                    if(oldRate.Rate==request.newRate)
                    {
                        recipe.totalRates = recipe.totalRates - oldRate.Rate;
                        recipe.AmountOfRates = recipe.AmountOfRates - 1;
                        if (recipe.AmountOfRates > 0)
                        {
                            recipe.Rating = recipe.totalRates / (double)recipe.AmountOfRates;
                        }
                        else
                        {
                            recipe.Rating = 0;
                        }
                        db.RecipeRatings.Remove(oldRate);
                    }
                    else
                    {
                        recipe.totalRates = recipe.totalRates + request.newRate - oldRate.Rate;
                        recipe.Rating = recipe.totalRates / (double)recipe.AmountOfRates;
                        oldRate.Rate = request.newRate;
                    }
                }
                else
                {
                    recipe.totalRates = (recipe.totalRates??0) + request.newRate;
                    recipe.AmountOfRates = (recipe.AmountOfRates??0) + 1;
                    recipe.Rating = recipe.totalRates / (double)recipe.AmountOfRates;
                    await db.RecipeRatings.AddAsync(new RecipeRating { RecipeId = request.recipeId, Rate = request.newRate, UserId = user.Id });
                }
                db.SaveChanges();
                return new RateResponse();
            }
            else
            {
                return new RateResponse() { Code=401, Message="User not found" };
            }
        }

        public async Task<ValidateResult> Validate(RateRequest request)
        {
            return new ValidateResult();
        }
    }
}
