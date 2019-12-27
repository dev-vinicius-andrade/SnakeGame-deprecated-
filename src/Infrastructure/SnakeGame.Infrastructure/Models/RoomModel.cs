using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class RoomModel<TChar,TFood>
    where  TChar:IChar
    where  TFood:BaseFood,IPositionObject
    {
        public RoomModel(string backgroundColor, bool isAvailable = true,bool connectOnlyWithGuid=false)
        {
            Players = new List<PlayerModel<TChar>>();
            Foods = new List<TFood>();
            IsAvailable = isAvailable;
            BackgroundColor = backgroundColor;
            ConnectOnlyWithId = connectOnlyWithGuid;
        }
        public Guid Id { get; set; }
        public string BackgroundColor { get; }
        public  bool IsAvailable { get; private set; }
        public  bool ConnectOnlyWithId { get; private set; }
        
        public DateTime DateCreated { get; set; }
        public  IList<PlayerModel> Players { get;}
        public  IList<TFood> Foods { get;}

        public void LockRoom()=>IsAvailable = false;
        public void DislockRoom()=>IsAvailable = true;

    }
}
