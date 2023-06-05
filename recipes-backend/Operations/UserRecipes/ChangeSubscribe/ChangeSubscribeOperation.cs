using AutoMapper;
using recipes_backend.Operations.Recipe.Rate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.changeFavorite;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.UserRecipes.ChangeSubscribe
{
    public class ChangeSubscribeOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeSubscribeOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChangeSubscribeResponse> Execute(ChangeSubscribeRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new ChangeSubscribeResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var sub = await db.Subscriptions.Where(x => x.UserId == user.Id && x.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                var author = await db.Users.Where(x => x.Id == request.AuthorId).FirstOrDefaultAsync();

                if (sub != null)
                {
                    db.Subscriptions.Remove(sub);
                    author.AmountOfSubscribers--;
                }
                else
                {
                    db.Subscriptions.Add(new Subscription { UserId = user.Id, AuthorId = author.Id });
                    author.AmountOfSubscribers = author.AmountOfSubscribers + 1;
                }
            }
            else
            {
                return new ChangeSubscribeResponse() { Code = 401, Message = "User not found" };
            }
            await db.SaveChangesAsync();
            return new ChangeSubscribeResponse();
        }

        public async Task<ValidateResult> Validate(ChangeSubscribeRequest request)
        {
            return new ValidateResult();
        }
    }
}
