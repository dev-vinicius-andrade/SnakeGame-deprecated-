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

        let hub = hubConnectionBuilder.build();
        if(timeout!=null || timeout!=undefined)
            hub.serverTimeoutInMilliseconds=timeout;
        hub.start().then(async p=> {}).catch((error)=>{console.log("error connecting on "+hubName+"! "+ error);});
        return  hub;
    }
}