class Helper{
    static RandomValue(minValue, maxValue) {
        return Math.floor(Math.random() * (maxValue - minValue) ) + minValue;
    }
    static DegreesToRadian(angle){
        return ((angle*Math.PI)/180);
    }
}