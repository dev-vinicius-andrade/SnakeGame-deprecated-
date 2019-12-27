using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Domain.Room.Configurations
{
    public partial class RoomConfigurationModel
    {
        public int MaxRooms { get; set; }
        public int MaxFoods { get; set; }
        public int MaxPlayers { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string BackgroundColor { get; set; }
        public int PlayersInScore { get; set; }
        public InfosConfigurationModel Infos { get; set; }
    }
}
