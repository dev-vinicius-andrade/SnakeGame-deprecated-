export default class FoodsModule {
    canvasModule;
    constructor(canvasModule) {
        this.canvasModule=canvasModule;
    }


    generateFoods(foods)
    {
        for(let food of foods)
            this.canvasModule.drawRectangle(food.color,food.borderColor,food.position,food.size);
    }
}