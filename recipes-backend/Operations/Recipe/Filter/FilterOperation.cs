using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.Filter;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.Filter
{
    public class FilterOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilterOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FilterResponse> Execute(FilterRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new FilterResponse { Code = validate.Code, Message = validate.Message };
            }


            var filterResult = db.Recipes.Include(y=>y.MenuTypeLists)
                                         .Include(y=>y.RecipeIngredients)
                                         .Include(y=>y.RecipeImages)
                                         .Include(y=>y.User)
                                         .Where(x => x.IsPublished == true &&
                                                  x.Difficulty >= (request.DifficultyMin ?? 1) &&
                                                  x.Difficulty <= (request.DifficultyMax ?? 5));

            if (request.Name != null)
            {
                filterResult = filterResult.Where(x => x.Name.Contains(request.Name));
            }
            if (request.RequiredTimeMin != null)
            {
                filterResult = filterResult.Where(x => x.RequiredTime >= request.RequiredTimeMin);
            }
            if(request.RequiredTimeMax != null)
            {
                filterResult = filterResult.Where(x => x.RequiredTime <= request.RequiredTimeMax);
            }
            if(request.DishTypeId != null && request.DishTypeId.Count!=0)
            {
                filterResult = filterResult.Where(x => request.DishTypeId.Contains(x.DishTypeId??0));
            }
            if(request.FoodTypeId != null && request.FoodTypeId.Count != 0)
            {
                filterResult = filterResult.Where(x => request.FoodTypeId.Contains(x.FoodTypeId??0));
            }
            if(request.MenuTypeId != null)
            {
                foreach(var menu in request.MenuTypeId)
                {
                    filterResult = filterResult.Where(x => x.MenuTypeLists.Any(i => i.MenuTypeId == menu));
                }
            }
            if (request.asIngredientPool)
            {
                filterResult = filterResult.Where(recipe =>
                    recipe.RecipeIngredients.All(recipeIngredient =>
                        (request.IngredientId??new List<int>()).Any(presentIngredient => presentIngredient == recipeIngredient.Id)));
            }
            else if(request.IngredientId != null)
            {
                foreach( var ingredient in request.IngredientId)
                {
                    filterResult = filterResult.Where(x => x.RecipeIngredients.Any(i => i.IngredientId == ingredient));
                }
            }
            switch (request.SortType)
            {
                case "Назва":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.Name); else filterResult = filterResult.OrderByDescending(o => o.Name);
                    break;
                case "Складність":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.Difficulty); else filterResult = filterResult.OrderByDescending(o => o.Difficulty);
                    break;
                case "Калорійність":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.CaloricValue); else filterResult = filterResult.OrderByDescending(o => o.CaloricValue);
                    break;
                case "Рейтинг":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.Rating); else filterResult = filterResult.OrderByDescending(o => o.Rating);
                    break;
                case "Кількість оцінок":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.AmountOfRates); else filterResult = filterResult.OrderByDescending(o => o.AmountOfRates);
                    break;
                case "Необхідний час":
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.RequiredTime); else filterResult = filterResult.OrderByDescending(o => o.RequiredTime);
                    break;
                default:
                    if (!request.isDescending) filterResult = filterResult.OrderBy(o => o.CreationDate); else filterResult = filterResult.OrderByDescending(o => o.CreationDate);
                    break;
            }
            return new FilterResponse { Recipes = _mapper.Map<List<RecipeShort>>(await filterResult.Skip(request.rows??0).Take(15).ToListAsync()) };
        }

        public async Task<ValidateResult> Validate(FilterRequest request)
        {
            return new ValidateResult();
        }
    }
}
