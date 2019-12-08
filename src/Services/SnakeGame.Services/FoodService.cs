using System;
using System.Collections.Generic;
using System.Linq;
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

        public FoodModel GenerateFood(RoomModel room)
        {
            lock (room)
            {

                if (!CanGenerate(room))
                    return null;

                var food = new FoodGenerator(_gameData.Configurations).Generate(
                    _roomService.GetRandomAvailableColor(room));
                if (Exists(room, food))
                    return GenerateFood(room);

                room.Foods.Add(food);
                return food;
            }
        }

        private bool Exists(RoomModel room, FoodModel food)
        {
            lock (room)
            {
                return room.Foods.Any(p => p.Position.X == food.Position.X && p.Position.Y == food.Position.Y);
            }
            
        }

        private List<FoodModel> GetNearBy(RoomModel room, PositionModel position, int delta)
        {
            lock (room)
            {
                return  room.Foods.Where(p =>
                {
                    var xPositionCompare = CalculationsHelper.Distance(p.Position.X.Value, position.X.Value) <=
                                           delta;
                    var yPositionCompare = CalculationsHelper.Distance(p.Position.Y.Value, position.Y.Value) <=
                                           delta;
                    return xPositionCompare && yPositionCompare;
                }).ToList();
                ;
            }
        }


        private bool CanGenerate(RoomModel room)
        {
            lock (room)
            {
               return room.Foods.Count < _gameData.Configurations.RoomConfiguration.MaxFoods;
            }
            
        }

        public FoodModel Get(RoomModel room, SnakeModel snake)
        {
            lock (room)
            {
                var foods =  GetNearBy(room, snake.CurrentlyPosition, snake.HeadSize);
                if (!foods.Any())
                    return null;

                //var food = foods.FirstOrDefault(p=> CalculationsHelper.Distance(foods.))




                return foods.FirstOrDefault();
            }
        }
        public void RemoveFood(RoomModel room, FoodModel food)
        {
            lock (room)
            {
                try
                {
                    room.Foods.Remove(food);
                }
                catch (Exception e)
                {
                }

            }
        }
    }
}
