"use strict";

window.onload = function() {

    let canvas = document.getElementById("snakeCanvas");
    let canvasController = new CanvasController(canvas);
    canvasController.initialize(CONFIGURATIONS.CANVAS.WIDTH,CONFIGURATIONS.CANVAS.HEIGHT,CONFIGURATIONS.CANVAS.BACKGROUND_COLOR);
    const gameHub  = new signalR
        .HubConnectionBuilder()
        .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.GAME)
        //.withAutomaticReconnect([1000,1000,1000,1000,1000,null])
        .build();

    this.JoinGame = async function(){
        gameHub.serverTimeoutInMilliseconds=100000;
        gameHub.start().then(async p=> {
            let game = await Setup();
            let player = await game.connectPlayer('teste novo player');
            await  game.registerEvents(player);
            await game.start(player);
            async function Setup() {
                let gameController = new GameController(gameHub, canvasController, new FoodsController(canvasController,gameHub),new PlayerController(gameHub),new SnakeController(gameHub,canvasController));
                return await ConfigureGameDispose(gameController);
            }
            async function ConfigureGameDispose(game)
            {
                window.addEventListener("beforeunload",async function (e) {
                    await game.playerController.disconnectPlayer();
                });
                return  game;
            }
        }).catch((error)=>{console.log("error connecting on gameHub!  "+ error);});
    }
}

