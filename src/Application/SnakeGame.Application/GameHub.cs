using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Application.Entities;
using SnakeGame.Application.Handlers;
using SnakeGame.Application.Services;
using SnakeGame.Domain.Player;
using SnakeGame.Domain.Player.Models;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Application
{
    public partial class GameHub : Hub
    {
        private readonly GameHandler _gameHandler;
        private readonly GameConfigurations _configurations;


        public GameHub(
            GameHandler gameHandler,
            GameConfigurations configurations

  )
        {
            _gameHandler = gameHandler;
            _configurations = configurations;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendCoreAsync("RoomConfigurations", new object[] { GetConfigurations() });
            return base.OnConnectedAsync();

        }
        public GameConfigurationsModel GetConfigurations()
        {
            return new GameConfigurationsModel
            {
                Room = new GameConfigurationsModel.RoomConfiguirationModel
                {
                    Width = _configurations.RoomConfiguration.Width,
                    Height = _configurations.RoomConfiguration.Height,
                    BackgroundColor = _configurations.RoomConfiguration.BackgroundColor,
                    FrameRateInterval = _configurations.GameFrameRateMilliSeconds,
                    Infos = _configurations.RoomConfiguration.Infos
                }
            };

        }


        public GameModel GameStatus(Guid roomId, Guid playerGuid)
        {
            var roomHandler = _gameHandler.GetRoomHandler(roomId);
            var player = roomHandler.GetPlayer(playerGuid);
            try
            {

                if (player.Alive)
                {
                    roomHandler.GenerateFood();

                    //var nextPosition = player.Char.Move(player.Char.Model.Direction);
                    //var foodColision = _foodService.Get(_room, nextPosition, _player.Char.Size);

                    //if (foodColision != null)
                    //{
                    //    player.Char.Add(player.Char.Model, nextPosition, false);
                    //    _foodService.RemoveFood(_room, foodColision);

                    //}
                    //else
                    //{
                    //    _snakeService.Add(_player.Char, nextPosition);
                    //}
                    //_clients.Clients(_roomService.GetConnectedClientsIds(_room)).SendCoreAsync("GameChanged", new object[] { });
                    //_clients.Caller.SendCoreAsync("PlayerStatus", new object[] { _player });
                }
            }
            catch (Exception ex)
            {
                //_clients.Caller.SendCoreAsync("BackendError", new object[] { ex });
            }

            return new GameModel(roomHandler.Room, roomHandler.GetScore());
        }


        public dynamic NewPlayer(string name, string roomId)
        {
            try
            {

                var player = _gameHandler.NewPlayer(Context.ConnectionId, name, roomId) as PlayerModel;
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
        public void DirectionChanged(Guid roomGuid, Guid playerGuid, DirectionsEnum newDirection)
        {
            try
            {

                var roomHandler = _gameHandler.GetRoomHandler(roomGuid);
                var player = roomHandler.GetPlayer(playerGuid);
                var charHandler = new PlayerCharHandler(player.Char,roomHandler.Width,roomHandler.Height);

                charHandler.ChangeDirection(newDirection);
            }
            catch (Exception)
            {


            }

            //_gameService.Configure(Clients,roomGuid,playerId);
            //_gameService.GameStatus();
        }

    }
}
