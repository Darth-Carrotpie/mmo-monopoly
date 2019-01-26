const Settings = require("./Game/settings");

const playerActions = Object.values(Settings.playerActions);
const getIntentType = (id) => {
    return playerActions.find((t) => t.id == id).type;
}

const boardMessage = (board) => {
    const message = {};
    message["message-type"] = "board";
    message.board = board;
    return message;
}

const stateMessage = (state) => {
    const message = {};
    message["message-type"] = "state";
    message.state = state;
    return message;
}

class Human {
    constructor(player, sendMessage, game) {
        this.player = player;
        this.sendMessage = sendMessage;
        this.game = game;
    }

    receiveMessage(message) {
        if (message["message-type"] != "intent") {
            console.error("Invalid message received", message);
            return;
        }

        if (message.intent.turnCount != this.game.turnCount) {
            console.error("Invalid turn count given", message);
            return;
        }

        this.player.intent = getIntentType(message.intent.intentId);
    }
}

class Room {
    constructor(game) {
        this.game = game;
        this.humans = [];
        this.robots = [];
    }

    tick() {
        this.game.advance();
        this.humans.forEach(human => {
            human.sendMessage(stateMessage(this.game.getState(human.player.id)));
        });
        console.log("TICK: " + this.game.turnCount);
    }

    join(sendMessage) {
        const human = new Human(this.game.addPlayer(), sendMessage, this.game);
        human.sendMessage(boardMessage(this.game.getBoard()));
        this.humans.push(human);
        return human;
    }

    leave(human) {
        this.humans.splice(this.humans.indexOf(human));
    }

}

module.exports = Room;
