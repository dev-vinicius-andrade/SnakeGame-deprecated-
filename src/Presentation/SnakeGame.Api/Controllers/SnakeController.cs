using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using SnakeGame.Application;
using SnakeGame.Application.Handlers;
using SnakeGame.Domain.Admin;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SnakeController : Controller
    {
        private readonly SnakeService _snakeService;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly UserManagement _userManagement;

        public SnakeController(IHubContext<GameHub> hubContext,
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

            //var result = _snakeService.ChangeSpeedConfiguration(value);

            //return new JsonResult(result){StatusCode = result.Code};
            return null;
        }
        [HttpPost("changeInitialSize")]
        public JsonResult ChangeInitialSize(int value)
        {
            if(!_userManagement.IsUserConnected())
                return new JsonResult(ResponseHelper.DefaultUnauthorized);

            //var result = _snakeService.ChangeInitialSize(value);
            //return new JsonResult(result){StatusCode = result.Code};
            return null;
        }


    }
}