using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Food.Models;
using SnakeGame.Domain.Player;
using SnakeGame.Domain.Player.Abstractions;
using SnakeGame.Domain.Room.Abstractions;
using SnakeGame.Infrastructure.Data.Interfaces;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Services.Room
{
    public class RoomHandler : BaseRoomHandler

    {
        private readonly SnakeGenerator _charGenerator;

        public RoomHandler(
            IGameData gameData, 
            SnakeGenerator charGenerator, 
            GameConfigurations configurations) : base(gameData.Room, configurations.RoomConfiguration)
        {
            _charGenerator = charGenerator;
        }


        public override IFood GenerateFood()
        {
            var color = GetRandomAvailableColor();
            var position = RandomHelper.RandomPosition(
                xMinValue: 0,
                xMaxValue: RoomConfigurations.Width,
                yMinValue: 0,
                yMaxValue: RoomConfigurations.Height,
                color: new ColorModel(color, color));
            var food =new FoodModel(Guid.NewGuid(), position);

            Room.Foods.Add(food);
            return food;
        }

        public override IPlayer CreatePlayer(string connectionId, string name)
        {
            var snake = GeneratePlayerChar();

            return new PlayerGenerator(connectionId, name, Room.Id)
                .New(new PlayerCharHandler(snake,RoomConfigurations.Width,RoomConfigurations.Height));

        } 
        private IChar GeneratePlayerChar()
        {
            var color = new ColorModel(
                backgroundColor: GetRandomAvailableColor(),
                borderColor: ColorHelper.ChangeColorLevel(RoomConfigurations.BackgroundColor, 0.5));


            return _charGenerator.Generate(
                color: color,
                xMaxValue: RoomConfigurations.Width,
                yMaxValue: RoomConfigurations.Height);
        }

        public override IReadOnlyList<IScore> GetScore()
        {

            var players = Room.Players.Take(RoomConfigurations.PlayersInScore)
                .OrderByDescending(p => p.Char.Length).ToList();
            return players.Select(player => player.Score).ToList();
        }




    }
}
