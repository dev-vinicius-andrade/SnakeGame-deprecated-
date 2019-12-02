
class PositionController {
    constructor(setup) {
        this.setup=setup;
    }
    RandomPosition() {
        let position = new Position(
            Randomizer.RandomValue(this.setup.canvas.minimumPosition,this.setup.canvas.canvasElement.width - this.setup.canvas.scale),
            Randomizer.RandomValue(this.setup.canvas.minimumPosition,this.setup.canvas.canvasElement.height - this.setup.canvas.scale)
        );
        return position;
    }
}