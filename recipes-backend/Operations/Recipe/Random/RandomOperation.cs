using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Operations.Recipe.Random;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.GetFilterData
{
    public class RandomOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RandomOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RandomResponse> Execute()
        {
            return new RandomResponse
            {
                Id = (await db.Recipes.OrderBy(o => Guid.NewGuid()).FirstAsync()).Id,
            };
        }

        public async Task<ValidateResult> Validate(RandomRequest request)
        {
            return new ValidateResult();
        }
    }
}
