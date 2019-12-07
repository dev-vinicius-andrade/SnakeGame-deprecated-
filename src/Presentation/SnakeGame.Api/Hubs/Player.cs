using System;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class Player:Hub
    {
        private readonly PlayerService _playerService;
        private readonly RoomService _roomService;

        public Player(RoomService roomService,PlayerService playerService)
        {
            _playerService = playerService;
            _roomService = roomService;
        }

        public PlayerModel New(string name, string roomId)
        {
            try
            {

                var player = _playerService.New(Context.ConnectionId, name, roomId);
                return player;

            }
            catch (Exception ex)
            {


            }

            return null;
        }


        public void Disconnect(Guid roomId)
        {
            try
            {
                _playerService.Disconnect(roomId, Context.ConnectionId);
            }
            catch (Exception e)
            {
            }
        }
    }
}
