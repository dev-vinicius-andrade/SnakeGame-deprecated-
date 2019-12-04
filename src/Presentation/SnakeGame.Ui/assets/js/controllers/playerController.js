class PlayerController {
    hub;
    player;
    constructor(hub) {
        this.hub=hub;
        this.registerEvents();
    }
    registerEvents(){
        this.onPlayerJoined(this);
    }
    async new(name){
      let player = await this.hub
            .invoke("New",name).catch((error)=>console.log(error));
        return player;
    }
    async disconnectPlayer(player)
    {
        if(this.player)
            await this.hub
                .invoke("Disconnect",player).catch((error)=>console.log(error));
    }
    onPlayerJoined(scope)
    {

        this.hub.on("PlayerJoined", function (player) {
        });
    }


}