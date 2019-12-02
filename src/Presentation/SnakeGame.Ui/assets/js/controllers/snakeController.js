class PlayerController{
    constructor(snake,canvasController,hub){
        this.canvasController = canvasController;
        this.movementType = CONFIGURATIONS.SNAKE.MOVEMENT_TYPE;
        this.speed = CONFIGURATIONS.SNAKE.SPEED;
        this.snake = snake;
        this.canvasController.objects.snakes.push(snake);
        this.hub = hub;
    }


    drawSnakePath(){
        let scope = this;
        scope.removeLastPathPosition();
        this.snake.path.forEach(function (position) {
            scope.draw(scope.snake.color, position, scope.snake.headRadius);
        });


    }
    draw(color,position){
        this.canvasController.draw(color, position, this.snake.headRadius);
    }
    move(position){
            switch (this.movementType) {
                case canvasDrawTypeEnum.RECTANGLE:
                    this.linearMovement(position);
                    break;
                case  canvasDrawTypeEnum.CIRCLE:
                    this.angularMovement(position);
            }

    }
    add(position){
        this.snake.path.push(new Position(position.x,position.y));
        this.draw(this.snake.color,position);
    }
    linearMovement(position){
        this.snake.currentlyPosition.X+= (position.x*this.speed);
        this.snake.currentlyPosition.Y+=(position.y*this.speed);
        this.recalculateXPosition();
        this.recalculateYPosition();
        this.snake.path.push(new Position(this.snake.currentlyPosition.x,this.snake.currentlyPosition.y));
        this.drawSnakePath();
    }
    recalculateXPosition()
    {
        if(this.snake.currentlyPosition.x>= this.canvasController.canvasElement.width)
            this.snake.currentlyPosition.x=0;
        if(this.snake.currentlyPosition.x<0)
            this.snake.currentlyPosition.x=this.canvasController.canvasElement.width;
    }
    recalculateYPosition()
    {
        if(this.snake.currentlyPosition.y>= this.canvasController.canvasElement.height)
            this.snake.currentlyPosition.y=0;
        if(this.snake.currentlyPosition.y<0)
            this.snake.currentlyPosition.y=this.canvasController.canvasElement.height;
    }
    angularMovement(position){
        this.snake.angle+=position.angle;
        let radian = Helper.DegreesToRadian(this.snake.angle);
        this.snake.currentlyPosition.x+= (this.speed*Math.cos(radian));
        this.snake.currentlyPosition.y+=(this.speed*Math.sin(radian));
        this.recalculateXPosition();
        this.recalculateYPosition();
        this.snake.path.push(
            new Position(
                this.snake.currentlyPosition.x,
                this.snake.currentlyPosition.y,
                position.angle
            )
        );
        this.drawSnakePath();
    }
    changeDirection(keyDownEvent){
        let directionController = new DirectionController(this.speed);
        let directionFunction = directionController[keyDownEvent.code];
        if(directionFunction)
        {
            let directionValue = directionFunction();
            if((this.snake.direction.x!=directionValue.x && this.snake.direction.y!=directionValue.y) ||
            (this.movementType== canvasDrawTypeEnum.CIRCLE && this.snake.direction.angle!=directionValue.angle))
            {
                this.snake.direction=directionValue;
                this.move(this.snake.direction);
            }
        }
    }

    removeLastPathPosition()
    {
        if(this.snake.path.length>1) {

           let position =  this.snake.path.shift();
           this.canvasController.clear(position, this.snake.headRadius);
        }
    }


}