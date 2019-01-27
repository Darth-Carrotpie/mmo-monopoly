const Settings = require("./settings");
const Tile = require("./tile");
const Player = require("./player");
const Transaction = require("./transaction");

const rollDice = (roll) => {
    for (let i = 0; i < Settings.diceCount; i++) {
        roll[i] = Math.floor(Math.random() * (Settings.diceMax - Settings.diceMin) + Settings.diceMin);
    }
}

const pay = (payer, owners, money) => {
    // Maybe record this, so server doesn't have to redo the logic?
    owners.forEach(owner => {
        owner.cash += money;
        Transaction.ReceiveRent(payer, owner, money);
    });
}

class Game {
    constructor() {
        this.board = Game.generateBoard(); // Array of tiles
        this.turnCount = 0;
        this.playerCounter = 0;
        this.players = []; // Array of players
    }

    getBoard() {
        return this.board.map((t) => t.getState());;
    }

    getState(playerId) {
        const state = {};
        state.turnCount = this.turnCount;
        state.players = this.players.map((p) => p.getState());

        const player = this.players.find((p) => p.id == playerId);
        if (player) {
            state.me = player.getState();
            state.me.possibleActions = player.possibleActions(this.board);
            state.me.transactions = player.transactions;
        }

        return state;
    }

    addPlayer() {
        const player = new Player(this.playerCounter);
        this.players.push(player);
        this.playerCounter++;
        return player;
    }

    advance() {
        // RESET TRANSACTIONS
        this.players.forEach(player => {
            player.transactions.length = 0;
        });
        // REMOVE LOSERS
        Game.removeLosers(this.players, this.board);
        // BUILD
        Game.build(this.players, this.board);
        // ADVANCE, GET DOUBLE BONUS
        Game.moveForward(this.players, this.board);
        // SORT FROM BOARD START
        this.players.sort((a, b) => {
            return a.position - b.position;
        });
        // PAY OR RESOLVE TILES
        Game.resolveTiles(this.players, this.board);
        // RESET INTENT TO DO NOTHING
        this.players.forEach(player => {
            player.intent = Player.Action.Nothing;
        });
        this.turnCount++;
    }

    static generateBoard() {
        const board = [];
        board.push(Tile.Go()); // First tile is always empty
        for (let i = 0; i < Settings.boardSize; i++) {
            board.push(Game.randomTile(i));
        }
        return board;
    }

    static randomTile(place) {
        const type = Tile.RandomType(place);
        switch (type) {
            case Tile.Type.Empty:
                return Tile.Empty();
            case Tile.Type.Street:
                const cost = Settings.initialCost + Settings.costGrow * place;
                const color = Tile.RandomColor();
                return Tile.Street(cost, color);
            case Tile.Type.Go:
                return Tile.Go();
            case Tile.Type.Pay:
                const payCost = Math.floor((Settings.initialCost + Settings.costGrow * Math.pow(place, 1.5)) * Settings.payMultiplier);
                // console.log("pay", place, payCost);
                return Tile.Pay(payCost);
            default:
                console.error("Unsupported tile type: " + type);
                return Tile.Empty();
        }
    }

    static removeLosers(players, board) {
        for (var i = players.length - 1; i >= 0; i--) {
            const player = players[i];
            if (player.cash < 0) {
                // console.warn("Player went bankrupt", player);
                players.splice(i, 1);
            }
            /*
            if (player.position >= board.length) {
                console.warn("Player over boundaries", player);
                players.splice(i, 1);
            }
            */
        }
    }

    static build(players, board) {
        players.forEach(player => {
            const currentTile = board[player.position];
            const possibleActions = player.possibleActions(board).map((a) => a.type);
            if (!possibleActions.includes(player.intent)) {
                console.error("Impossible intent for player", player, possibleActions);
                return;
            }

            if (player.intent == Player.Action.BuyHouse) {
                const cost = currentTile.properties.cost
                player.cash -= cost;
                player.houses.push(player.position);
                Transaction.PayBuild(player, cost);
            }

            if (player.intent == Player.Action.BuyHotel) {
                const cost = currentTile.properties.cost * Settings.hotelCostMultiplier;
                player.cash -= cost;
                player.hotels.push(player.position);
                Transaction.PayBuild(player, cost);
            }
        });
    }

    static moveForward(players, board) {
        players.forEach(player => {
            rollDice(player.roll);
            const initialPosition = player.position;
            player.position += player.roll.reduce((a, b) => a + b, 0);
            // YOU SHALL NOT PASS
            player.position = Math.min(player.position, Settings.boardSize - 1);
            const endPosition = player.position;

            // Collecting GO
            for (let i = initialPosition + 1; i + 1 < Math.min(endPosition, board.length); i++) {
                const tile = board[i];
                if (tile.type == Tile.Type.Go) {
                    player.cash += Settings.goReward;
                    Transaction.PassGo(player, i);
                }
            }

            // RESOLVE DOUBLE
            if (player.roll[0] == player.roll[1]) {
                player.cash += Settings.doubleBonus;
                Transaction.RollDouble(player, initialPosition);
            }
        });
    }

    static resolveTiles(players, board) {
        // Add recursion support for chests?
        players.forEach(player => {
            const tile = board[player.position];
            if (tile.type == Tile.Type.Street) {
                Game.payRent(players, player, tile.properties.cost);
            } else if (tile.type == Tile.Type.Pay) {
                player.cash -= tile.properties.cost;
                Transaction.PayTax(player, tile.properties.cost);
            }
        });
    }

    static houseOwners(players, position) {
        console.assert(typeof position === "number");
        const owners = [];
        players.forEach(player => {
            if (player.houses.includes(position)) {
                owners.push(player);
            }
        });
        return owners;
    }

    static hotelOwners(players, position) {
        console.assert(typeof position === "number");
        const owners = [];
        players.forEach(player => {
            if (player.hotels.includes(position)) {
                owners.push(player);
            }
        });
        return owners;
    }

    static payRent(players, player, cost) {
        console.assert(typeof cost === "number");
        const houseOwners = Game.houseOwners(players, player.position);
        const hotelOwners = Game.hotelOwners(players, player.position);

        if (houseOwners.length > 0) {
            player.cash -= cost;
            Transaction.PayRent(player, cost);
            const split = Math.floor(cost / houseOwners.length);
            pay(player, houseOwners, split);
        }

        if (hotelOwners.length > 0) {
            player.cash -= cost * hotelOwners.length;
            Transaction.PayRent(player, cost * hotelOwners.length);
            pay(player, hotelOwners, cost);
        }
    }
}

module.exports = Game;
