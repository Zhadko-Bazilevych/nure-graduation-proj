using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.GetFilterData
{
    public class GetFilterDataOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFilterDataOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetFilterDataResponse> Execute()
        {
            //var validate = await Validate(request);
            //if (validate.Code != 200)
            //{
            //    return new GetFilterDataResponse { Code = validate.Code, Message = validate.Message };
            //}

            return new GetFilterDataResponse
            {
                DishTypes = _mapper.Map<List<IdItem>>(await db.DishTypes.OrderBy(o=>o.Name).ToListAsync()),
                MenuTypes = _mapper.Map<List<IdItem>>(await db.MenuTypes.OrderBy(o => o.Name).ToListAsync()),
                FoodTypes = _mapper.Map<List<IdItem>>(await db.FoodTypes.OrderBy(o => o.Name).ToListAsync()),
            };
        }

        public async Task<ValidateResult> Validate(GetFilterDataRequest request)
        {
            return new ValidateResult();
        }
    }
}
