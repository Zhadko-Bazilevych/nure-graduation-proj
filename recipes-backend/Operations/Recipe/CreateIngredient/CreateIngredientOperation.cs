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

namespace recipes_backend.Operations.Recipe.CreateIngredient
{
    public class CreateIngredientOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateIngredientOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateIngredientResponse> Execute(CreateIngredientRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new CreateIngredientResponse { Code = validate.Code, Message = validate.Message };
            }
            var ingredient = new Ingredient { Name = request.Name };
            db.Ingredients.Add(ingredient);
            await db.SaveChangesAsync();
            return new CreateIngredientResponse { Id = ingredient.Id };
        }

        public async Task<ValidateResult> Validate(CreateIngredientRequest request)
        {
            return new ValidateResult();
        }
    }
}
