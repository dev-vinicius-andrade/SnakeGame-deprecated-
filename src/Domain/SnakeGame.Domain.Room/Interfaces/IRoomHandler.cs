using System;
using System.Collections.Generic;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Room.Interfaces
{
    public interface IRoomHandler

    {
        int Width { get; }
        int Height { get; }
        IRoom Room { get; }
        IReadOnlyList<string> GetConnectedClientsIds();
        string GetRandomAvailableColor();
        IReadOnlyList<IScore> GetScore();
        bool IsRoomAvailable();
        IPlayer GetPlayer(Guid playerGuid);
        IFood GenerateFood();
        IPlayer CreatePlayer(string connectionId, string name);
        void AddPlayer(IPlayer playerModel);
        bool HasPositionBeeingUsed(IPosition position, int delta = 0);
        
        bool AnyCharInPosition(IPosition position,int delta=0);
        bool AnyFoodInPosition(IPosition position,int delta=0);
        bool IsColorBeeingUsed(string color);
        bool AnyCharWithColor(string color);
        bool AnyFoodWithColor(string color);
    }
}