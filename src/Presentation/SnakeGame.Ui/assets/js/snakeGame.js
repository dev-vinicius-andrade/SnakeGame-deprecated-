"use strict";
window.onload = function() {
    let setupController = new SetupController();
    let directionController = new DirectionController();
    let positionController = new PositionController(setupController);


    this.JoinGame = function () {
        let snake = new Snake(positionController.RandomPosition(setupController), directionController.RandomDirection());
        let snakeController = new SnakeController(snake, setupController);
        let gameController = new GameController(snakeController);
        gameController.setKeyDownEventHandler(DocumentEventHandler.keyDownEventHandler,snakeController);
        gameController.setSnakeCurrentlyPositionOnChangeEventHandler(gameController.SnakeCurrentlyPositionOnChangeEventHandler,snakeController);
        gameController.start();
    }
}

