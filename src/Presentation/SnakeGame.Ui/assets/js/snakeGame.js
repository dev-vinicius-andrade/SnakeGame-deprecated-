"use strict";

window.onload = async function() {
    let gameConfigurations;
    let canvas = document.getElementById("snakeCanvas");
    let playerInput = document.getElementById("playerNameInput");
    let canvasController = new CanvasController(canvas);
    let gameHub;
    await ConfigureGameHub();


    async function ConfigureGameHub(){
        gameHub  = new signalR
            .HubConnectionBuilder()
            .withUrl(CONFIGURATIONS.GAME.HUB.BASE+CONFIGURATIONS.GAME.HUB.GAME)
            //.withAutomaticReconnect([1000,1000,1000,1000,1000,null])
            .build();
        gameHub.serverTimeoutInMilliseconds=100000;
        gameHub.start().then(async p=> {}).catch((error)=>{console.log("error connecting on gameHub!  "+ error);});
        gameHub.on("Configurations",  async function (configurations) {
            console.log(configurations);
            gameConfigurations = configurations;
            await  initializeCanvas(gameConfigurations);
        });
    }
    async function initializeCanvas(configurations){
        canvasController.initialize(configurations.room.width,configurations.room.height,configurations.room.backgroundColor);
    }
    async function GameSetup() {
        let gameController =
            new GameController(gameHub,
                gameConfigurations,
                canvasController,
                new FoodsController(canvasController, gameHub),
                new PlayerController(gameHub), new SnakeController(gameHub, canvasController));
        return gameController;
    }

    async function ConfigureGameDispose(game, player) {
        window.addEventListener("beforeunload", async function (e) {
            await game.playerController.disconnectPlayer(player);
        });
        return game;
    }

    this.JoinGame = async function(roomId){
            let game = await GameSetup();
            let player = await game.connectPlayer(playerInput.value, roomId);
            await  ConfigureGameDispose(game,player);
            if(player!==null || player !==undefined) {
                await game.ShowGame();
                await game.registerEvents(player);
                await game.start(player);
            }else{
                await game.HideGame();
                await RoomIsNotAvailable();
            }
     }
    this.JoinNewRoom=async function () {
        await  JoinGame(null);
    }
    this.JoinExistingRoom= async function () {
        console.log("not implemented");
    }
     this.RoomIsNotAvailable = async  function() {
            alert("Room is not available")
     }
     

}

