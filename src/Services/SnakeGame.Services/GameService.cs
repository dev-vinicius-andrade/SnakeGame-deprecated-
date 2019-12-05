using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
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
                    BackgroundColor = _gameData.Configurations.RoomConfiguration.BackgroundColor,
                    FrameRateInterval =  _gameData.Configurations.GameFrameRateMilliSeconds,
                    Infos = _gameData.Configurations.RoomConfiguration.Infos
                }
            };
            
        }

        public void FrameRateDelay() => Task.Delay(_gameData.Configurations.GameFrameRateMilliSeconds);
        public bool IsPlayerAlive()=> _isConfigured 
            ?_player.Alive
            : throw new Exception("Before anything, you MUST call Configure method");
        public void  MovePlayer()
        { 
            var movementTracker = _snakeService.Move(_player.Snake, _player.Snake.Direction);
            var foodColision = GetFoodColision(movementTracker.Snake);
            if (foodColision != null)
            {
                _snakeService.Add(foodColision,  movementTracker.Snake);
                _foodService.RemoveFood(_room,foodColision);
            }
            
            
        }

        private FoodModel GetFoodColision(SnakeModel snake)
        {
            return  _foodService.Get(_room, snake.CurrentlyPosition);
        }


        public void Start()
        {
            try
            {

                if (IsPlayerAlive())
                {
                    _foodService.GenerateFood(_room);
                    MovePlayer();
                    _clients.Caller.SendCoreAsync("SnakeMoved", new object[]{ new GameModel(_room) });
                }

            }
            catch (Exception ex)
            {
                _clients.Caller.SendCoreAsync("BackendError", new object[] { ex });
            }
        }

    }
}
