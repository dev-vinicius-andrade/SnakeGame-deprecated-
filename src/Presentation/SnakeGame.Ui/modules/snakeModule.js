import  DirectionModule from "./directionModule.js";
export default class SnakeModule{
    canvasModule;
    hub;
    constructor(hub,canvasController){
        this.canvasModule = canvasController;
        this.hub = hub;
    }
    changeDirection(keyDownEvent,player){
        let directionModule = new DirectionModule(player.snake.speed);
        let directionFunction = directionModule[keyDownEvent.code];
        if(directionFunction) {
            let direction = directionFunction();
            if(player.snake.direction!=direction)
                this.hub.invoke("DirectionChanged", player.roomId, player.id, directionFunction()).catch((error) => console.log(error));
        }
    }

    renderSnake(snake){
        if(snake!=null && snake!=undefined)
            this.canvasModule.drawPath(snake.path, snake.headSize);

    }
}