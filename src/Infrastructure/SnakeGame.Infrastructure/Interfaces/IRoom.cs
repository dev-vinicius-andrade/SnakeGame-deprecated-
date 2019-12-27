using System;
using System.Collections.Generic;
namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IRoom : ITrackable
    {
        IColor Color { get; }
        bool IsAvailable { get; }
        bool ConnectOnlyWithId { get; }
        DateTime DateCreated { get; }
        IList<IPlayer> Players { get; }
        IList<IFood> Foods { get; }

    }
}