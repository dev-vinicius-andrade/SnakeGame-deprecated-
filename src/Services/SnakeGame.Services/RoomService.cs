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

        public RoomModel Get(Guid roomGuid)
        {
            lock (_gameData)
            {
                var room = _gameData.Rooms.FirstOrDefault(p => p.RoomGuid == roomGuid);
                return room;
            }
        }
        public RoomModel New()
        {
            lock (_gameData)
            {
                var room = new RoomModel { RoomGuid = Guid.NewGuid(), DateCreated = DateTime.UtcNow };
                _gameData.Rooms.Add(room);
                return room;
            }
        }

        public void RemoveRoom(RoomModel room)
        {
            lock (_gameData)
            {
                _gameData.Rooms.Remove(room);
            }
        }

        public List<RoomModel> AvailableRooms()
        {
            lock (_gameData)
            {
                return _gameData.Rooms.Where(IsRoomAvailable).ToList();
            }
        }



        public bool IsRoomAvailable(RoomModel room)
        {
            lock (room)
            {
                return   room.IsAvailable 
                         &&(room.ConnectOnlyWithGuid ==false)
                         &&(room.Players.Count < _gameData.Configurations.RoomConfiguration.MaxPlayers);
            }
        }

        public string GetRandomAvailableColor(RoomModel room)
        {
            lock (room)
            {
                var color = RandomHelper.RandomColor();
                return room.IsColorBeeingUsed(color)
                    ? GetRandomAvailableColor(room)
                    : color;
            }

        }


        public IReadOnlyList<string> GetConnectedClientsIds(RoomModel room)
        {
            lock (room)
            {
                return room.Players.Select(p => p.Id).ToList();
            }
    
        }
    }
}
