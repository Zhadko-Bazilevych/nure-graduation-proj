using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.PatternDelete;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.PatternDelete
{
    public class PatternDeleteOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatternDeleteOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PatternDeleteResponse> Execute(PatternDeleteRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new PatternDeleteResponse { Code = validate.Code, Message = validate.Message };
            }
            if(db.SearchPatterns.Any(x=>x.Id == request.id))
            {
                var pattern = await db.SearchPatterns.Where(x => x.Id == request.id).FirstOrDefaultAsync();
                db.SearchPatterns.Remove(pattern);
                db.SaveChanges();
                return new PatternDeleteResponse();
            }

            return new PatternDeleteResponse { Code = 404, Message="Pattern not found"};
        }

        public async Task<ValidateResult> Validate(PatternDeleteRequest request)
        {
            return new ValidateResult();
        }
    }
}
