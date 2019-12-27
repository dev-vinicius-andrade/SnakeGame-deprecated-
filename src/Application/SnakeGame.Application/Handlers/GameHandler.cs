using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Player;
using SnakeGame.Domain.Room.Interfaces;
using SnakeGame.Domain.Room.Models;
using SnakeGame.Infrastructure.Data.Extensions;
using SnakeGame.Infrastructure.Data.Interfaces;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Room;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Application.Handlers
{
    public class GameHandler
    {

        private readonly GameConfigurations _configurations;
        private readonly SnakeGenerator _charGenerator;
        private readonly IGameContext _gameContext;

        public GameHandler(IGameContext gameContext, GameConfigurations configurations, SnakeGenerator charGenerator)
        {
            _configurations = configurations;
            _charGenerator = charGenerator;
            _gameContext = gameContext;
        }
        public bool MaxRoomsReached() => _gameContext.Count >= _configurations.RoomConfiguration.MaxRooms;
        public IReadOnlyList<IRoomHandler> AvailableRooms()
            => _gameContext.Where(gamedata => gamedata.Room.IsAvailable)
                .Select(gameData => GetRoomHandler(gameData.Room.Id))
                .ToList();
        public IRoomHandler NewRoomHandler()
        {
            if (MaxRoomsReached())
                return null;
            var configuration = _configurations.RoomConfiguration;
            var roomColor = new ColorModel(configuration.BackgroundColor, configuration.BackgroundColor);
            var room = new RoomModel(roomColor) { Id = Guid.NewGuid(), DateCreated = DateTime.UtcNow };

            return _gameContext.Add(new GameDataModel(room)) 
                ? GetRoomHandler(room.Id) 
                : null;
        }
        


        public void RemoveRoom(Guid roomGuid) => _gameContext.Remove(roomGuid);

        public IRoomHandler GetRoomHandler(Guid roomGuid)
        {
            var gameData = _gameContext.Get(roomGuid);
            if (gameData.IsNullOrEmpty())
               return null;
            return new RoomHandler(gameData, _charGenerator, _configurations);
        }
        public void RemovePlayer(Guid roomId, Guid playerGuid)
        {
            var room = GetRoomHandler(roomId).Room;

                var player = room.Players.FirstOrDefault(p => p.Id == playerGuid);

                if (!player.IsNullOrEmpty())
                    room.Players.Remove(player);
                if (!room.Players.Any())
                    RemoveRoom(roomId);
            
        }
        public IPlayer NewPlayer(string connectionId, string name, string roomId)
        {

                var roomHandler = roomId.IsNullOrEmpty()
                    ? AvailableRooms().OrderBy(handler => handler.Room.DateCreated).FirstOrDefault()
                    : GetRoomHandler(roomId.ToGuid());

                if (roomHandler.IsNullOrEmpty())
                    roomHandler = NewRoomHandler();


                if (!roomHandler.IsRoomAvailable())
                    return null;

                var playerModel = roomHandler.CreatePlayer(connectionId, name);
                roomHandler.AddPlayer(playerModel);
                return playerModel;

        }


        public bool IsPlayerAlive(Guid roomId, Guid playerGuid)
        {

            var roomHandler = GetRoomHandler(roomId);
            if (roomHandler.IsNullOrEmpty())
                return false;

            var player = roomHandler.GetPlayer(playerGuid);
            if (player.IsNullOrEmpty())
                return false;

            return player.Alive;
        }
    }
}
