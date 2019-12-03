"use strict";
window.onload = function() {

    let canvas = document.getElementById("snakeCanvas");
    let canvasController = new CanvasController(CONFIGURATIONS.SNAKE.MOVEMENT_TYPE,canvas);
    let directionController = new DirectionController();
    //let positionController = new PositionController(canvasController);
    let gameHub  = new signalR
        .HubConnectionBuilder()
        .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.GAME)
        .build();
    let playerHub  = new signalR
        .HubConnectionBuilder()
        .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.PLAYER)
        .build();
    let foodHub =   new signalR
        .HubConnectionBuilder()
        .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.FOOD)
        .build();
    let snakeHub =   new signalR
        .HubConnectionBuilder()
        .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.SNAKE)
        .build();

    canvasController.initialize();


    this.JoinGame = async function () {

        let game = await Setup();
        await game.connectPlayer('teste novo player');
        canvasController.writeRoomId(game.playerController.player.roomId);
        await ConfigureGameDispose(game)

        await game.start();
    }
    async function ConfigureGameDispose(game)
    {
        window.addEventListener("beforeunload",async function (e) {
            await game.playerController.disconnectPlayer();
        });
    }
    async function InitializeHubs(){
         await gameHub.start().catch((error)=>console.log("error connecting on gameHub!"));
         await playerHub.start().catch((error)=>console.log("error connecting on playerHub!"));
         await foodHub.start().catch((error)=>console.log("error connecting on foodHub!"));
         await snakeHub.start().catch((error)=>console.log("error connecting on foodHub!"));
    }

    async function Setup() {
        await InitializeHubs();
        let snakeController = new SnakeController().setCanvasController(canvasController).setHub(snakeHub);
        let playerController = new PlayerController(snakeController).setHub(playerHub);

        let foodsController = new FoodsController().setCanvasController(canvasController).setHub(foodHub);
        let gameController = new GameController(foodsController,playerController).setCanvasController(canvasController).setHub(gameHub);
        return gameController;

    }
}

