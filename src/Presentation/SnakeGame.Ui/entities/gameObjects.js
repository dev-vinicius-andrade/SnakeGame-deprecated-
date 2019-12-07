export default  class GameObjects {
    roomGuid;
    players;
    score;
    foods;
    constructor() {
        this.roomGuid = null;
        this.score = null;
        this.players=new Array();
        this.foods = new Array();
    }

}