class Randomizer{
    static RandomValue(minValue, maxValue) {
        return Math.floor(Math.random() * (maxValue - minValue) ) + minValue;
    }
}