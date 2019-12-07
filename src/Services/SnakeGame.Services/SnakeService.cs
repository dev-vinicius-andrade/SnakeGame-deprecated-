using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class SnakeService
    {
        private readonly GameData _gameData;
        private readonly FoodService _foodService;

        public SnakeService(GameData gameData, FoodService foodService)
        {
            _gameData = gameData;
            _foodService = foodService;
        }
        public SnakeModel Create(string color, string borderColor)
        {
            return new SnakeGenerator(_gameData.Configurations)
                .Generate(color, borderColor);
        }


        public SnakeMovementTracker Move(SnakeModel snake, PositionModel direction)
        {
            lock (snake)
            {
                var currentlyPosition = snake.CurrentlyPosition;
                return new SnakeMovementTracker(snake)
                    .TrackMovement(BoundaryReachPositionRecalculator(
                         position: new PositionModel
                         {
                             X = currentlyPosition.X + GetDirectionAxisSpeed(direction.X.Value),
                             Y = currentlyPosition.Y + GetDirectionAxisSpeed(direction.Y.Value),
                             Angle = direction.Angle
                         },
                          direction: snake.Direction));
            }

        }

        private PositionModel BoundaryReachPositionRecalculator(PositionModel position, PositionModel direction)
        {

            //y = 1 baixo
            //y = -1 cima

            var recalculatedPosition = position.Clone();
            if (position.X >= _gameData.Configurations.RoomConfiguration.Width&&direction.X==1)
                recalculatedPosition.X = 0;
            if (position.X <= 0 && direction.X == -1)
                recalculatedPosition.X = _gameData.Configurations.RoomConfiguration.Width;
            if (position.Y <= 0 && direction.Y ==-1)
                recalculatedPosition.Y = _gameData.Configurations.RoomConfiguration.Height;
            if (position.Y >= _gameData.Configurations.RoomConfiguration.Height && direction.Y == 1)
                recalculatedPosition.Y = 0;
            return recalculatedPosition;
        }
        private int GetDirectionAxisSpeed(int axisValue) => axisValue * (_gameData.Configurations.SnakeConfiguration.Speed + _gameData.Configurations.SnakeConfiguration.HeadSize);

        public ResponseModel ChangeSpeed(int value)
        {
            lock (_gameData.Configurations)
            {
                if (value == _gameData.Configurations.SnakeConfiguration.Speed)
                    return ResponseHelper.CreateBadRequest("Speed is already at this value");
                _gameData.Configurations.SnakeConfiguration.Speed = value;
                return ResponseHelper.CreateOk("Speed Changed");
            }
        }

        public ResponseModel ChangeInitialSize(int value)
        {
            lock (_gameData.Configurations)
            {
                if (value == _gameData.Configurations.SnakeConfiguration.InitialSnakeSize)
                    return ResponseHelper.CreateBadRequest("Initial Size is already at this value");
                _gameData.Configurations.SnakeConfiguration.InitialSnakeSize = value;
                return ResponseHelper.CreateOk("Initial Size Changed");
            }
        }

        public void Add(FoodModel foodColision, SnakeModel snake)
        {
            snake.Path.Add(new PositionModel
            {

                X = foodColision.Position.X,
                Y = foodColision.Position.Y,
                Angle = 0

            }) ;
        }

        public bool DirectionChanged(SnakeModel snake, PositionModel newDirection)
        {
            var directionChanged = DirectionChanged(snake.Direction, newDirection);
            if (directionChanged)
                snake.Direction = new PositionModel
                {
                    X = newDirection.X,
                    Y = newDirection.Y,
                    Angle = newDirection.Angle
                };

            return directionChanged;
        }

        public bool DirectionChanged(PositionModel currentlyDirection, PositionModel newDirection)
        {
            if (currentlyDirection.X == 0 && newDirection.X != 0)
                return true;

            if (currentlyDirection.Y == 0 && newDirection.Y != 0)
                return true;

            return false;
        }


    }
}
