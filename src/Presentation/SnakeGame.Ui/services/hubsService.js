import Configurations from "../configurations/configurations.js";
export default  class HubsService{
    async  configureGameHub(){
        return  await this.buildHub(Configurations.Integration.GameHub,null,null);
    }

    async buildHub(hubName,timeout,reconnectPolicy)
    {
        if(reconnectPolicy==null || reconnectPolicy ==undefined)
            reconnectPolicy = Configurations.Integration.DefaultReconnectPolicy;
        if(timeout==null || timeout ==undefined)
            timeout = Configurations.Integration.DefaultTimeout;
        let hubConnectionBuilder = new signalR
            .HubConnectionBuilder()
            .withUrl(Configurations.Integration.BaseHub+hubName);

        if(reconnectPolicy!=null || reconnectPolicy!=undefined)
            hubConnectionBuilder.withAutomaticReconnect(reconnectPolicy);

        hubConnectionBuilder.configureLogging(signalR.LogLevel.Debug);
        let hub = hubConnectionBuilder.build();
        if(timeout!=null || timeout!=undefined)
            hub.serverTimeoutInMilliseconds=timeout;

        //this.configureConnectionSlow(hub);
        hub.start().then(async p=> {}).catch((error)=>{console.log("error connecting on "+hubName+"! "+ error);});
        return  hub;
    }

     configureConnectionSlow(hub){
        // hub.connectionSlow(function () {
        //     console.log('We are currently experiencing difficulties with the connection.')
        // });
    }


}