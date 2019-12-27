using System;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Application.Configurations;
using SnakeGame.Application.Entities;
using SnakeGame.Application.Handlers;

namespace SnakeGame.Application.Services
{

    public class GameService : Hub
    {
        private readonly GameConfigurations _configurations;
        private readonly SnakeService _snakeService;
        private readonly FoodService _foodService;
        private readonly GameHandler _gameHandler;
        public GameService(
            GameConfigurations configurations,
            GameHandler gameHandler,
            //RoomService roomService,
            FoodService foodService,
            SnakeService snakeService)
        {
            _configurations = configurations;
            _snakeService = snakeService;
            _gameHandler = gameHandler;
            _foodService = foodService;

        }

        //public void Configure(IHubCallerClients clients,Guid roomGuid, Guid playerGuid)
        //{
        //    _room = _ga.Model(roomGuid);
        //    if(_room.IsNullOrEmpty()) throw new Exception("invalid_room");
        //    _player = _playerService.Model(_room,playerGuid);
        //    if(_player.IsNullOrEmpty()) throw new Exception("invalid_player");

        //    _clients = clients;
        //    if (!_clients.IsNullOrEmpty())
        //        _isConfigured = true;
        //}


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
                roomHandler.GenerateFood();

                var nextPosition = player.Char.Move(player.Char.Model.Direction);
                var foodColision = _foodService.Get(_room, nextPosition, _player.Char.Size);

                if (foodColision != null)
                {
                    player.Char.Add(player.Char.Model, nextPosition, false);
                    _foodService.RemoveFood(_room, foodColision);

                }
                else
                {
                    _snakeService.Add(_player.Char, nextPosition);
                }
                //_clients.Clients(_roomService.GetConnectedClientsIds(_room)).SendCoreAsync("GameChanged", new object[] { });
                //_clients.Caller.SendCoreAsync("PlayerStatus", new object[] { _player });
            }
            catch (Exception ex)
            {
                //_clients.Caller.SendCoreAsync("BackendError", new object[] { ex });
            }

            return new GameModel(roomHandler.Model, roomHandler.GetScore());
        }


    }
}
