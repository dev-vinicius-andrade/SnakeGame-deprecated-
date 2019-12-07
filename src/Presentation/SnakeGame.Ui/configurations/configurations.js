let configurations = {
    Integration:{
        BaseHub:'https://localhost:5001/',
        PlayerHub:'player',
        GameHub:'game',
        SnakeHub:'snake',
        DefaultReconnectPolicy:[1000,1000,1000,1000,1000,null],
        DefaultTimeout : 100000

    }
}

export default configurations;