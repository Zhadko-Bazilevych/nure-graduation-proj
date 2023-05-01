using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using recipes_backend.Helpers;
using recipes_backend.Models;
using recipes_backend.Operations.OAuth.AuthByCode;
using recipes_backend.Operations.OAuth.Refresh;
using recipes_backend.Services;
using recipes_backend.Services.GoogleOAuthServiceModels;

namespace recipes_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {

        private readonly IServiceProvider _serviceProvider;

        public OAuthController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("Redir")]
        public IActionResult RedirectOnOAuthServer()
        {
            GoogleOAuthService g = new GoogleOAuthService();
            var url = g.GenerateOAuthRequestUrl();
            return new JsonResult(url);
        }

        [HttpPost("AuthByCode")]
        public async Task<IActionResult> AuthByCode(AuthByCodeRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<AuthByCodeOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            var operation = _serviceProvider.GetRequiredService<RefreshOperation>();
            var result = await operation.Execute(request);
            if (result.Code != 200)
            {
                return StatusCode(result.Code, result.Message);
            }
            return new JsonResult(result);
        }
    }
}