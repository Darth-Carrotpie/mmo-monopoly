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
    }

    possibleActions(tile) {
        const actions = [];
        actions.push("nothing"); // Can do nothing all the time
        if (tile.type == Tile.Type.Street) {
            if (this.cash > tile.properties.cost) {
                actions.push("buy-house");
            }
            if (this.cash > tile.properties.cost) {
                actions.push("buy-hotel");
            }
        }
    }
}

Player.Action = {
    Nothing: Settings.playerActions.Nothing,
    BuyHouse: Settings.playerActions.BuyHouse,
    BuyHotel: Settings.playerActions.BuyHotel,
}

module.exports = Player;
