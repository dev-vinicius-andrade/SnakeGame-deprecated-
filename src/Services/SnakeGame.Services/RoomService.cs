using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class RoomService
    {
        private readonly GameData _gameData;

        public  RoomService(GameData gameData)
        {
            _gameData = gameData;
        }

        public Room Get(Guid roomGuid)
        {
            var room = _gameData.Rooms.FirstOrDefault(p => p.RoomGuid == roomGuid);
            return room ?? throw new Exception($"Room does not exists!");
        }
        public Room New()
        {
            var room = new Room{RoomGuid = Guid.NewGuid(), DateCreated = DateTime.UtcNow};
            _gameData.Rooms.Add(room);
            return room;
        }

        public void RemoveRoom(Room room)
        {
            _gameData.Rooms.Remove(room);
        }

        public List<Room> AvailableRooms()=>_gameData.Rooms.Where(IsRoomAvailable).ToList();
        public bool IsRoomAvailable(Room room) => room.IsAvailable && (room.Players.Count < _gameData.Configurations.RoomConfiguration.MaxPlayers);

        public string GetRandomAvailableColor(Room room)
        {
            var color = RandomHelper.RandomColor();
            return room.IsColorBeeingUsed(color)
                ? GetRandomAvailableColor(room)
                : color;
        }


        public IReadOnlyList<string> GetConnectedClientsIds(Room room)
        {
            return room.Players.Select(p => p.Id).ToList();
        }
    }
}
