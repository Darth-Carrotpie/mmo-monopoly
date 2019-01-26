const Settings = {
    // GAME
    boardSize: 256,
    initialCost: 100,
    costGrow: 20,
    hotelCostMultiplier: 2,
    diceMin: 1,
    diceMax: 6,
    diceCount: 2,
    doubleBonus: 100,
    // TILE
    tileColors: [
        "orange",
        "blue",
        "green",
        "red",
        "purple",
        "gold",
        "orange2",
        "blue2",
        "green2",
        "red2",
        "purple2",
        "gold2",
        "orange3",
        "blue3",
        "green3",
        "red3",
        "purple3",
        "gold3",
    ],
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
