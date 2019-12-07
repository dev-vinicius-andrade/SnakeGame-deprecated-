class Position{
    _x;
    _y;
    _angle;
    constructor(xPosition,yPosition,angle) {
        this._x=null;
        this._y=null;
        this._angle = null;
        this.x= xPosition;
        this.y=yPosition;
        this.angle=angle;
        this.onChangeController=null;
    }
    set angle(angle)
    {
        if(this._angle!=angle) {
            this._angle = angle;
            this.onChange(this);
        }
    }
    get angle(){
        return this._angle;
    }
    get x(){
        return this._x;
    }
    set x(position)
    {
        if(this._x!=position) {
            this._x = position;
            this.onChange(this);
        }
    }
    set y(position)
    {
        if(this._y!=position) {
            this._y = position;
            this.onChange(this);
        }
    }
    get y(){
        return this._y;
    }
    onChange(position){}
    registerOnChangeHandler(eventHandler,controller){
        this.onChangeController=controller;
        this.onChange = eventHandler;
    }
}