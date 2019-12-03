using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeGame.Infrastructure.Models
{
    public class Room
    {
        public Room(bool isAvailable = true)
        {
            Players = new List<PlayerModel>();
            Foods = new List<FoodModel>();
            IsAvailable = isAvailable;

        }
        public  bool IsAvailable { get; private set; }
        public Guid RoomGuid { get; set; }
        public DateTime DateCreated { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<FoodModel> Foods { get; set; }

        public void LockRoom()=>IsAvailable = false;
        public void DislockRoom()=>IsAvailable = true;
        public bool IsColorBeeingUsed(string color)=> AnySnakeWithColor( color) || AnyFoodWithColor(color);

        public bool AnySnakeWithColor(string color) => Players.Any(p => p.Snake.Color == color);
        public bool AnyFoodWithColor(string color) => Foods.Any(p => p.Color == color);
    }
}
