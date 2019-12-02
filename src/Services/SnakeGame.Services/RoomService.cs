using System;
using System.Collections.Generic;
using System.Linq;
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
            var room = new Room{RoomGuid = Guid.NewGuid()};
            _gameData.Rooms.Add(room);
            return room;
        }

        public void RemoveRoom(Room room)
        {
            _gameData.Rooms.Remove(room);
        }

        public List<Room> AvailableRooms()=>_gameData.Rooms.Where(IsRoomAvailable).ToList();
        public bool IsRoomAvailable(Room room)=> room.Players.Count < _gameData.Configurations.MaxPlayers;
    }
}
