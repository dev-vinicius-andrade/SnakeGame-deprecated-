using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Room.Configurations;
using SnakeGame.Domain.Room.Helpers;
using SnakeGame.Domain.Room.Interfaces;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Room.Abstractions
{
    public abstract class BaseRoomHandler:IRoomHandler

    {
        public int Width { get; }
        public int Height { get; }
        public IRoom Room { get; }
        protected readonly RoomConfigurationModel RoomConfigurations;

        protected BaseRoomHandler(IRoom room, RoomConfigurationModel roomConfigurations)
        {
            Room = room;
            
            Width = roomConfigurations.Width;
            Height = roomConfigurations.Height;
            RoomConfigurations = roomConfigurations;
            
        }
        public abstract IReadOnlyList<IScore> GetScore();
        
        public abstract IFood GenerateFood();
        public abstract IPlayer CreatePlayer(string connectionId, string name);



        public virtual bool IsRoomAvailable() => Room.IsAvailable
                                                 && (Room.ConnectOnlyWithId == false)
                                                 && (Room.Players.Count < RoomConfigurations.MaxPlayers);
        
        public void AddPlayer(IPlayer player) =>Room.Players.Add(player);

        public  virtual bool HasPositionBeeingUsed(IPosition position,int delta=0)
            => AnyCharInPosition(position,delta) || AnyFoodInPosition(position,delta);

        public bool AnyCharInPosition(IPosition position, int delta = 0)
            => Room.Players.Select(p => p.Char).ToList().GetNearBy(position, delta).Any();


        public bool AnyFoodInPosition(IPosition position,int delta=0)
            =>Room.Foods.GetNearBy(position,delta).Any();

        
        public IReadOnlyList<string> GetConnectedClientsIds()=>Room.Players.Select(p => p.ConnectionId).ToList();

        public IPlayer GetPlayer(Guid playerGuid) =>
            Room.Players.FirstOrDefault(player => player.Id.Equals(playerGuid));

        public string GetRandomAvailableColor()
        {
            var color = RandomHelper.RandomColor();
            return IsColorBeeingUsed(color)
                ? GetRandomAvailableColor()
                : color;
        }

        public  bool IsColorBeeingUsed(string color) 
            =>AnyCharWithColor(color) || AnyFoodWithColor(color);
        public  bool AnyCharWithColor(string color) => Room.Players.Any(p => p.Char.Color.Background == color);
        public  bool AnyFoodWithColor(string color) => Room.Foods.Any(p => p.Position.Color.Background == color);
    }
}
