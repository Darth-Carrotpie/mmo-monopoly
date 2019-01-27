const Settings = require("./settings");
const Tile = require("./tile");
const Game = require("./game");
const fs = require('fs');


const game = new Game();
// console.log(game);
console.log("##  GET BOARD ##")
const boardMessage = {
    messageType: "board",
    board: game.getBoard()
}
fs.writeFile("board.json", JSON.stringify(boardMessage));

const randomItem = (array) => {
    return array[Math.floor(Math.random() * array.length)];
}

const turns = 20;
for (let i = 0; i < turns; i++) {
    if (i < 10) {
        game.addPlayer();
        game.addPlayer();
    }

    console.log("##  GET STATE ##")
    const stateMessage = {
        messageType: "state",
        state: game.getState(0)
    }
    console.log(stateMessage.state.me.houses, stateMessage.state.me.hotels);
    fs.writeFile("state" + i + ".json", JSON.stringify(stateMessage));

    game.players.forEach(player => {
        player.intent = randomItem(player.possibleActions(game.board).map((a) => a.type));
    });

    game.advance();
}

console.log("##  GET STATE ##")
const stateMessage = {
    messageType: "state",
    state: game.getState(0)
}
console.log(stateMessage.state.me.houses, stateMessage.state.me.hotels);
fs.writeFile("state" + turns + ".json", JSON.stringify(stateMessage));
