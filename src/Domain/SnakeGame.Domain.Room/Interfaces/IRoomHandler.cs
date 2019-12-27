using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Room.Interfaces
{
    public interface IRoomHandler<TChar,TFood>:IRoom<TChar,TFood>
        where  TChar:IChar
        where  TFood:BaseFood
    {

        IReadOnlyList<string> GetConnectedClientsIds();
        string GetRandomAvailableColor();
        IReadOnlyList<ScoreModel> GetScore();
        bool IsRoomAvailable();
        PlayerModel GetPlayer(Guid playerGuid);
        TFood GenerateFood();
        PlayerModel CreatePlayer(string connectionId, string name);
        void AddPlayer(PlayerModel playerModel);
        bool HasPositionBeeingUsed(PositionModel position,int delta=0)
            => AnyCharInPosition(position,delta) || AnyFoodInPosition(position,delta);
        
        bool AnyCharInPosition(PositionModel position,int delta=0);


        bool AnyFoodInPosition(PositionModel position,int delta=0);
        bool IsColorBeeingUsed(string color);
        bool AnyCharWithColor(string color);
        bool AnyFoodWithColor(string color);
    }
}