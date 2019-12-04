using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Services.Entities
{
    public class ConfigurationsModel
    {
        public RoomConfiguirationModel Room { get; set; }

        public class RoomConfiguirationModel
        {
            public int Width { get; internal set; }
            public int Height { get; set; }
            public object BackgroundColor { get; set; }
        }
    }
}
