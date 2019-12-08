using System;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public partial class Game
    {
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
