using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.UserRecipes.GetUserList
{
    public class GetUserListOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserListOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //type 2-Favorite 2-SubscribedToAuthors 3-Own  
        public async Task<GetUserListResponse> Execute(GetUserListRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new GetUserListResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail ==  Email).FirstOrDefaultAsync();
                GetUserListResponse response = new GetUserListResponse();

                var RecipesDb = db.Recipes.Include(y => y.RecipeImages).Include(y => y.User).ThenInclude(i => i.SubscriptionAuthors).Include(y => y.FavoriteRecipes);
                List<Models.Recipe> RecipeList;
                switch (request.listType)
                {
                    case 1:
                        RecipeList = await RecipesDb.Where(x => x.FavoriteRecipes.Any(a => a.UserId == user.Id)).OrderByDescending(o => o.Id).ToListAsync();
                        break;
                    case 2:
                        RecipeList = await RecipesDb.Where(x => x.User.SubscriptionAuthors.Any(x => x.UserId == user.Id)).OrderByDescending(o => o.Id).ToListAsync();
                        break;
                    default:
                        RecipeList = await RecipesDb.Where(x => x.UserId == (request.authorId == null ? user.Id : request.authorId)).OrderByDescending(o => o.Id).ToListAsync();
                        break;
                }
                response.Recipes = _mapper.Map<List<RecipeShort>>(RecipeList);
                return response;
            }
            else
            {
                return new GetUserListResponse { Code = 401, Message = "User not found" };
            }
        }

        public async Task<ValidateResult> Validate(GetUserListRequest request)
        {
            return new ValidateResult();
        }
    }
}
