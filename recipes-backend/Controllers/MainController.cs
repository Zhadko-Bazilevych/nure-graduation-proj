using Microsoft.AspNetCore.Mvc;
using recipes_backend.Models;

namespace recipes_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        recipesContext db;
        private readonly IServiceProvider _serviceProvider;

        public MainController(recipesContext db)
        {
            this.db = db;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(int request)
        {
            db.SaveChanges();
            return new JsonResult(request);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(int request)
        {
            return new JsonResult(request);
        }
    }
}
