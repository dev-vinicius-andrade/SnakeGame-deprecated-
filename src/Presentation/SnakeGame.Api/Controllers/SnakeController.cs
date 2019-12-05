using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Api.Hubs;
using SnakeGame.Domain.Admin;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Services;

namespace SnakeGame.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SnakeController : Controller
    {
        private readonly SnakeService _snakeService;
        private readonly IHubContext<Game> _hubContext;
        private readonly UserManagement _userManagement;

        public SnakeController(IHubContext<Game> hubContext,
            UserManagement userManagement,
            SnakeService snakeService)
        {
            _hubContext = hubContext;
            _snakeService = snakeService;
            _userManagement = userManagement;
        }

        [HttpPost("changeSpeed")]
        public JsonResult ChangeSpeed(int value)
        {
            if(!_userManagement.IsUserConnected())
                return new JsonResult(ResponseHelper.DefaultUnauthorized);

            var result = _snakeService.ChangeSpeed(value);
            return new JsonResult(result){StatusCode = result.Code};
        }
        [HttpPost("changeInitialSize")]
        public JsonResult ChangeInitialSize(int value)
        {
            if(!_userManagement.IsUserConnected())
                return new JsonResult(ResponseHelper.DefaultUnauthorized);

            var result = _snakeService.ChangeInitialSize(value);
            return new JsonResult(result){StatusCode = result.Code};
        }


    }
}