
class PositionController {
    constructor(canvasController) {
        this.canvasController=canvasController;
    }
    RandomPosition() {
        let position = new Position(
            Helper.RandomValue(this.canvasController.minimumPosition,this.canvasController.canvasElement.width - this.canvasController.scale),
            Helper.RandomValue(this.canvasController.minimumPosition,this.canvasController.canvasElement.height - this.canvasController.scale),
            0
        );
        return position;
    }
}