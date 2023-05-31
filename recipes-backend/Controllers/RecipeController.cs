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
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Services;
using recipes_backend.Services.GoogleOAuthServiceModels;

namespace recipes_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public RecipeController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> RecipeInfo(int id)
        {
            var operation = _serviceProvider.GetRequiredService<RecipeInfoOperation>();
            var result = await operation.Execute(id);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("Rate")]
        public async Task<IActionResult> Rate(RateRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<RateOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("changeFavorite")]
        public async Task<IActionResult> changeFavorite(changeFavoriteRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<changeFavoriteOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> Filter(FilterRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<FilterOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetFilterData")]
        public async Task<IActionResult> GetFilterData()
        {
            var operation = _serviceProvider.GetRequiredService<GetFilterDataOperation>();
            var result = await operation.Execute();
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpPost("FilterIngredient")]
        public async Task<IActionResult> FilterIngredient(FilterIngredientRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<FilterIngredientOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }
    }
}