using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Room.Models
{
    public class RoomModel:IRoom

    {
        public RoomModel(IColor color, bool isAvailable = true,bool connectOnlyWithGuid=false)
        {
            Players = new List<IPlayer>();
            Foods = new List<IFood>();
            IsAvailable = isAvailable;
            Color = color;
            ConnectOnlyWithId = connectOnlyWithGuid;
        }
        public Guid Id { get; set; }
        public IColor Color { get; }
        public  bool IsAvailable { get; private set; }
        public  bool ConnectOnlyWithId { get; private set; }
        
        public DateTime DateCreated { get; set; }
        public  IList<IPlayer> Players { get;}
        public  IList<IFood> Foods { get;}
    }
}
