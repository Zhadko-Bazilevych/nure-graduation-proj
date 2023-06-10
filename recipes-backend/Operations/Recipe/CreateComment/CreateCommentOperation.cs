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

namespace recipes_backend.Operations.Recipe.CreateComment
{
    public class CreateCommentOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ImageService imageService;

        public CreateCommentOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ImageService imgService)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            imageService = imgService;
        }

        public async Task<CreateCommentResponse> Execute(CreateCommentRequest request)
        {
            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
            if (user != null)
            {
                Comment newComment = new Comment
                {
                    Content = request.Content,
                    Image = (request.Image == null ? null : await imageService.SaveImage(request.Image, "Images", "Comments")),
                    ParentCommentId = request.ParentCommentId,
                    DateCreated = DateTime.Now,
                    RecipeId = request.RecipeId,
                    UserId = user.Id
                };
                await db.Comments.AddAsync(newComment);
                await db.SaveChangesAsync();
                return new CreateCommentResponse { Id = newComment.Id, Image = newComment.Image };
            }
            else
            {
                return new CreateCommentResponse { Code = 401, Message = "User not found" };
            }
        }

        public async Task<ValidateResult> Validate(CreateCommentRequest request)
        {
            return new ValidateResult();
        }
    }
}
