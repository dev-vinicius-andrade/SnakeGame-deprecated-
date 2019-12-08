"use strict";
import CanvasModule from "./modules/canvasModule.js";
import GameModule from "./modules/gameModule.js";
import PlayerModule from "./modules/playerModule.js";
import SnakeModule from "./modules/snakeModule.js";
import HubsService from "./services/hubsService.js";
window.onload = async function() {
    let canvas = document.getElementById("snakeCanvas");

    let hubsService = new  HubsService();
    let canvasModule = new CanvasModule(canvas);
    let gameHub = await hubsService.configureGameHub();
    let playerModule = new PlayerModule(gameHub,new SnakeModule(gameHub,canvasModule));
    let gameModule = new GameModule(gameHub, canvasModule,playerModule);

    async function JoinGame(roomId){
            let player = await playerModule.connectPlayer(await playerModule.getPlayerName(), roomId);
            if(player!==null || player !==undefined) {
                await gameModule.showGame();
                await gameModule.registerEvents(player);
                await gameModule.start(player);
            }else{
                await gameModule.hideGame();
                await RoomIsNotAvailable();
            }
     }
    this.JoinNewRoom =async function() {
        await  JoinGame(null);
    }
   this.JoinExistingRoom =async function() {
        console.log("not implemented");
    }
    this.RoomIsNotAvailable = async function() {
        console.log("Room is not available")
     }
     

}

