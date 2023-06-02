using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.PatternList;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.PatternList
{
    public class PatternListOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatternListOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PatternListResponse> Execute()
        {
            var validate = await Validate();
            if (validate.Code != 200)
            {
                return new PatternListResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();

                var result = await db.SearchPatterns.Include(y => y.PatternIngredientLists).ThenInclude(y => y.Ingredient)
                                                    .Include(y => y.PatternMenuTypes).ThenInclude(y => y.MenuType)
                                                    .Include(y => y.PatternDishTypes).ThenInclude(y => y.DishType)
                                                    .Include(y => y.PatternFoodTypes).ThenInclude(y => y.FoodType)
                                                    .Where(x => x.UserId == user.Id).OrderBy(x=>x.Name).ToListAsync();
                var response = new PatternListResponse
                {
                    Items = _mapper.Map<List<IdItem>>(result),
                    Patterns = _mapper.Map<List<Pattern>>(result)
                };
                return response;
            }
            else
            {
                return new PatternListResponse() { Code = 401, Message = "User not found" };
            }
        }

        public async Task<ValidateResult> Validate()
        {
            return new ValidateResult();
        }
    }
}
