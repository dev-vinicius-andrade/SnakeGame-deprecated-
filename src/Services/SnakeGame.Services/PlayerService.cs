using System;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
     public class PlayerService
    {
        private readonly GameData _gameData;
        private readonly RoomService _roomService;
        private readonly SnakeService _snakeService;

        public PlayerService(GameData gameData,RoomService roomService,SnakeService snakeService)
        {
            _gameData = gameData;
            _roomService = roomService;
            _snakeService = snakeService;
        }
        public PlayerModel New(string connectionId, string name)
        {
            var availableRoom = _roomService.AvailableRooms().FirstOrDefault();
            if (availableRoom.IsNull())
                availableRoom = _roomService.New();

            if (!_roomService.IsRoomAvailable(availableRoom)) return New(connectionId, name);

            var playerModel = new PlayerModel
            {
                RoomId = availableRoom.RoomGuid,
                Id = connectionId,
                Name = name,
                Snake = _snakeService.Create()
            };
            availableRoom.Players.Add(playerModel);
            return playerModel;
        }

        public void Disconnect(Guid roomId,string playerId)
        {
            var room = _roomService.Get(roomId);
            var player = room.Players.FirstOrDefault(p => p.Id == playerId);

            if(!player.IsNull())
                room.Players.Remove(player);
            if (!room.Players.Any())
                _roomService.RemoveRoom(room);
        }
    }
}
