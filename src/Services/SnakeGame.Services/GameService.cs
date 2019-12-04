using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Domain.Food;
using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{

    public class GameService
    {
        private readonly GameData _gameData;
        private readonly RoomService _roomService;
        private readonly FoodService _foodService;
        private readonly SnakeService _snakeService;
        private readonly PlayerService _playerService;
        private IHubCallerClients _clients;
        private PlayerModel _player;
        private bool _isConfigured;
        private Room _room;


        public GameService(
            GameData gameData,
            RoomService roomService,
            FoodService foodService,
            SnakeService snakeService,
            PlayerService playerService)
        {
            _gameData = gameData;
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
            _player = _playerService.Get(_room,playerId);
            _clients = clients;
            if (!_room.IsNull() && !_player.IsNull() && !_clients.IsNull())
                _isConfigured = true;
        }


        public ConfigurationsModel GetConfigurations()
        {
            return  new ConfigurationsModel
            {
                Room = new ConfigurationsModel.RoomConfiguirationModel
                {
                    Width = _gameData.Configurations.RoomConfiguration.Width,
                    Height = _gameData.Configurations.RoomConfiguration.Height,
                    BackgroundColor = _gameData.Configurations.RoomConfiguration.BackgroundColor
                }
            };
            
        }

        public void FrameRateDelay() => Task.Delay(_gameData.Configurations.GameFrameRateMilliSeconds);
        public bool IsPlayerAlive()=> _isConfigured 
            ?_player.Alive
            : throw new Exception("Before anything, you MUST call Configure method");


        public SnakeMovementTracker  MovePlayer()
        { 
            var movementTracker = _snakeService.Move(_player.Snake, _player.Snake.Direction);
            _clients.Caller.SendCoreAsync("SnakeMoved", new object[]{ _room});
            //_clients.Caller.SendCoreAsync("EnemyMoved", new object[] { movementTracker });
            return movementTracker;
        }

        public void Start()
        {
            try
            {

                if (IsPlayerAlive())
                {
                    MovePlayer();
                    //FrameRateDelay();
                }

            }
            catch (Exception ex)
            {
                _clients.Caller.SendCoreAsync("BackendError", new object[] { ex });
            }
        }
    }
}
