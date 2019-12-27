using SnakeGame.Domain.Player.Abstractions;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Player
{
    public class PlayerCharHandler : BasePlayerCharHandler
    {

        public PlayerCharHandler(BaseChar playerChar, int xLimit,int yLimit):base(playerChar, xLimit, yLimit){}


        protected override int GetDirectionAxisMovement(int axisValue,int movement=1) => axisValue * movement;
        protected override PositionModel BoundaryReachPositionRecalculator(PositionModel newPosition, PositionModel direction, int xMaxValue, int yMaxValue)
        {
            var recalculatedPosition = newPosition.Clone();
            if (newPosition.X >= xMaxValue && direction.X == 1)
                recalculatedPosition.X = 0;
            if (newPosition.X <= 0 && direction.X == -1)
                recalculatedPosition.X = xMaxValue;
            if (newPosition.Y <= 0 && direction.Y == -1)
                recalculatedPosition.Y = yMaxValue;
            if (newPosition.Y >= yMaxValue && direction.Y == 1)
                recalculatedPosition.Y = 0;
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
