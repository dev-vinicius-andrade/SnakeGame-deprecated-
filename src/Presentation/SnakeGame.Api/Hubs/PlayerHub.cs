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

        public PlayerHub(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public PlayerModel New(string name)
        {
            try
            {
                var player = _playerService.New(Context.ConnectionId, name);
                //Clients.Caller.SendCoreAsync("NewFinished", new object[] {player});

                //Clients.AllExcept(player.Id).SendCoreAsync("PlayerJoined", new object[] {player});
                return player;
            }
            catch (Exception ex)
            {

                
            }

            return null;
        }

        public void Disconnect(PlayerModel player)
        {
            try
            {
                _playerService.Disconnect(player.RoomId,player.Id);
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
