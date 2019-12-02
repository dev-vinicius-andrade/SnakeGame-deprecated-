using System.Collections.Generic;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Services.Entities
{
    public class GameData
    {
        public GameConfigurationsModel Configurations { get; }

        public GameData(GameConfigurationsModel configurations)
        {
            Configurations = configurations;
            Rooms = new List<Room>();
        }
        public List<Room> Rooms { get; set; }
    }
}
