using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Domain.Food;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;
using SnakeGame.Services.Entities;

namespace SnakeGame.Api.Hubs
{
    public class GameHub:Hub
    {
        private readonly GameService _gameService;

        private readonly FoodService _foodService;
        private readonly RoomService _roomService;
        private readonly PlayerService _playerService;

        public GameHub(GameService gameService,FoodService foodService,RoomService roomService,PlayerService playerService)
        {
            _gameService = gameService;
            _foodService = foodService;
            _roomService = roomService;
            _playerService = playerService;
        }


        public ConfigurationsModel GetConfigurations()
        {
            return _gameService.GetConfigurations();
        }

        public  void Start(Guid roomId)
        {
            _gameService.Configure(Clients, roomId, Context.UserIdentifier);
            _gameService.Start();

        }

        public void GenerateFood(string roomGuid)
        {
            try
            {
                var food = _foodService.GenerateFood(_roomService.Get(roomGuid.ToGuid()));
                Clients.All.SendCoreAsync("FruitGenerated", new object[] { food });
            }
            catch (Exception ex)
            {


            }
        }
        public void DirectionChanged(Guid roomGuid, PositionModel newDirection)
        {
            var room = _roomService.Get(roomGuid);
            var player = _playerService.Get(room, Context.UserIdentifier);
            player.Snake.Direction = newDirection;
        }

        public List<FoodModel> GetAll(Guid roomGuid)
        {
            try
            {
                return _roomService.Get(roomGuid).Foods;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public PlayerModel New(string name)
        {
            try
            {
                var player = _playerService.New(Context.UserIdentifier, name);

                //Clients.AllExcept(player.Id).SendCoreAsync("PlayerJoined", new object[] {player});
                return player;
            }
            catch (Exception ex)
            {


            }

            return null;
        }

        public List<PlayerModel> GetEnemies(Guid roomGuid)
        {
            try
            {
                return _roomService.Get(roomGuid).Players.Where(p => p.Id != Context.UserIdentifier).ToList();
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public void Disconnect(PlayerModel player)
        {
            try
            {
                _playerService.Disconnect(player.RoomId, Context.UserIdentifier);
                var a = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
