using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Player;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Room.Abstractions;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Services.Room
{
    public class RoomHandler<TChar,TFood> : BaseRoomHandler<TChar,TFood>
        where  TChar:ICharHandler
        where  TFood:BaseFood,IPositionObject
    {
        public RoomHandler(IRoom<TChar,TFood> room, GameConfigurations configurations) : base(room, configurations.RoomConfiguration,configurations.SnakeConfiguration)
        {
        }
        public IRoom<TChar,TFood> Get() => Room;

        public override TFood GenerateFood()
        {
            var color = GetRandomAvailableColor();
            var position = RandomHelper.RandomPosition(
                xMinValue: 0,
                xMaxValue: RoomConfigurations.Width,
                yMinValue: 0,
                yMaxValue: RoomConfigurations.Height,
                color: new ColorModel(color, color));
            var food =new FoodModel(Guid.NewGuid(), position) as TFood;

            Room.Model.Foods.Add(food);
            return food;
        }

        public override PlayerModel<IChar> CreatePlayer(string connectionId, string name)
        {
            var snake = GeneratePlayerChar();

            return new PlayerGenerator()
                .New<IChar>(connectionId, name, Room.Model.Id, new PlayerCharHandler(snake,RoomConfigurations.Width,RoomConfigurations.Height));

        } 
        private BaseChar GeneratePlayerChar()
        {
            var color = new ColorModel(
                backgroundColor: GetRandomAvailableColor(),
                borderColor: ColorHelper.ChangeColorLevel(RoomConfigurations.BackgroundColor, 0.5));


            return new SnakeGenerator(SnakeConfiguration).Generate(
                color: color,
                xMaxValue: RoomConfigurations.Width,
                yMaxValue: RoomConfigurations.Height);
        }

        public override IReadOnlyList<ScoreModel> GetScore()
        {

            var players = Room.Model.Players.Take(RoomConfigurations.PlayersInScore)
                .OrderByDescending(p => p.Char.Model.Length).ToList();
            return players.Select(player => new ScoreModel
            {
                PlayerName = player.Name,
                CharColor = player.Char.Model.Color.BackgroundColor,
                Points = player.Char.Model.Path.Count
            }).ToList();
        }




    }
}
