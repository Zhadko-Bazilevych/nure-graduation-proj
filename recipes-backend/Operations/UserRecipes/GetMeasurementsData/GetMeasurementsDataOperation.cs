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

namespace recipes_backend.Operations.UserRecipe.GetMeasurementsData
{
    public class GetMeasurementsDataOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMeasurementsDataOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetMeasurementsDataResponse> Execute()
        {
            return new GetMeasurementsDataResponse
            {
                Measurements = _mapper.Map<List<IdItem>>(await db.Measurements.OrderBy(o=>o.Name).ToListAsync()),

            };
        }
    }
}
