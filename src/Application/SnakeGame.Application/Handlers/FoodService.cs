using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Application.Configurations;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Application.Handlers
{
    public class FoodService
    {
        //private readonly GameConfigurations _configurations;
        ////private readonly RoomService _roomService;

        //public FoodService(
        //    GameConfigurations configurations
        //    //,RoomService roomService
        //    )
        //{
        //    _configurations = configurations;
        //   // _roomService = roomService;
        //}



        //private bool Exists(RoomModel room, BaseFood food)
        //{
        //    lock (room)
        //    {
        //        return room.Foods.Any(p => p.Position.X == food.Position.X && p.Position.Y == food.Position.Y);
        //    }
            
        //}

        //private List<BaseFood> GetNearBy(RoomModel room, PositionModel position, int delta=0)
        //{
        //    lock (room)
        //    {
        //        return  room.Foods.Where(p =>
        //        {
        //            var xPositionCompare = CalculationsHelper.Distance(p.Position.X.Value, position.X.Value) <=
        //                                   delta;
        //            var yPositionCompare = CalculationsHelper.Distance(p.Position.Y.Value, position.Y.Value) <=
        //                                   delta;
        //            return xPositionCompare && yPositionCompare;
        //        }).ToList();
        //        ;
        //    }
        //}


        //private bool CanGenerate(RoomModel room)
        //{
        //    lock (room)
        //    {
        //       return room.Foods.Count < _configurations.RoomConfiguration.MaxFoods;
        //    }
            
        //}

        //public BaseFood Get(RoomModel room, PositionModel position, int delta=0)
        //{
        //    lock (room)
        //    {
        //        var foods =  GetNearBy(room, position, delta);
        //        if (!foods.Any())
        //            return null;

        //        //var food = foods.FirstOrDefault(p=> CalculationsHelper.Distance(foods.))




        //        return foods.FirstOrDefault();
        //    }
        //}
        //public void RemoveFood(RoomModel room, BaseFood food)
        //{
        //    lock (room)
        //    {
        //        try
        //        {
        //            room.Foods.Remove(food);
        //        }
        //        catch (Exception )
        //        {
        //        }

        //    }
        //}
    }
}
