class FruitsController {
    constructor(canvasController, positionController) {
        this.canvasController=canvasController;
        this.positionController = positionController;
    }
    generateRandomFruit()
    {

        let fruit = new Fruit(this.positionController.RandomPosition());


        if(!this.fruitExists(fruit))
            this.addFruit(fruit);
    }
    addFruit(fruit)
    {
        this.canvasController.objects.fruits.push(fruit);
        let randomFruitColor = this.RandomColor();
        this.canvasController.draw(randomFruitColor, fruit.position, CONFIGURATIONS.FRUITS.FOOD_RADIUS);
    }
    removeFruit(fruit)
    {
        this.canvasController.objects.fruits.splice(this.canvasController.objects.fruits.indexOf(fruit),1);
        this.canvasController.clear(fruit,CONFIGURATIONS.FRUITS.FOOD_RADIUS);
    }
    getFruit(position)
    {
        
        return this.canvasController.objects.fruits.find(function (element) {
            return element.position.X===position.X && element.position.Y===position.Y;
        });
    }
    fruitExists(fruit){
        let exists = this.canvasController.objects.fruits.some(function (element) {
            debugger;
            return element.position.X===fruit.position.X && element.position.Y===fruit.position.Y;
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