class GameController {
    hub;
    canvasObjects;
    canvasController;
    foodsController;
    playerController;
    snakeController;
    count;
    divCanvas;
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
        this.divCanvas = document.getElementById("div-canvas");
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

    async ShowCanvas() {
        this.divCanvas.style.display = 'block';
        this.divHome.style.display='none';
        this.canvasController.display();
    }
    async HideCanvas(){
        this.divHome.style.display = 'block';
        this.divCanvas.style.display='none';
        this.canvasController.hide();
    }
}