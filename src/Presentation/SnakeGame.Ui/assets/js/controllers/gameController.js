class GameController {
    hub;
    canvasObjects;
    canvasController;
    foodsController;
    playerController;
    snakeController;
    count;
    divGame;
    divInfos;
    divHome;
    configurations;
    constructor(hub,configurations,canvasController,foodsController,playerController,snakeController) {
        this.hub=hub;
        this.configurations = configurations;
        this.canvasObjects=new CanvasObjects();
        this.canvasController=canvasController;
        this.foodsController = foodsController;
        this.playerController = playerController;
        this.snakeController = snakeController;
        this.count=0;
        this.divGame = document.getElementById("div-game");
        this.divInfos = document.getElementById("div-infos");
        this.divHome = document.getElementById("div-home");
    }
     registerKeyDownEventHandler(eventHandler, controller,player){
        document.onkeydown = function (e) {
            eventHandler(e,controller,player);
        }
        return this;
    }

    async connectPlayer(playerName){
        return  await this.playerController.new(playerName);
    }

    registerEvents(player){
        this.registerKeyDownEventHandler(DocumentEventHandler.keyDownEventHandler,this.snakeController,player);
        this.registerOnSnakeMovedEventHandler(this);
        this.hub.onreconnecting()
    }

    start(player){
        window.setInterval(()=>{
        this.hub.invoke('start', player.roomId)
            .then(r => {

                this.renderCanvasObjects(this);

            })
            .catch((error)=>{console.log("Error on game start: "+error)});
        },this.configurations.room.frameRateInterval)
    }

    registerBackEndError()
    {
        this.hub.on("BackendError", function (error) {
            console.log(error);
        });
    }
    renderCanvasObjects(scope){
        scope.renderCanvas(scope);
        scope.renderSnakes(scope);
        //requestAnimationFrame(scope.renderCanvasObjects(scope));
    }
    renderCanvas(scope){
        this.canvasController.initialize(this.configurations.room.width,this.configurations.room.height,this.configurations.room.backgroundColor);
    }
     renderSnakes(scope){
        if(scope.canvasObjects.players!=null)
            for(let player of scope.canvasObjects.players)
            {
            //scope.canvasController.clear(position, size)
                scope.canvasController.drawPath(player.snake.color, player.snake.borderColor, player.snake.path, player.snake.headSize);
            }
    }
     renderFoods(){

    }
    functionRenderScore(){

    }
    registerOnSnakeMovedEventHandler(scope)
    {
        this.hub.on("SnakeMoved", function (movementTracker) {

               scope.count+=1;
               console.log(scope.count);
               scope.canvasObjects.players = movementTracker.players;
               scope.canvasObjects.foods = movementTracker.foods;

        });
    }

    async ShowGame() {
        this.divGame.style.display = 'block';
        this.divGame.style.backgroundColor = this.configurations.room.backgroundColor;
        this.divHome.style.display='none';
        this.canvasController.display();
        await this.showInfos();
    }
    async showInfos(){
        this.divInfos.style.backgroundColor = this.configurations.room.infos.backgroundColor;
        this.divInfos.style.opacity = this.configurations.room.infos.opacity;
        this.divInfos.style.width = this.configurations.room.infos.width;
        this.divInfos.style.height = this.configurations.room.infos.height;
        this.divInfos.style.position ='absolute';
        this.divInfos.style.top =0;
        this.divInfos.style.rigth =0;
    }
    async HideGame(){
        this.divHome.style.display = 'block';
        this.divGame.style.display='none';
        this.canvasController.hide();
    }
}