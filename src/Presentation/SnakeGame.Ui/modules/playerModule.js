export default class PlayerModule {
    hub;
    snakeModule;
    constructor(hub,snakeModule) {
        this.hub=hub;
        this.snakeModule=snakeModule;
    }

    async connectPlayer(name, roomId){
      let player = await this.hub
            .invoke("NewPlayer",name, roomId).then((result)=>{
                console.log(result);

          }).catch((error)=>console.log(error));
      this.onGameDispose(this,player);
        return player;
    }
    async  onGameDispose(scope, player) {
         window.addEventListener("beforeunload", async function (e) {
            await scope.disconnectPlayer(player);
        });
    }
    async getPlayerName()
    {
        let playerInput = document.getElementById("playerNameInput");
        return  playerInput.value;
    }

    async disconnectPlayer(player)
    {
        if(player)
            await this.hub
                .invoke("Disconnect",player.roomId).catch((error)=>console.log(error));
    }

     renderSnakes(players)
    {
        if(players!=null)
            for(let player of players)
                this.snakeModule.renderSnake(player.snake);
    }
    getKeyDownEventHandler()
    {
        return this.snakeModule
    }


}