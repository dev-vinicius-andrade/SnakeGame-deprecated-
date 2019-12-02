class FoodsController {
    constructor() {
        this.canvasController=null;
        this.hub=null;
    }
    setCanvasController(controller)
    {
        this.canvasController = controller;
        return this;
    }
    setHub(hub){
        this.hub=hub;
        this.registerEvents();
        return this;
    }
    registerEvents(){
        this.onFoodGenerated();
    }
    async loadFoods(roomId)
    {
        let foods = await this.hub.invoke("GetAll",roomId).catch((error) => console.log(error));
        for(let food of foods)
            this.add(food);
    }
    generateFood(roomId)
    {

        this.hub.invoke("GenerateFood",roomId).catch((error) => console.log(error));
    }
    onFoodGenerated()
    {
        let scope = this;
        this.hub.on("FruitGenerated", function (food) {
            scope.add(food);
        });
    }
    add(food)
    {
        this.canvasController.objects.foods.push(food);
        let color = this.RandomColor();
        this.canvasController.draw(color, food.position, CONFIGURATIONS.FOOD.FOOD_RADIUS);
    }
    remove(food)
    {
        this.canvasController.objects.foods.splice(this.canvasController.objects.foods.indexOf(food),1);
        this.canvasController.clear(food.position,CONFIGURATIONS.FOOD.FOOD_RADIUS);
    }
    get(position)
    {
        for (const food of this.canvasController.objects.foods) {
            if (
                (position.x > food.position.x - 10) && (position.x < food.position.x + 10) &&
                (position.y > food.position.y - 10) && (position.y < food.position.y + 10)
            ) {
                return food;
            }
        }
    }

    exists(food){
        let exists = this.canvasController.objects.foods.some(function (element) {
            return element.position.x===food.position.x && element.position.y===food.position.y;
        });
        return exists;
    }
    RandomColor()
    {
        let color= '#'+((1 << 24) * Math.random()|0).toString(16);
        if(color!= CONFIGURATIONS.CANVAS.BACKGROUND_COLOR && color!= CONFIGURATIONS.SNAKE.DEFAULT_BACKGROUND_COLOR)
            return color;
        return  this.RandomColor();
    }
}