class Position{
    constructor(xPosition,yPosition,angle) {
        this.xPosition=null;
        this.yPosition=null;
        this.angleValue = null;
        this.x= xPosition;
        this.y=yPosition;
        this.angle=angle;
        this.onChangeController=null;
    }
    set angle(angle)
    {
        if(this.angleValue!=angle) {
            this.angleValue = angle;
            this.onChange(this);
        }
    }
    get angle(){
        return this.angleValue;
    }
    get x(){
        return this.xPosition;
    }
    set x(position)
    {

        if(this.xPosition!=position) {
            this.xPosition = position;
            this.onChange(this);
        }
    }
    set y(position)
    {
        if(this.yPosition!=position) {
            this.yPosition = position;
            this.onChange(this);
        }
    }
    get y(){
        return this.yPosition;
    }
    onChange(position){}
    registerOnChangeHandler(eventHandler,controller){
        this.onChangeController=controller;
        this.onChange = eventHandler;
    }
}