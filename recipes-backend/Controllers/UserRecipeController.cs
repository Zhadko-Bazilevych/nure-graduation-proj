using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using recipes_backend.Helpers;
using recipes_backend.Models;
using recipes_backend.Operations.OAuth.AuthByCode;
using recipes_backend.Operations.OAuth.Refresh;
using recipes_backend.Operations.Recipe.changeFavorite;
using recipes_backend.Operations.Recipe.Filter;
using recipes_backend.Operations.Recipe.FilterIngredient;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Operations.Recipe.PatternDelete;
using recipes_backend.Operations.Recipe.PatternList;
using recipes_backend.Operations.Recipe.PatternUpdate;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Operations.UserRecipe.GetMeasurementsData;
using recipes_backend.Operations.UserRecipes.AuthorInfo;
using recipes_backend.Operations.UserRecipes.AuthorSubscriptionList;
using recipes_backend.Operations.UserRecipes.ChangeSubscribe;
using recipes_backend.Operations.UserRecipes.EditUser;
using recipes_backend.Operations.UserRecipes.GetUserList;
using recipes_backend.Services;
using recipes_backend.Services.GoogleOAuthServiceModels;

namespace recipes_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRecipeController : ControllerBase
    {
        recipesContext db;
        private readonly IServiceProvider _serviceProvider;

        public UserRecipeController(recipesContext db, IServiceProvider serviceProvider)
        {
            this.db = db;
            _serviceProvider = serviceProvider;
        }

        [Authorize]
        [HttpGet("AuthorSubscriptionList")]
        public async Task<IActionResult> AuthorSubscriptionList()
        {
            var operation = _serviceProvider.GetRequiredService<AuthorSubscriptionListOperation>();
            var result = await operation.Execute();
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("ChangeSubscribe")]
        public async Task<IActionResult> ChangeSubscribe(ChangeSubscribeRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<ChangeSubscribeOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpPost("AuthorInfo")]
        public async Task<IActionResult> AuthorInfo(AuthorInfoRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<AuthorInfoOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpPost("GetUserList")]
        public async Task<IActionResult> GetUserList(GetUserListRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<GetUserListOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(EditUserRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<EditUserOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetMeasurementsData")]
        public async Task<IActionResult> GetMeasurementsData()
        {
            var operation = _serviceProvider.GetRequiredService<GetMeasurementsDataOperation>();
            var result = await operation.Execute();
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }
    }
}