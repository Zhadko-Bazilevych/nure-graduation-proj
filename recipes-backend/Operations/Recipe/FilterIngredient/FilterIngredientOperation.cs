using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.FilterIngredient;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.FilterIngredient
{
    public class FilterIngredientOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilterIngredientOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FilterIngredientResponse> Execute(FilterIngredientRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new FilterIngredientResponse { Code = validate.Code, Message = validate.Message };
            }

            var IngredientFilter = db.Ingredients.Where(x => x.Name.Contains(request.Name));
            if(request.notIncluded != null)
            {
                IngredientFilter = IngredientFilter.Where(x => !request.notIncluded.Any(y => y == x.Id));
            }

            return new FilterIngredientResponse
            {
                Ingredients = _mapper.Map<List<IdIngredient>>(await IngredientFilter.OrderBy(o => o.Name).Take(10).ToListAsync())
            };
        }

        public async Task<ValidateResult> Validate(FilterIngredientRequest request)
        {
            return new ValidateResult();
        }
    }
}
