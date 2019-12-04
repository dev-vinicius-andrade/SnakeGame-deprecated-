using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakeGame.Domain.Food;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class FoodService
    {
        private readonly GameData _gameData;
        private readonly RoomService _roomService;

        public FoodService(GameData gameData,RoomService roomService)
        {
            _gameData = gameData;
            _roomService = roomService;
        }

        public FoodModel GenerateFood(Room room)
        {
            lock (room)
            {

                if (!CanGenerate(room))
                    return null;

                var food = new FoodGenerator(_gameData.Configurations).Generate(
                    _roomService.GetRandomAvailableColor(room));
                if (Exists(room, food) || ExistsNearBy(room, food))
                    return GenerateFood(room);

                room.Foods.Add(food);
                return food;
            }
        }

        private bool Exists(Room room, FoodModel food)
        {
            lock (room)
            {
                return room.Foods.Any(p => p.Position.X == food.Position.X && p.Position.Y == food.Position.Y);
            }
            
        }

        private bool ExistsNearBy(Room room, FoodModel food)
        {
            lock (room)
            {
                var foodSize = _gameData.Configurations.FoodConfiguration.FoodSize;
                return room.Foods.Any(p =>
                {
                    var xPositionCompare = CalculationsHelper.Distance(p.Position.X.Value, food.Position.X.Value) <=
                                           foodSize;
                    var yPositionCompare = CalculationsHelper.Distance(p.Position.Y.Value, food.Position.Y.Value) <=
                                           foodSize;
                    return xPositionCompare && yPositionCompare;
                });
                ;
            }
        }


        private bool CanGenerate(Room room)
        {
            lock (room)
            {
               return room.Foods.Count < _gameData.Configurations.RoomConfiguration.MaxFoods;
            }
            
        }
    }
}
