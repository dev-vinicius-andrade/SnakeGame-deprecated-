using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Infrastructure.Models
{
    public class Room
    {
        public Room()
        {
            Players = new List<PlayerModel>();
            Foods = new List<FoodModel>();
        }
        public Guid RoomGuid { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<FoodModel> Foods { get; set; }
    }
}
