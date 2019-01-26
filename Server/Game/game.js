const Settings = require("./settings");
const Tile = require("./tile");

class Game {
    constructor() {
        this.board = Game.generateBoard(); // Array of tiles
    }

    static generateBoard() {
        const board = [];
        for (let i = 0; i < Settings.boardSize; i++) {
            board.push(Game.randomTile(i));
        }
        return board;
    }

    static randomTile(place) {
        const type = Tile.RandomType();
        switch (type) {
            case Tile.Type.Empty:
                return Tile.Empty();
            case Tile.Type.Street:
                const cost = Settings.initialCost + Settings.costGrow * place;
                const color = Tile.RandomColor();
                return Tile.Street(cost, color);
            default:
                console.error("Unsupported tile type: " + type);
                return Tile.Empty();
        }
    }
}

module.exports = Game;
