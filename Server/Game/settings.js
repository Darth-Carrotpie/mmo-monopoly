const Settings = {
    boardSize: 16,
    initialCost: 100,
    costGrow: 50,
    tileTypes: {
        Street: {
            type: "street",
            chance: 5
        },
        Empty: {
            type: "empty",
            chance: 1
        }
    }
}

module.exports = Settings;
