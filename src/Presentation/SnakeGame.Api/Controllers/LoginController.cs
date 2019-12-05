using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Api.Hubs;
using SnakeGame.Services;

namespace SnakeGame.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IHubContext<Game> _hubContext;
        private readonly AdminService _adminService;

        public LoginController(IHubContext<Game> hubContext,
            AdminService adminService)
        {
            _hubContext = hubContext;
            _adminService = adminService;
        }

        [HttpPost("login")]
        public JsonResult Login(string username, string password)
        {
            var responseModel = _adminService.Login(username, password);
            return new JsonResult(new {Message = responseModel.Message}){StatusCode = responseModel.Code};
        }
        [HttpPost("register")]
        public JsonResult Register(string username, string password)
        {
            var responseModel = _adminService.Register(username, password);
            return new JsonResult(new {Message = responseModel.Message}){StatusCode = responseModel.Code};
        }



    }
}