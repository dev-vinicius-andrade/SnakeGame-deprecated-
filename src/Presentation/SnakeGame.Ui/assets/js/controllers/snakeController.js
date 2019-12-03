class SnakeController{
    canvasController;
    movementType;
    player;
    hub;
    constructor(){
        this.canvasController = null;
        this.movementType = CONFIGURATIONS.SNAKE.MOVEMENT_TYPE;
        this.player = null;
        //this.canvasController.objects.snakes.push(snake);
        this.hub = null;
    }
    setCanvasController(controller)
    {
        this.canvasController = controller;
        return this;
    }
    setHub(hub){
        this.hub=hub;
        return this;
    }
    setPlayer(player)
    {
        this.player = player;
        return this;
    }
    clearLastSnakePath(scope, path, snake)
    {
        let position = path.shift();
        scope.canvasController.clear(position, snake.headRadius);
        // path.forEach(function (position) {
        //     scope.canvasController.clear(position, snake.headRadius);
        // });
    }
    draw(color,position,radius){
        this.canvasController.draw(color, position,radius);
    }
    move(player,position){
        switch (this.movementType) {
            case canvasDrawTypeEnum.RECTANGLE:
                this.linearMovement(this,position,player);
                break;
            case  canvasDrawTypeEnum.CIRCLE:
                this.angularMovement(this,position,player);
                break;
        }
    }
    recalculateXPosition(position)
    {
        if(position.x>= this.canvasController.canvasElement.width)
            position.x=0;
        if(position.x<0)
            position.x=this.canvasController.canvasElement.width;
        return position;
    }
    recalculateYPosition(position)
    {
        if(position.y>= this.canvasController.canvasElement.height)
            position.y=0;
        if(position.y<0)
            position.y=this.canvasController.canvasElement.height;
        return position;
    }
    registerMovement(roomId, position)
    {
        this.hub.invoke("Moved", roomId, position).catch((error) => console.log(error));
    }
    linearMovement(scope,position,player){
        let currentlyPosition = player.snake.currentlyPosition;
        currentlyPosition.x+= (position.x*player.snake.speed);
        currentlyPosition.y+=(position.y*player.snake.speed);
        // player.snake.currentlyPosition.x+= (position.x*player.snake.speed);
        // player.snake.currentlyPosition.y+=(position.y*player.snake.speed);
        //this.removeLastPathPosition(player);
        currentlyPosition =this.recalculateXPosition(currentlyPosition);
        currentlyPosition = this.recalculateYPosition(currentlyPosition);
        //this.player.snake.path.push(new Position(player.snake.currentlyPosition.x,player.snake.currentlyPosition.y));
        this.registerMovement(player.roomId,currentlyPosition);
        this.trackSnakePath(scope,player);
    }
    angularMovement(scope,position,player){
        // player.snake.angle+=position.angle;
        // let radian = Helper.DegreesToRadian(player.snake.angle);
        // player.snake.currentlyPosition.x+= (player.snake.speed*Math.cos(radian));
        // player.snake.currentlyPosition.y+=(player.snake.speed*Math.sin(radian));
        //this.removeLastPathPosition(player);
        // this.recalculateXPosition(player);
        // this.recalculateYPosition(player);
        // this.player.snake.path.push(
        //     new Position(
        //         player.snake.currentlyPosition.x,
        //         player.snake.currentlyPosition.y,
        //         position.angle
        //     )
        // );
        // this.registerMovement(player)
        // this.drawSnakePath(scope,player.snake);
    }
    changeDirection(keyDownEvent,player){
        let directionController = new DirectionController(player.snake.speed);
        let directionFunction = directionController[keyDownEvent.code];
        if(directionFunction)
        {
            let directionValue = directionFunction();
            if((player.snake.direction.x!==directionValue.x && player.snake.direction.y!==directionValue.y) ||
                (this.movementType=== canvasDrawTypeEnum.CIRCLE && player.snake.direction.angle!==directionValue.angle))
            {
                player.snake.direction=directionValue;
                this.hub.invoke("DirectionChanged",player.roomId, player.id, directionValue).catch((error)=>console.log(error));
                this.move(player, player.snake.direction);
            }
        }
    }
    add(position){
        this.player.snake.path.push(new Position(position.x,position.y));
        this.draw(this.player.snake.color,position);
    }
    trackSnakePath(scope,player){
        // let scope = this;
        // snake.path.forEach(function (position) {
        //     scope.draw(snake.color, position, snake.headRadius);
        // });
        scope.hub.on("SnakeMoved", function (movementTracker) {
            player.snake = movementTracker.snake;
            scope.clearLastSnakePath(scope,movementTracker.beforeMovement,movementTracker.snake);
            scope.canvasController.drawPath(movementTracker.snake.color,movementTracker.snake.path)});
            // movementTracker.afterMovement.forEach(function (position) {
            //     scope.draw(movementTracker.snake.color, position, movementTracker.snake.headRadius);
            // });
    }
}