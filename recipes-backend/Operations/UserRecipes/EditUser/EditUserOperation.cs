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

namespace recipes_backend.Operations.UserRecipes.EditUser
{
    public class EditUserOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        ImageService imageService;

        public EditUserOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ImageService imgService)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            imageService = imgService;
        }

        public async Task<EditUserResponse> Execute(EditUserRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new EditUserResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                if (request.AuthorId != user.Id) 
                    return new EditUserResponse { Code = 401, Message = "You have no access to this settings" };

                user.isPublicMail = request.IsPublicMail;
                user.Description = request.Description;
                if(request.Name != null && request.Name != "")
                {
                    user.Name = request.Name;
                }
                if (request.isImageChanging)
                {
                    if(request.Image != null)
                    {
                        user.Image = await imageService.SaveImage(request.Image, "Images", "Users");
                    }
                    else
                    {
                        if(user.Image != null)
                        {
                            imageService.DeleteImageByPath(user.Image);
                        }
                        user.Image = null;
                    }
                }
                await db.SaveChangesAsync();
                return new EditUserResponse();
            }
            else
            {
                return new EditUserResponse() { Code = 401, Message = "User not found" };
            }
        }

        public async Task<ValidateResult> Validate(EditUserRequest request)
        {
            return new ValidateResult();
        }
    }
}
