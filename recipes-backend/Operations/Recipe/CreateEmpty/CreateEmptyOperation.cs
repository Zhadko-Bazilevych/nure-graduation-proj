using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.CreateEmpty
{
    public class CreateEmptyOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEmptyOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateEmptyResponse> Execute()
        {
            var validate = await Validate();
            if (validate.Code != 200)
            {
                return new CreateEmptyResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                Models.Recipe r = new Models.Recipe
                {
                    UserId = user.Id
                };

                db.Recipes.Add(r);
                await db.SaveChangesAsync();

                return new CreateEmptyResponse { Id = r.Id };
            }
            else
            {
                return new CreateEmptyResponse() { Code=401, Message="User not found" };
            }
        }

        public async Task<ValidateResult> Validate()
        {
            return new ValidateResult();
        }
    }
}
