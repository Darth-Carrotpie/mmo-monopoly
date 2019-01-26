const Settings = require("./settings");
const Tile = require("./tile");

const playerActions = Object.values(Settings.playerActions);
const getTypeId = (type) => {
    return playerActions.find((t) => t.type == type).id;
}

class Player {
    constructor(id) {
        this.id = id; // Unique id
        this.position = 0; // All players start at 0
        this.houses = []; // Array of board indices
        this.hotels = []; // Array of board indices
        this.cash = Settings.initialMoney;
        this.intent = Player.Action.Nothing;
        this.roll = []; // Array for dice rolls
    }

    getState() {
        const state = { ...this };
        delete state.intent;
        return state;
    }

    possibleActions(board) {
        const tile = board[this.position];
        const actions = [];
        actions.push(Player.Action.Nothing); // Can do nothing all the time
        if (tile.type == Tile.Type.Street) {
            if (this.cash > tile.properties.cost) {
                actions.push(Player.Action.BuyHouse);
            }
            // TODO: add check for 3 same color houses
            if (this.cash > tile.properties.cost * Settings.hotelCostMultiplier) {
                actions.push(Player.Action.BuyHotel);
            }
        }
        return actions;
    }
}

Player.Action = {
    Nothing: Settings.playerActions.Nothing.type,
    BuyHouse: Settings.playerActions.BuyHouse.type,
    BuyHotel: Settings.playerActions.BuyHotel.type,
}

module.exports = Player;
