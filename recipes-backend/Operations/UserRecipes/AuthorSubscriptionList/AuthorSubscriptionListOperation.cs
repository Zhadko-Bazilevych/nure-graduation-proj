using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.UserRecipes.AuthorSubscriptionList
{
    public class AuthorSubscriptionListOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorSubscriptionListOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //type 1-Own 2-Favorite 3-SubscribedToAuthors
        public async Task<AuthorSubscriptionListResponse> Execute()
        {
            var validate = await Validate();
            if (validate.Code != 200)
            {
                return new AuthorSubscriptionListResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail ==  Email).FirstOrDefaultAsync();
                
                AuthorSubscriptionListResponse response = new AuthorSubscriptionListResponse();

                var AuthorList = await db.Users.Include(y => y.SubscriptionAuthors).Where(x => x.SubscriptionAuthors.Any(y => y.UserId == user.Id)).OrderByDescending(o=>o.Id).ToListAsync();
                response.Authors = _mapper.Map<List<AuthorShort>>(AuthorList);

                return response;
            }
            else
            {
                return new AuthorSubscriptionListResponse { Code = 401, Message = "User not found" };
            }
        }

        public async Task<ValidateResult> Validate()
        {
            return new ValidateResult();
        }
    }
}
