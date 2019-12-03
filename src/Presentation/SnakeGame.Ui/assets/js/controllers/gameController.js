class GameController {
    constructor(foodsController,playerController) {
        this.canvasController=null;
        this.foodsController = foodsController;
        this.playerController = playerController;
        this.snakeMovementIntervalTimeout = CONFIGURATIONS.SNAKE.DEFAULT_SNAKE_MOVEMENT_INTERVAL_TIMEOUT;
        this.fruitsGenerationIntervalTimeout = CONFIGURATIONS.FOOD.DEFAULT_FRUITS_GENERATION_INTERVAL_TIMEOUT;
        this.hub=null;
        this.roomId=null;
    }
    setHub(hub)
    {
        this.hub=hub;
        return this;
    }
    setCanvasController(controller)
    {
        this.canvasController = controller;
        return this;
    }
    setKeyDownEventHandler(eventHandler, controller,player){
        document.onkeydown = function (e) {
            eventHandler(e,controller,player);
        }
        return this;
    }
    setSnakeCurrentlyPositionOnChangeEventHandler(eventHandler)
    {
        this.playerController.player.snake.currentlyPosition.registerOnChangeHandler(eventHandler,this);
        return this;
    }
    async connectPlayer(playerName){
        let scope = this;
        this.roomId= await this.playerController.new(playerName);
    }

    async start(){

        debugger;
        this.setKeyDownEventHandler(DocumentEventHandler.keyDownEventHandler,this.playerController.snakeController,this.playerController.player);
            //.setSnakeCurrentlyPositionOnChangeEventHandler(this.SnakeCurrentlyPositionOnChangeEventHandler,this.playerController.snakeController);

       await this.generateFoods(this);
       await this.moveSnake(this);
       await this.monitorGame(this);

    };
    moveSnake(scope)
    {
        window.setInterval(()=>{
            this.playerController.snakeController.move(this.playerController.player,this.playerController.player.snake.direction);
        },this.snakeMovementIntervalTimeout);
    }

    async generateFoods(scope)
    {
        await this.foodsController.loadFoods(this.roomId);
        window.setInterval(()=> {
            scope.foodsController.generateFood(scope.roomId);
        },scope.fruitsGenerationIntervalTimeout);
    }
    async monitorGame(scope)
    {
        this.hub.on("OnGameMonitoring", async function (room) {
            debugger;
            scope.canvasController.initialize();
            await scope.foodsController.loadFoods(scope.roomId);
            await scope.playerController.loadAll(scope.roomId);

        })
    }



    SnakeCurrentlyPositionOnChangeEventHandler(position){
        debugger;
        let food = position.onChangeController.foodsController.get(position);
        if(food)
        {
            position.onChangeController.foodsController.remove(food);
            position.onChangeController.snakeController.add(food.position);
        }



    }

}