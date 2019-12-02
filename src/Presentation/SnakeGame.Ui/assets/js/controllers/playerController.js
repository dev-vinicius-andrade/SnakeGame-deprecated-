class PlayerController {
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
    async new(name){
       this.player = await this.hub
            .invoke("New",name).catch((error)=>console.log(error));
        return this;
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