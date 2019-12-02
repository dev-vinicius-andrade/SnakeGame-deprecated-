class GameController {
    constructor(foodsController,playerController) {
        this.foodsController = foodsController;
        this.playerController = playerController;
        this.snakeMovementIntervalTimeout = CONFIGURATIONS.SNAKE.DEFAULT_SNAKE_MOVEMENT_INTERVAL_TIMEOUT;
        this.fruitsGenerationIntervalTimeout = CONFIGURATIONS.FOOD.DEFAULT_FRUITS_GENERATION_INTERVAL_TIMEOUT;
        this.hub=null;
    }
    setHub(hub)
    {
        this.hub=hub;
        return this;
    }

    setKeyDownEventHandler(eventHandler, controller){
        document.onkeydown = function (e) {
            eventHandler(e,controller);
        }
        return this;
    }
    setSnakeCurrentlyPositionOnChangeEventHandler(eventHandler)
    {
        this.playerController.snakeController.snake.currentlyPosition.registerOnChangeHandler(eventHandler,this);
        return this;
    }
    async connectPlayer(playerName){
        let scope = this;
        await this.playerController.new(playerName);
    }

    async start(){

        debugger;
        this.setKeyDownEventHandler(DocumentEventHandler.keyDownEventHandler,this.playerController.snakeController);
            //.setSnakeCurrentlyPositionOnChangeEventHandler(this.SnakeCurrentlyPositionOnChangeEventHandler,this.playerController.snakeController);

       await this.generateFoods();
            //this.moveSnake();

    };
    moveSnake()
    {
        window.setInterval(()=>{
            this.playerController.snakeController.move(this.playerController.snakeController.snake.direction);
        },this.snakeMovementIntervalTimeout);
    }

    async generateFoods()
    {
        let scope = this;
        await this.foodsController.loadFoods(this.playerController.player.roomId)
        window.setInterval(()=> {
            scope.foodsController.generateFood(scope.playerController.player.roomId);
        },scope.fruitsGenerationIntervalTimeout);
    }


    SnakeCurrentlyPositionOnChangeEventHandler(position){

        let food = position.onChangeController.foodsController.get(position);
        if(food)
        {
            position.onChangeController.foodsController.remove(food);
            position.onChangeController.snakeController.add(food.position);
        }



    }

}