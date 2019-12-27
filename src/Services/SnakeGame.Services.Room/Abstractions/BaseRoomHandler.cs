using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Player.Configurations;
using SnakeGame.Domain.Room.Configurations;
using SnakeGame.Domain.Room.Helpers;
using SnakeGame.Domain.Room.Interfaces;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Services.Room.Abstractions
{
    public abstract class BaseRoomHandler<TChar,TFood>:IRoomHandler<TChar,TFood>
        where  TChar:IChar,IPositionObject
        where  TFood:BaseFood,IPositionObject
    {
     
        public  IRoom<TChar,TFood> Room;
        protected readonly RoomConfigurationModel RoomConfigurations;
        protected readonly SnakeConfigurationModel SnakeConfiguration;

        protected BaseRoomHandler(IRoom<TChar,TFood> room, RoomConfigurationModel roomConfigurations, SnakeConfigurationModel snakeConfiguration)
        {
            Room = room;
            RoomConfigurations = roomConfigurations;
            SnakeConfiguration = snakeConfiguration;
        }
        public abstract IReadOnlyList<ScoreModel> GetScore();
        
        public abstract TFood GenerateFood();
        public abstract PlayerModel CreatePlayer(string connectionId, string name);



        public virtual bool IsRoomAvailable() => Room.Model.IsAvailable
                                                 && (Room.Model.ConnectOnlyWithId == false)
                                                 && (Room.Model.Players.Count < RoomConfigurations.MaxPlayers);
        

        

        RoomModel<TChar, TFood> IRoom<TChar, TFood>.Model => throw new NotImplementedException();

        public void AddPlayer(PlayerModel<TChar> player) =>Room.Model.Players.Add(player);

        public  virtual bool HasPositionBeeingUsed(PositionModel position,int delta=0)
            => AnyCharInPosition(position) || AnyFoodInPosition(position);

        public bool AnyCharInPosition(PositionModel position, int delta = 0)
            => Room.Model.Players.Select(p => p.Char).ToList().GetNearBy(position, delta).Any();


        public bool AnyFoodInPosition(PositionModel position,int delta=0)
            =>Room.Model.Foods.GetNearBy(position,delta).Any();

        public IReadOnlyList<string> GetConnectedClientsIds()=>Room.Model.Players.Select(p => p.ConnectionId).ToList();

        public PlayerModel<TChar> GetPlayer(Guid playerGuid) =>
            Room.Model.Players.FirstOrDefault(player => player.PlayerGuid.Equals(playerGuid));

        public string GetRandomAvailableColor()
        {
            var color = RandomHelper.RandomColor();
            return IsColorBeeingUsed(color)
                ? GetRandomAvailableColor()
                : color;
        }

        public  bool IsColorBeeingUsed(string color) 
            =>AnyCharWithColor(color) || AnyFoodWithColor(color);
        public  bool AnyCharWithColor(string color) => Room.Model.Players.Any(p => p.Char.Model.Color.BackgroundColor == color);
        public  bool AnyFoodWithColor(string color) => Room.Model.Foods.Any(p => p.Position.Color.BackgroundColor == color);
    }
}
