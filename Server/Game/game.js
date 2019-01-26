const Settings = require("./settings");
const Tile = require("./tile");
const Player = require("./player");

class Game {
    constructor() {
        this.board = Game.generateBoard(); // Array of tiles
        this.turnCount = 0;
        this.playerCounter = 0;
        this.players = [];
    }

    getBoard() {
        return this.board;
    }

    getState() {
        const state = {};
        state.turnCount = this.turnCount;
        state.players = this.players;
    }

    addPlayer() {
        const player = new Player(this.playerCounter);
        this.players.push(player);
        this.playerCounter++;
        return player;
    }

    advance() {

    }

    static generateBoard() {
        const board = [];
        board.push(Tile.Empty()); // First tile is always empty
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
