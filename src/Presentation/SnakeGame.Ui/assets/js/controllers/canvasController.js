class  CanvasController{
    movementType;
    objects;
    canvasElement;
    context;
    backgroundColor;
    scale;
    minimumPosition;
    constructor(type,canvasObject) {
        this.movementType = type;
        this.objects = new CanvasObjects();
        this.canvasElement = canvasObject;
        this.context=  canvasObject.getContext('2d');
        this.canvasElement.width = CONFIGURATIONS.CANVAS.WIDTH;
        this.canvasElement.height = CONFIGURATIONS.CANVAS.HEIGHT;
        this.backgroundColor = CONFIGURATIONS.CANVAS.BACKGROUND_COLOR;
        this.scale = CONFIGURATIONS.SNAKE.HEAD_RADIUS;
        this.minimumPosition = CONFIGURATIONS.CANVAS.MINIMUM_POSITION;
    }
    writeRoomId(roomId)
    {
        this.context.beginPath();
        this.context.font = '10px Arial';
        this.context.fillStyle = 'rgba(198, 255, 53, 0.1)'
        this.context.fillText(roomId,this.canvasElement.width-210, 15);
        this.context.closePath();

    }
    drawPath(color,path)
    {
        this.context.beginPath();
        this.context.fillStyle =color;
        this.context.lineCap = "square";
        let firstPosition = path.shift();
        this.context.moveTo(firstPosition.x,firstPosition.y);
        for(let position of path)
            this.context.lineTo(position.x,position.y);



        //this.context.rect(position.x,position.y,this.scale,this.scale);
        this.context.lineWidth = 10;
        this.context.stroke();
        this.context.closePath();
    }
    draw(color, position, radius = null)
    {
        this.context.fillStyle =color;
        switch (this.movementType) {
            case canvasDrawTypeEnum.RECTANGLE:
                this.drawRectangle(color,position);
                break;
            case canvasDrawTypeEnum.CIRCLE:
                this.drawCircle(color,position,radius);
                break;
        }
    }
    drawRectangle(color,position)
    {
        this.context.beginPath();
        this.context.fillStyle =color;
        this.context.rect(position.x,position.y,this.scale,this.scale);
        this.context.fill();
        this.context.closePath();
    }
    drawCircle(color,position,radius=null)
    {
        if(!radius)
            radius=this.scale;
        this.context.beginPath();
        this.context.fillStyle =color;
        this.context.arc(position.x,position.y,radius,0,2*Math.PI);
        this.context.fill();
        this.context.closePath();
    }

    clear(position, radius=null)
    {
        switch (this.movementType) {
            case canvasDrawTypeEnum.RECTANGLE:
                this.drawRectangle(this.backgroundColor,position);
                break;
            case canvasDrawTypeEnum.CIRCLE:
                this.drawCircle(this.backgroundColor,position,radius+CONFIGURATIONS.CANVAS.DEFAULT_ADITIONAL_CLEAR_RADIUS_VALUE);
                break;
        }
    }
    initialize()
    {
        this.context.beginPath();
        this.context.fillStyle =this.backgroundColor;
        this.context.rect(0,0,this.canvasElement.width,this.canvasElement.height);
        this.context.fill();
        this.context.closePath();
    }
}