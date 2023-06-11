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

namespace recipes_backend.Operations.Recipe.GetInitialComments
{
    public class GetInitialCommentsOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetInitialCommentsOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetInitialCommentsResponse> Execute(GetInitialCommentsRequest request)
        {
            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            User? user = null;
            if (Email != null)
            {
                user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
            }

            var comments = await db.Comments.Where(x => x.RecipeId == request.Id && x.ParentCommentId == null).Include(x => x.InverseParentComment).Include(x => x.User).OrderByDescending(x=>x.DateCreated).ToListAsync();
            var collectedComments = _mapper.Map<List<CollectedComment>>(comments);
            if (user != null)
            {
                foreach (var comment in collectedComments)
                {
                    if (comment.UserId == user.Id)
                    {
                        comment.isAuthor = true;
                    }
                }
            }

            return new GetInitialCommentsResponse
            {
                Comments = _mapper.Map<List<CollectedComment>>(comments)
            };
        }

        public async Task<ValidateResult> Validate(GetInitialCommentsRequest request)
        {
            return new ValidateResult();
        }
    }
}
