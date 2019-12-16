using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Configurations;
using SnakeGame.Infrastructure.Data;
using SnakeGame.Infrastructure.Data.Models;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class RoomService
    {
        private readonly GameConfigurations _configurations;
        private readonly GameContext _gameContext;

        public RoomService(GameConfigurations configurations, GameContext gameContext)
        {
            _configurations = configurations;
            _gameContext = gameContext;
        }

        //public  RoomService(GameConfigurations configurations,GameData gameData )
        //{
        //    _configurations = configurations;
        //    _gameData = gameData;
        //}
        public RoomModel Get(Guid roomGuid)
        {
            if (!_gameContext.Rooms.ContainsKey(roomGuid))
                return null;

            if (_gameContext.Rooms.TryGetValue(roomGuid, out var room))
                return room;

            return null;
        }
        public RoomModel New()
        {
            if (MaxRoomsReached())
                return null;

            var room = new RoomModel { RoomGuid = Guid.NewGuid(), DateCreated = DateTime.UtcNow };
            return _gameContext.Rooms.TryAdd(room.RoomGuid, room)
                ? room
                : null;
        }

        public bool MaxRoomsReached() => _gameContext.Rooms.Count >= _configurations.RoomConfiguration.MaxRooms;
        public void RemoveRoom(Guid roomGuid)
        {
            if (_gameContext.Rooms.ContainsKey(roomGuid))
                _gameContext.Rooms.TryRemove(roomGuid, out _);
        }

        public List<RoomModel> AvailableRooms()
        {
            return _gameContext.Rooms.Values.Where(IsRoomAvailable).ToList();
        }


        public bool IsRoomAvailable(RoomModel room)
        {
            return room.IsAvailable
                     && (room.ConnectOnlyWithGuid == false)
                     && (room.Players.Count < _configurations.RoomConfiguration.MaxPlayers);
        }

        public string GetRandomAvailableColor(RoomModel room)
        {
            var color = RandomHelper.RandomColor();
            return room.IsColorBeeingUsed(color)
                ? GetRandomAvailableColor(room)
                : color;
        }


        public IReadOnlyList<string> GetConnectedClientsIds(RoomModel room)
        {
            return room.Players.Select(p => p.ConnectionId).ToList();
        }

        public IReadOnlyList<ScoreModel> GetScore(RoomModel room)
        {

            var players = room.Players.Take(_configurations.RoomConfiguration.PlayersInScore)
                .OrderByDescending(p => p.Snake.Size).ToList();
            return players.Select(player => new ScoreModel
            {
                PlayerName = player.Name,
                SnakeColor = player.Snake.Color,
                Points = player.Snake.Path.Count - _configurations.SnakeConfiguration.InitialSnakeSize
            }).ToList();
        }
    }
}
