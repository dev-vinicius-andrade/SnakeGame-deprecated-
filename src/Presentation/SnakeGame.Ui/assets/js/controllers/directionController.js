class DirectionController {

    right()
    {
        return new Position(1,0);
    }
    left(){
        return new Position(-1,0);
    }
    up(){
        return new Position(0,1);
    }
    down(){
        return new Position(0,-1);
    }

}