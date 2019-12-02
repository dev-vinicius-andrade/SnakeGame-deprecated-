class Snake {
    constructor(initialPosition,direction) {
        this.size = 1;
        this.currentlyPosition = initialPosition;
        this.angle = 0;
        this.path = new Array(initialPosition);
        this.direction = direction;
        this.color = CONFIGURATIONS.SNAKE.DEFAULT_BACKGROUND_COLOR;
        this.headRadius = CONFIGURATIONS.SNAKE.HEAD_RADIUS;
    }

}