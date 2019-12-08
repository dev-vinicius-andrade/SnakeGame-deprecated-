using System.Collections.Generic;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Services.Entities
{
    public class GameData
    {
        public GameConfigurationsModel Configurations { get; }

        public GameData(GameConfigurationsModel configurations)
        {
            Configurations = configurations;
            Rooms = new List<RoomModel>();
        }
        public List<RoomModel> Rooms { get; set; }
    }
}
