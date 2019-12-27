using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Domain.Player.Configurations
{
    public class SnakeConfigurationModel
    {
        public int RotationSpeed{get;set;}
        public int Speed{get;set;}
        public int HeadSize{get;set;}
        public int InitialSnakeSize { get; set; }
    }
}
