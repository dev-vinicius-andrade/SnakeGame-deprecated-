import Direction from "../entities/direction.js"
export default class DirectionModule {
    constructor() {

    }
    ArrowRight()
    {
        return 2;
    }
    ArrowLeft(){
        return 0
    }
    ArrowUp(){
        return 1;
    }
    ArrowDown(){
        return 3;
    }
}