using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.UserRecipes.AuthorInfo
{
    public class AuthorInfoOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorInfoOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthorInfoResponse> Execute(AuthorInfoRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new AuthorInfoResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            int userId = 0;
            if (Email != null)
            {
                userId = (await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync()).Id;
            }

            var author = await db.Users.Where(x => x.Id == request.AuthorId).FirstOrDefaultAsync();
            if(author != null)
            {
                var response = new AuthorInfoResponse
                {
                    Author = _mapper.Map<AuthorInfo>(author)
                };
                if(userId == request.AuthorId)
                {
                    response.Author.IsMe = true;
                }
                else
                {
                    response.Author.IsMe = false;
                    response.Author.Mail = author.isPublicMail ? author.Mail : null;
                    response.Author.IsSubscribed = await db.Subscriptions.AnyAsync(x => x.AuthorId == author.Id && x.UserId == userId);

                }
                return response;
            }
            else
            {
                return new AuthorInfoResponse { Code = 404, Message = "Author not found" };
            }

        }

        public async Task<ValidateResult> Validate(AuthorInfoRequest request)
        {
            return new ValidateResult();
        }
    }
}
