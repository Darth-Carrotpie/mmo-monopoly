const Settings = {
    // GAME
    boardSize: 16,
    initialCost: 100,
    costGrow: 50,
    diceMin: 1,
    diceMax: 6,
    diceCount: 2,
    // TILE
    tileTypes: {
        Street: {
            id: 0,
            type: "street",
            chance: 5
        },
        Empty: {
            id: 1,
            type: "empty",
            chance: 1
        }
    },
    // PLAYER
    initialMoney: 1500,
    playerActions: {
        Nothing: {
            id: 0,
            type: "nothing",
        },
        BuyHouse: {
            id: 1,
            type: "buy-house",
        },
        BuyHotel: {
            id: 2,
            type: "buy-hotel",
        }
    },
}

module.exports = Settings;
