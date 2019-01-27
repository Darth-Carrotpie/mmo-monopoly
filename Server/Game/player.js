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
        this.transactions = []; // Array of transactions
    }

    getState() {
        const state = { ...this };
        delete state.intent;
        delete state.transactions;
        return state;
    }

    possibleActions(board) {
        const tile = board[this.position];
        const actions = [];
        actions.push({
            type: Player.Action.Nothing,
            id: getTypeId(Player.Action.Nothing),
            cost: 0,
        }); // Can do nothing all the time
        if (tile.type == Tile.Type.Street) {
            if (this.cash > tile.properties.cost) {
                actions.push({
                    type: Player.Action.BuyHouse,
                    id: getTypeId(Player.Action.BuyHouse),
                    cost: tile.properties.cost,
                });
            }
            // TODO: add check for 3 same color houses
            if (this.cash > tile.properties.cost * Settings.hotelCostMultiplier) {
                actions.push({
                    type: Player.Action.BuyHotel,
                    id: getTypeId(Player.Action.BuyHotel),
                    cost: tile.properties.cost * Settings.hotelCostMultiplier,
                });
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
