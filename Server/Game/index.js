const Settings = require("./settings");
const Tile = require("./tile");
const Game = require("./game");

const game = new Game();
// console.log(game);
console.log("##  GET BOARD ##")
console.log(JSON.stringify(game.getBoard()));

const randomItem = (array) => {
    return array[Math.floor(Math.random() * array.length)];
}

for (let i = 0; i < 5; i++) {
    game.addPlayer();
    game.advance();
    game.players.forEach(player => {
        player.intent = randomItem(player.possibleActions(game.board).map((a) => a.type));
    });
}

console.log("##  GET STATE ##")
console.log(JSON.stringify(game.getState(0)));
