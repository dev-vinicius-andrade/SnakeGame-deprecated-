using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Application.Configurations;
using SnakeGame.Domain.Player;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Domain.Room.Interfaces;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Data;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Room;
using SnakeGame.Services.Room.Abstractions;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Application.Handlers
{
    public class GameHandler
    {

        private readonly GameConfigurations _configurations;
        private readonly GameContext<IRoomHandler<IChar,BaseFood>> _gameContext;

        public GameHandler(GameConfigurations configurations, GameContext<IRoom<IChar,BaseFood>> gameContext)
        {
            _configurations = configurations;
            _gameContext = gameContext;
        }
        public bool MaxRoomsReached() => _gameContext.GameData.Count >= _configurations.RoomConfiguration.MaxRooms;
        public IRoomHandler<IChar,BaseFood> NewRoomHandler()
        {
            if (MaxRoomsReached())
                return null;


            var roomHandler = new RoomHandler<ICharHandler,BaseFood>(new RoomModel<ICharHandler,BaseFood>(_configurations.RoomConfiguration.BackgroundColor)
                { Id = Guid.NewGuid(), DateCreated = DateTime.UtcNow }, _configurations);
            return _gameContext.GameData.TryAdd(roomHandler.Get().Model.Id,roomHandler)
                ? roomHandler
                : null;
        }
        public IReadOnlyList<IRoomHandler<IChar,BaseFood>> AvailableRooms()
            => _gameContext.GameData.Values.Where(roomHandler => roomHandler.IsRoomAvailable())
            //.Select(roomHandler => roomHandler.Model())
            .ToList();


        public void RemoveRoom(Guid roomGuid)
        {
            if (_gameContext.GameData.ContainsKey(roomGuid))
                _gameContext.GameData.TryRemove(roomGuid, out _);
        }
        public IRoomHandler<IChar,BaseFood> GetRoomHandler(Guid roomGuid)
        {
            if (!_gameContext.GameData.ContainsKey(roomGuid))
                return null;

            if (_gameContext.GameData.TryGetValue(roomGuid, out var room))
                return room;

            return null;
        }
        public void RemovePlayer(Guid roomId, Guid playerGuid)
        {
            var room = GetRoomHandler(roomId).Model;
            lock (room)
            {
                var player = room.Players.FirstOrDefault(p => p.PlayerGuid == playerGuid);

                if (!player.IsNullOrEmpty())
                    room.Players.Remove(player);
                if (!room.Players.Any())
                    RemoveRoom(roomId);
            }
        }
        public PlayerModel NewPlayer(string connectionId, string name, string roomId)
        {
            lock (_configurations)
            {

                var roomHandler = roomId.IsNullOrEmpty()
                    ? AvailableRooms().OrderBy(handler =>  handler.Model.DateCreated).FirstOrDefault()
                    : GetRoomHandler(roomId.ToGuid());

                if (roomHandler.IsNullOrEmpty())
                    roomHandler = NewRoomHandler();


                if (!roomHandler.IsRoomAvailable())
                    return null;

                var playerModel = roomHandler.CreatePlayer(connectionId, name);
                roomHandler.AddPlayer(playerModel);
                return playerModel;
            }
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
