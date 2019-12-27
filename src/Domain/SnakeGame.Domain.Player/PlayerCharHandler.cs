using System.Collections.Generic;
using SnakeGame.Domain.Player.Abstractions;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Player
{
    public class PlayerCharHandler : BasePlayerCharHandler
    {

        public PlayerCharHandler(IChar playerChar, int xLimit,int yLimit) :base(playerChar, xLimit, yLimit){}


        protected override int GetDirectionAxisMovement(int axisValue,int movement=1) => axisValue * movement;
        protected override IPosition BoundaryReachPositionRecalculator(IPosition newPosition, IDirection direction, int xMaxValue, int yMaxValue)
        {
            var recalculatedPosition = newPosition.Clone();
            if (newPosition.Coordinate.X >= xMaxValue && direction.XSpeed == 1)
                recalculatedPosition.Coordinate.X = 0;
            if (newPosition.Coordinate.X <= 0 && direction.XSpeed == -1)
                recalculatedPosition.Coordinate.X = xMaxValue;
            if (newPosition.Coordinate.Y <= 0 && direction.YSpeed == -1)
                recalculatedPosition.Coordinate.Y = yMaxValue;
            if (newPosition.Coordinate.Y >= yMaxValue && direction.YSpeed == 1)
                recalculatedPosition.Coordinate.Y = 0;
            return recalculatedPosition;
        }

        public ResponseModel ChangeSpeedConfiguration(int value)
        {
            //lock (_configurations)
            //{
            //    if (value == _configurations.SnakeConfiguration.Speed)
            //        return ResponseHelper.CreateBadRequest("Speed is already at this value");
            //    _configurations.SnakeConfiguration.Speed = value;
            //    return ResponseHelper.CreateOk("Speed Changed");
            //}
            return null;
        }

        public ResponseModel ChangeInitialSize(int value)
        {
            //lock (_configurations)
            //{
            //    if (value == _configurations.SnakeConfiguration.InitialSnakeSize)
            //        return ResponseHelper.CreateBadRequest("Initial Size is already at this value");
            //    _configurations.SnakeConfiguration.InitialSnakeSize = value;
            //    return ResponseHelper.CreateOk("Initial Size Changed");
            //}
            return null;
        }





    }
}
