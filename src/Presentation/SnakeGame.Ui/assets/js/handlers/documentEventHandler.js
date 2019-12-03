class DocumentEventHandler {
    constructor() {
    }

    static keyDownEventHandler(keyDownEvent, controller,player){
        switch (keyDownEvent.code) {
            case 'ArrowRight':
                controller.changeDirection(keyDownEvent,player);
                break;
            case 'ArrowLeft':
                controller.changeDirection(keyDownEvent,player);
                break;
            case 'ArrowUp':
                controller.changeDirection(keyDownEvent,player);
                break;
            case 'ArrowDown':
                controller.changeDirection(keyDownEvent,player);
                break;
            default:
                console.log('Key:'+keyDownEvent.code+ ' handler not implemented');

        }
    }
}