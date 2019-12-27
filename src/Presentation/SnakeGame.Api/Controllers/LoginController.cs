using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using SnakeGame.Application;
using SnakeGame.Application.Services;

namespace SnakeGame.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly AdminService _adminService;

        public LoginController(IHubContext<GameHub> hubContext,
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