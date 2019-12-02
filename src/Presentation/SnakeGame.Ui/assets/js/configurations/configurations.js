 let CONFIGURATIONS =  {

    SNAKE : {
        DEFAULT_SNAKE_MOVEMENT_INTERVAL_TIMEOUT:30,
        DEFAULT_BACKGROUND_COLOR:'#FFFFFF',
        HEAD_RADIUS:5,
        SPEED:2,
        ROTATION_SPEED:3,
        MOVEMENT_TYPE: canvasDrawTypeEnum.RECTANGLE
    },
    FOOD:{
        DEFAULT_FRUITS_GENERATION_INTERVAL_TIMEOUT:250,
        FOOD_RADIUS:5
    },
    CANVAS:{
        MINIMUM_POSITION:0,
        BACKGROUND_COLOR:'#393E46',
        DEFAULT_ADITIONAL_CLEAR_RADIUS_VALUE:2,
        WIDTH:500,
        HEIGHT:500
    },
     GAME:{
        HUB:{
            BASE:'https://localhost:5001/',
            FOOD:'food',
            PLAYER:'player',
            GAME:'game',
            SNAKE:'snake'
        }
     }

};