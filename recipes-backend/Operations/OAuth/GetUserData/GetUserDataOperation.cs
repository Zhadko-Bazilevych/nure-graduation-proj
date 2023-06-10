using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.OAuth.Refresh;
using recipes_backend.Services;
using System.Security.Claims;

namespace recipes_backend.Operations.OAuth.GetUserData
{
    public class GetUserDataOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserDataOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetUserDataResponse> Execute()
        {
            var validate = await Validate();
            if (validate.Code != 200)
            {
                return new GetUserDataResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();

                return new GetUserDataResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Mail = user.Mail,
                    Photo = user.Image,
                };
            }
            else
            {
                return new GetUserDataResponse { Code = 401, Message = "User not authorized" };
            }
        }

        public async Task<ValidateResult> Validate()
        {
            return new ValidateResult();
        }
    }
}
