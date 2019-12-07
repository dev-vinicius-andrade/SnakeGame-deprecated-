export default class  CanvasModule{
    objects;
    canvasElement;
    context;
    backgroundColor;
    constructor(canvasObject) {
        this.canvasElement = canvasObject;
        this.context=  canvasObject.getContext('2d');
    }
    writeText(text, position,color,font)
    {
        this.context.beginPath();
        this.context.font = font;
        this.context.fillStyle = color
        this.context.fillText(text,position.x, position.y);
        this.context.closePath();
    }
    drawPath(color,borderColor,path,size)
    {
        for(let position of path)
            this.drawRectangle(color,borderColor,position,size);
    }

    drawRectangle(color,borderColor,position,size)
    {
        this.context.beginPath();
        this.context.fillStyle =color;

        this.context.rect(position.x,position.y,size,size);
        this.context.fill();
        this.context.closePath();
        if(borderColor)
        {
            this.context.strokeStyle = borderColor;
            this.context.lineWidth = 1;
            this.context.stroke();
        }
    }

     clear(position, size)
    {
        this.drawRectangle(this.backgroundColor,null,position,size);
    }
    initialize(width,height,backgroundColor)
    {
        this.canvasElement.width = width;
        this.canvasElement.height=height;
        this.context.beginPath();
        this.context.fillStyle =backgroundColor;
        this.context.rect(0,0,width,height);
        this.context.fill();
        this.context.closePath();
    }
    display()
    {
        this.canvasElement.style.display='block';
    }
    hide()
    {
        this.canvasElement.style.display='none';
    }
}