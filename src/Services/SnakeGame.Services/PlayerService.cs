using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Infrastructure.Models.Configurations;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
     public class PlayerService
    {
        private readonly GameConfigurations _configurations;
        private readonly RoomService _roomService;
        private readonly SnakeService _snakeService;

        public PlayerService(GameConfigurations configurations,RoomService roomService,SnakeService snakeService)
        {
            _configurations = configurations;
            _roomService = roomService;
            _snakeService = snakeService;
        }
        public PlayerModel New(string connectionId, string name, string roomId)
        {
            lock (_configurations)
            {

                var availableRoom = roomId.IsNullOrEmpty()
                                    ?_roomService.AvailableRooms().OrderBy(p => p.DateCreated).FirstOrDefault()
                                    :_roomService.Get(roomId.ToGuid());
                if (availableRoom.IsNull())
                    availableRoom = _roomService.New();

                if (!availableRoom.IsAvailable)
                    return null;

                var playerModel = new PlayerModel
                {
                    RoomId = availableRoom.RoomGuid,
                    Id = connectionId,
                    Name = name,
                    Score = new ScoreModel(),
                    Snake = _snakeService.Create(
                        color:_roomService.GetRandomAvailableColor(availableRoom),
                        borderColor: ColorHelper.ChangeColorLevel(_configurations.RoomConfiguration.BackgroundColor,0.5))
                };
                availableRoom.Players.Add(playerModel);
                return playerModel;
            }
        }


        public void Disconnect(Guid roomId,string playerId)
        {
                var room = _roomService.Get(roomId);
                lock (room)
                {
                    var player = room.Players.FirstOrDefault(p => p.Id == playerId);

                    if (!player.IsNull())
                        room.Players.Remove(player);
                    if (!room.Players.Any())
                        _roomService.RemoveRoom(room);
                }
        }

        public PlayerModel Get(RoomModel room, string playerId)
        {
            lock (room)
            {
                return room.Players.FirstOrDefault(p => p.Id == playerId);
            }
            
        }
    }
}
