using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Configurations;
using SnakeGame.Infrastructure.Data.Models;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{

    public class GameService
    {
        private readonly GameConfigurations _configurations;
        private readonly RoomService _roomService;
        private readonly FoodService _foodService;
        private readonly SnakeService _snakeService;
        private readonly PlayerService _playerService;
        private IHubCallerClients _clients;
        private PlayerModel _player;
        private bool _isConfigured;
        private RoomModel _room;


        public GameService(
            GameConfigurations configurations,
            RoomService roomService,
            FoodService foodService,
            SnakeService snakeService,
            PlayerService playerService)
        {
            _configurations = configurations;
            _roomService = roomService;
            _foodService = foodService;
            _snakeService = snakeService;
            _playerService = playerService;
            _isConfigured = false;
            _player = null;
            _room = null;
        }

        public void Configure(IHubCallerClients clients,Guid roomGuid, string playerId)
        {
            _room = _roomService.Get(roomGuid);
            if(_room.IsNullOrEmpty()) throw new Exception("invalid_room");
            _player = _playerService.Get(_room,playerId);
            if(_player.IsNullOrEmpty()) throw new Exception("invalid_player");
            
            _clients = clients;
            if (!_clients.IsNullOrEmpty())
                _isConfigured = true;
        }


        public GameConfigurationsModel GetConfigurations()
        {
            return  new GameConfigurationsModel
            {
                Room = new GameConfigurationsModel.RoomConfiguirationModel
                {
                    Width = _configurations.RoomConfiguration.Width,
                    Height = _configurations.RoomConfiguration.Height,
                    BackgroundColor = _configurations.RoomConfiguration.BackgroundColor,
                    FrameRateInterval =  _configurations.GameFrameRateMilliSeconds,
                    Infos = _configurations.RoomConfiguration.Infos
                }
            };
            
        }

        public bool IsPlayerAlive()=> _isConfigured 
            ?_player.Alive
            : throw new Exception("Before anything, you MUST call Configure method");



        public void GameStatus()
        {
            try
            {

                if (IsPlayerAlive())
                {
                    _foodService.GenerateFood(_room);
                    var nextPosition = _snakeService.Move(_player.Snake, _player.Snake.Direction);
                    var foodColision = _foodService.Get(_room, nextPosition,_player.Snake.HeadSize);
                   
                    if (foodColision != null)
                    {
                        _snakeService.Add(_player.Snake,nextPosition,false); 
                        _foodService.RemoveFood(_room,foodColision);
                
                    }
                    else
                    {
                        _snakeService.Add(_player.Snake,nextPosition);
                    }
                    
                    
                }
                _clients.Clients(_roomService.GetConnectedClientsIds(_room)).SendCoreAsync("GameChanged", new object[] { new GameModel(_room, _roomService.GetScore(_room))});
                _clients.Caller.SendCoreAsync("PlayerStatus", new object[] {_player});
            }
            catch (Exception ex)
            {
                _clients.Caller.SendCoreAsync("BackendError", new object[] { ex });
            }
        }


    }
}
