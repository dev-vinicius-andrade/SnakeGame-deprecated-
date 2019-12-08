let configurations = {
    Integration:{
        BaseHub:'https://localhost:5001/',
        GameHub:'game',
        DefaultReconnectPolicy:[1000,1000,1000,1000,1000,null],
        DefaultTimeout : 100000
    }
}

export default configurations;