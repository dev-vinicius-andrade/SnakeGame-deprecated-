using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;
using SnakeGame.Services.Entities;

namespace SnakeGame.Api.Hubs
{
    public class PlayerHub:Hub
    {
        private readonly PlayerService _playerService;
        private readonly RoomService _roomService;

        public PlayerHub(PlayerService playerService,RoomService roomService)
        {
            _playerService = playerService;
            _roomService = roomService;
        }

        public PlayerModel New(string name)
        {
            try
            {
                var player = _playerService.New(Context.UserIdentifier, name);

                //Clients.AllExcept(player.Id).SendCoreAsync("PlayerJoined", new object[] {player});
                return player;
            }
            catch (Exception ex)
            {

                
            }

            return null;
        }

        public List<PlayerModel> GetEnemies(Guid roomGuid)
        {
            try
            {
                return _roomService.Get(roomGuid).Players.Where(p=>p.Id!=Context.UserIdentifier).ToList();
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public void Disconnect(PlayerModel player)
        {
            try
            {
                _playerService.Disconnect(player.RoomId,Context.UserIdentifier);
                var a = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
