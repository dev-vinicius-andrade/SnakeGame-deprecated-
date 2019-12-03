class SnakeController{
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
    drawSnakePath(snake){
        let scope = this;
        snake.path.forEach(function (position) {
            scope.draw(snake.color, position, snake.headRadius);
        });

    }
    draw(color,position,radius){
        this.canvasController.draw(color, position,radius);
    }
    move(player,position){
            switch (this.movementType) {
                case canvasDrawTypeEnum.RECTANGLE:
                    this.linearMovement(position,player);
                    break;
                case  canvasDrawTypeEnum.CIRCLE:
                    this.angularMovement(position,player);
                    break;
            }
    }
    add(position){
        this.player.snake.path.push(new Position(position.x,position.y));
        this.draw(this.player.snake.color,position);
    }
    linearMovement(position,player){
        player.snake.currentlyPosition.x+= (position.x*player.snake.speed);
        player.snake.currentlyPosition.y+=(position.y*player.snake.speed);
        //this.removeLastPathPosition(player);
        this.recalculateXPosition(player);
        this.recalculateYPosition(player);
        this.player.snake.path.push(new Position(this.player.snake.currentlyPosition.x,this.player.snake.currentlyPosition.y));
        this.registerMovement(player);
        this.drawSnakePath(player.snake);
    }
    registerMovement(player)
    {
        this.hub.invoke("Moved", player.roomId, player.id, player.snake.currentlyPosition).catch((error) => console.log(error));
    }
    angularMovement(position,player){
        player.snake.angle+=position.angle;
        let radian = Helper.DegreesToRadian(player.snake.angle);
        player.snake.currentlyPosition.x+= (player.snake.speed*Math.cos(radian));
        player.snake.currentlyPosition.y+=(player.snake.speed*Math.sin(radian));
        //this.removeLastPathPosition(player);
        this.recalculateXPosition(player);
        this.recalculateYPosition(player);
        this.player.snake.path.push(
            new Position(
                player.snake.currentlyPosition.x,
                player.snake.currentlyPosition.y,
                position.angle
            )
        );
        this.registerMovement(player)
        this.drawSnakePath(player.snake);
    }
    recalculateXPosition(player)
    {
        if(player.snake.currentlyPosition.x>= this.canvasController.canvasElement.width)
            player.snake.currentlyPosition.x=0;
        if(player.snake.currentlyPosition.x<0)
            player.snake.currentlyPosition.x=this.canvasController.canvasElement.width;
    }
    recalculateYPosition(player)
    {
        if(player.snake.currentlyPosition.y>= this.canvasController.canvasElement.height)
            player.snake.currentlyPosition.y=0;
        if(player.snake.currentlyPosition.y<0)
            player.snake.currentlyPosition.y=this.canvasController.canvasElement.height;
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
    removeLastPathPosition()
    {
        if(this.player.snake.path.length>1) {

           let position =  this.player.snake.path.shift();
           this.canvasController.clear(position, this.player.snake.headRadius);
        }
    }


}