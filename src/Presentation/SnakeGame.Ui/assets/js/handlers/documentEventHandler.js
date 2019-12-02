class DocumentEventHandler {
    constructor() {
    }

    static keyDownEventHandler(keyDownEvent, controller){
        switch (keyDownEvent.code) {
            case 'ArrowRight':
                controller.changeDirection(keyDownEvent);
                break;
            case 'ArrowLeft':
                controller.changeDirection(keyDownEvent);
                break;
            case 'ArrowUp':
                controller.changeDirection(keyDownEvent);
                break;
            case 'ArrowDown':
                controller.changeDirection(keyDownEvent);
                break;
            default:
                console.log('Key:'+keyDownEvent.code+ ' handler not implemented');

        }
    }
}