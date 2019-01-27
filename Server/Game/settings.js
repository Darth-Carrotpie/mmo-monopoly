const Settings = {
    // BOARD
    boardSize: 512,
    initialCost: 100,
    costGrow: 20,
    hotelCostMultiplier: 2,
    goReward: 200,
    payMultiplier: 0.01,
    // GAME
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
            chance: 5,
            chanceIncrease: 0,
        },
        Empty: {
            id: 1,
            type: "empty",
            chance: 1,
            chanceIncrease: 0,
        },
        Go: {
            id: 2,
            type: "go",
            chance: 1,
            chanceIncrease: 0,
        },
        Pay: {
            id: 3,
            type: "pay",
            chance: 1,
            chanceIncrease: 0.02,
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
    // TRANSACTION
    transactions: {
        RollDouble: {
            id: 0,
            type: "roll-double",
        },
        PassGo: {
            id: 1,
            type: "pass-go",
        },
        PayRent: {
            id: 2,
            type: "pay-rent",
        },
        ReceiveRent: {
            id: 3,
            type: "receive-rent",
        },
        PayTax: {
            id: 4,
            type: "pay-tax",
        },
        PayBuild: {
            id: 5,
            type: "pay-build",
        }
    },
}

module.exports = Settings;
