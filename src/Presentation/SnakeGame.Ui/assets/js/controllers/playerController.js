class PlayerController {
    hub;
    player;
    players;
    snakeController;
    constructor(snakeController) {
        this.hub=null;
        this.player = null;
        this.players=[];
        this.snakeController = snakeController;

    }
    setHub(hub){
        this.hub = hub;
        this.registerEvents();
        return this;
    }
    registerEvents(){
        this.onPlayerJoined();
    }
    async loadEnemies(roomId)
    {
        let players = await this.hub.invoke("GetEnemies",roomId).catch((error) => console.log(error));
        for(let player of players)
            this.snakeController.drawSnakePath(player.snake);
    }
    async new(name){
       this.player = await this.hub
            .invoke("New",name).catch((error)=>console.log(error));
       this.snakeController.setPlayer(this.player);
        return this.player.roomId;
    }
    async disconnectPlayer()
    {
        if(this.player)
            await this.hub
                .invoke("Disconnect",this.player).catch((error)=>console.log(error));
    }


    onPlayerJoined()
    {
        let scope = this;
        this.hub.on("PlayerJoined", function (player) {
            debugger;
            scope.players.push(player);
        });
    }


}