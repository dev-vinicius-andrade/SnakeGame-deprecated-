class DirectionController {
    constructor() {

    }
    ArrowRight()
    {
        return new Direction(1,0,CONFIGURATIONS.SNAKE.ROTATION_SPEED);
        //mudar o ROTATION_SPEED para 1 e -1 no arrow left

    }
    ArrowLeft(){
        return new Direction(-1,0,-CONFIGURATIONS.SNAKE.ROTATION_SPEED);
    }
    ArrowUp(){
        return new Direction(0,-1,0);
    }
    ArrowDown(){
        return new Direction(0,1,0);
    }
    RandomDirection()
    {
        let value = Helper.RandomValue(1,100);
        if(value<25)
            return this.ArrowRight();
        if(value<50)
            return  this.ArrowLeft();
        if(value<75)
            return  this.ArrowUp();
        if(value<=100)
            return this.ArrowDown();

        console.log('Failed to generate randomic direction, using right as default');
        return this.ArrowRight();
    }

}