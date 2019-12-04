class SnakeController{
    canvasController;
    hub;
    constructor(hub,canvasController){
        this.canvasController = canvasController;
        this.hub = hub;
    }
    changeDirection(keyDownEvent,player){
        let directionController = new DirectionController(player.snake.speed);
        let directionFunction = directionController[keyDownEvent.code];
        if(directionFunction)
            this.hub.invoke("DirectionChanged",player.roomId, directionFunction()).catch((error)=>console.log(error));
    }
}