using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SnakeGame.Infrastructure.Data.Models
{
    public class RoomModel
    {
        public RoomModel(bool isAvailable = true,bool connectOnlyWithGuid=false)
        {
            Players = new List<PlayerModel>();
            Foods = new List<FoodModel>();
            Score = new List<ScoreModel>();
            IsAvailable = isAvailable;
            ConnectOnlyWithGuid = connectOnlyWithGuid;
        }
        public Guid RoomGuid { get; set; }
        public  bool IsAvailable { get; private set; }
        public  bool ConnectOnlyWithGuid { get; private set; }
        
        public DateTime DateCreated { get; set; }
        public  List<PlayerModel> Players { get; set; }
        public  List<FoodModel> Foods { get; set; }
        public  List<ScoreModel> Score { get; set; }

        public void LockRoom()=>IsAvailable = false;
        public void DislockRoom()=>IsAvailable = true;
        public bool IsColorBeeingUsed(string color)=> AnySnakeWithColor( color) || AnyFoodWithColor(color);

        public bool AnySnakeWithColor(string color) => Players.Any(p => p.Snake.Color == color);
        public bool AnyFoodWithColor(string color) => Foods.Any(p => p.Color == color);
    }
}
