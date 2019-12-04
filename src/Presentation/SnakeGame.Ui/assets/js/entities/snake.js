class Snake {
    size;
    currentlyPosition;
    angle;
    path;
    direction;
    color;
    headSize;
    constructor(initialPosition,direction) {
        this.currentlyPosition = initialPosition;
        this.path = new Array(initialPosition);
        this.direction = direction;
    }

}