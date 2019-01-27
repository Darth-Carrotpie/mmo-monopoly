const Settings = require("./Game/settings");

const playerActions = Object.values(Settings.playerActions);
const getActionType = (id) => {
    return playerActions.find((t) => t.id == id).type;
}

const boardMessage = (board) => {
    const message = {};
    message.messageType = "board";
    message.board = board;
    return message;
}

const stateMessage = (state) => {
    const message = {};
    message.messageType = "state";
    message.state = state;
    return message;
}

const randomItem = (array) => {
    return array[Math.floor(Math.random() * array.length)];
}

class Human {
    constructor(player, sendMessage, game) {
        this.player = player;
        this.sendMessage = sendMessage;
        this.game = game;
    }

    receiveMessage(message) {
        if (message.messageType != "intent") {
            console.error("Invalid message received", message);
            return;
        }

        if (message.intent.turnCount != this.game.turnCount) {
            console.error("Invalid turn count given", message);
            return;
        }

        this.player.intent = getActionType(message.intent.actionId);
    }
}

class Room {
    constructor(game) {
        this.game = game;
        this.humans = [];
        this.robots = [];
    }

    tick() {
        this.robots.forEach(robot => {
            robot.intent = randomItem(robot.possibleActions(this.game.board).map((a) => a.type));
        });

        for (let i = this.robots.length - 1; i >= 0; i--) {
            const robot = this.robots[i];
            if (robot.cash < 0) {
                this.robots.splice(i, 1);
            }
        }

        const furthestRobot =  Math.max.apply(null, this.robots.map((r) => r.position));
        const furthestHuman =  Math.max.apply(null, this.humans.map((h) => h.player.position));

        this.game.advance();
        this.humans.forEach(human => {
            human.sendMessage(stateMessage(this.game.getState(human.player.id)));
        });
        console.log(
            "TICK: " + this.game.turnCount +
            " humans: " + this.humans.length +
            " furthestHuman: " + furthestHuman +
            " robots: " + this.robots.length +
            " furthestRobot: " + furthestRobot
            );
    }

    join(sendMessage) {
        const human = new Human(this.game.addPlayer(), sendMessage, this.game);
        human.sendMessage(boardMessage(this.game.getBoard(human.player.id)));
        this.humans.push(human);
        return human;
    }

    leave(human) {
        this.humans.splice(this.humans.indexOf(human));
    }

    addRobot() {
        const robot = this.game.addPlayer();
        this.robots.push(robot);
    }
}

module.exports = Room;
