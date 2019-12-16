using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Food;
using SnakeGame.Infrastructure.Configurations;
using SnakeGame.Infrastructure.Data.Models;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class FoodService
    {
        private readonly GameConfigurations _configurations;
        private readonly RoomService _roomService;

        public FoodService(GameConfigurations configurations,RoomService roomService)
        {
            _configurations = configurations;
            _roomService = roomService;
        }

        public FoodModel GenerateFood(RoomModel room)
        {
            lock (room)
            {

                if (!CanGenerate(room))
                    return null;

                var color = _roomService.GetRandomAvailableColor(room);
                var food = new FoodGenerator(_configurations).Generate(
                    color:color,
                    borderColor:color
                    );
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

        private List<FoodModel> GetNearBy(RoomModel room, PositionModel position, int delta=0)
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
               return room.Foods.Count < _configurations.RoomConfiguration.MaxFoods;
            }
            
        }

        public FoodModel Get(RoomModel room, PositionModel position, int delta=0)
        {
            lock (room)
            {
                var foods =  GetNearBy(room, position, delta);
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
                catch (Exception )
                {
                }

            }
        }
    }
}
