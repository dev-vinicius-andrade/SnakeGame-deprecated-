using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Application.Handlers;
using SnakeGame.Application.Services;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Application
{
    public partial class GameHub : Hub
    {
        private readonly GameService _gameService;
        private readonly GameHandler _gameHandler;

        private readonly SnakeService _snakeService;
        //private readonly RoomService _roomService;

        public GameHub(
            GameService gameService,
            GameHandler gameHandler,
            SnakeService snakeService
  //RoomService roomService,
  )
        {
            _gameService = gameService;
            _gameHandler = gameHandler;
            _snakeService = snakeService;
            //_roomService = roomService;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendCoreAsync("RoomConfigurations", new object[] { _gameService.GetConfigurations() });
            return base.OnConnectedAsync();
        }


        public void GameStatus(Guid roomId, Guid playerGuid)
        {
            try
            {
                //_gameService.Configure(Clients, roomId, playerId);
                if(_gameHandler.IsPlayerAlive(roomId,playerGuid))
                    _gameService.GameStatus(roomId, playerGuid);

            }
            catch (Exception)
            {


            }
        }

        public PlayerModel NewPlayer(string name, string roomId)
        {
            try
            {

                var player = _gameHandler.NewPlayer(Context.ConnectionId, name, roomId);
                return player;

            }
            catch (Exception)
            {


            }

            return null;
        }
        public void Disconnect(Guid roomGuid, Guid playerGuid)
        {
            try
            {
                _gameHandler.RemovePlayer(roomGuid, playerGuid);
            }
            catch (Exception)
            {
            }
        }
        public void DirectionChanged(Guid roomGuid, Guid playerGuid, PositionModel newDirection)
        {
            try
            {
                _gameHandler.GetRoomHandler(roomGuid)
                            .GetPlayer(playerGuid)
                            .Char.ChangeDirection(newDirection);
            }
            catch (Exception)
            {


            }

            //_gameService.Configure(Clients,roomGuid,playerId);
            //_gameService.GameStatus();
        }

    }
}
