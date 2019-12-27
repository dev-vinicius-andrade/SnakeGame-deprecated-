namespace SnakeGame.Application.Handlers
{
    public class SnakeService
    {
        //private readonly GameConfigurations _configurations;

        //public SnakeService(GameConfigurations configurations)
        //{
        //    _configurations = configurations;
        //}
        ////public BaseChar Create(ColorModel color)
        ////{
        ////    lock (_configurations)
        ////    {
        ////        return new SnakeGenerator(_configurations)
        ////            .Generate(color);
        ////    }
        ////}


        //public PositionModel Move(BaseChar playerChar, PositionModel direction)
        //{
        //    lock (playerChar)
        //    {
        //        return BoundaryReachPositionRecalculator(
        //            position: new PositionModel
        //            {
        //                X = playerChar.Position.X + GetDirectionAxisMovement(direction.X.Value),
        //                Y = playerChar.Position.Y + GetDirectionAxisMovement(direction.Y.Value)

        //            },
        //            direction: playerChar.Direction);
        //    }

        //}

        //private PositionModel BoundaryReachPositionRecalculator(PositionModel position, PositionModel direction)
        //{
        //    var recalculatedPosition = position.Clone();
        //    if (position.X >= _configurations.RoomConfiguration.Width && direction.X == 1)
        //        recalculatedPosition.X = 0;
        //    if (position.X <= 0 && direction.X == -1)
        //        recalculatedPosition.X = _configurations.RoomConfiguration.Width;
        //    if (position.Y <= 0 && direction.Y == -1)
        //        recalculatedPosition.Y = _configurations.RoomConfiguration.Height;
        //    if (position.Y >= _configurations.RoomConfiguration.Height && direction.Y == 1)
        //        recalculatedPosition.Y = 0;
        //    return recalculatedPosition;
        //}

        //private int GetDirectionAxisMovement(int axisValue) => axisValue * _configurations.SnakeConfiguration.HeadSize;

        //public ResponseModel ChangeSpeedConfiguration(int value)
        //{
        //    lock (_configurations)
        //    {
        //        if (value == _configurations.SnakeConfiguration.Speed)
        //            return ResponseHelper.CreateBadRequest("Speed is already at this value");
        //        _configurations.SnakeConfiguration.Speed = value;
        //        return ResponseHelper.CreateOk("Speed Changed");
        //    }
        //}

        //public ResponseModel ChangeInitialSize(int value)
        //{
        //    lock (_configurations)
        //    {
        //        if (value == _configurations.SnakeConfiguration.InitialSnakeSize)
        //            return ResponseHelper.CreateBadRequest("Initial Length is already at this value");
        //        _configurations.SnakeConfiguration.InitialSnakeSize = value;
        //        return ResponseHelper.CreateOk("Initial Length Changed");
        //    }
        //}

        //public void Add(IChar playerChar, IPosition position, bool removeLast = true)
        //{
        //    lock (_configurations)
        //    {
        //        playerChar.Position.Color = SnakeGenerator.GetBodyColor(playerChar.Color);
        //        playerChar.Path.Add(new PositionModel
        //        {
        //            Coordinate = new CoordinateModel
        //            {
        //                X = position.Coordinate.X,
        //                Y = position.Coordinate.Y
        //            },
        //            Color = playerChar.Color
        //        });
        //        if (removeLast)
        //            playerChar.Path.RemoveAt(0);



        //    }
        //}

        //public bool ChangeDirection(IChar playerChar, IDirection newDirection)
        //{
        //    if (!DirectionChanged(playerChar.Direction, newDirection))
        //        return false;


        //    //playerChar.Direction = new PositionModel
        //    //{
        //    //    X = newDirection.X,
        //    //    Y = newDirection.Y
        //    //};

        //    return true;
        //}

        //public bool DirectionChanged(IDirection currentlyDirection, IDirection newDirection)
        //{
        //    if (currentlyDirection.XSpeed == 0 && newDirection.XSpeed != 0)
        //        return true;

        //    if (currentlyDirection.YSpeed == 0 && newDirection.YSpeed != 0)
        //        return true;

        //    return false;
        //}


    }
}
