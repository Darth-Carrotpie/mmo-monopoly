const Settings = require("./settings");

const tileTypes = Object.values(Settings.tileTypes);
const totalChance = (place) => {
    const chances = tileTypes.map((tc) => tc.chance + place * tc.chanceIncrease);
    return chances.reduce((a, b) => a + b, 0);
}

const getTypeId = (type) => {
    return tileTypes.find((t) => t.type == type).id;
}

const randomItem = (array) => {
    return array[Math.floor(Math.random() * array.length)];
}


class Tile {
    constructor(type, properties) {
        this.type = type,
            this.properties = properties;
    }

    getState() {
        const state = { ...this };
        state.typeId = getTypeId(state.type);
        return state;
    }

    static Empty() {
        return new Tile(Tile.Type.Empty, {});
    }

    static Street(cost, color) {
        return new Tile(Tile.Type.Street, { cost, color });
    }

    static Go() {
        return new Tile(Tile.Type.Go);
    }

    static Pay(cost) {
        return new Tile(Tile.Type.Pay, { cost });
    }

    static RandomColor() {
        return randomItem(Settings.tileColors);
    }

    static RandomColorExcept(color) {
        const colors = Settings.tileColors.map((c) => c);
        colors.splice(colors.indexOf(color), 1);
        return colors[Math.floor(Math.random() * colors.length)];
    }

    static RandomType(place) {
        let randomValue = Math.random() * totalChance(place);

        for (let i = 0; i < tileTypes.length; i++) {
            const tc = tileTypes[i];

            randomValue -= tc.chance + place * tc.chanceIncrease;
            if (randomValue < 0) {
                return tc.type;
            }
        }

        console.error("Bad algorythm");
        return Tile.Type.Empty;
    }
}

Tile.Type = {
    Street: Settings.tileTypes.Street.type,
    Empty: Settings.tileTypes.Empty.type,
    Go: Settings.tileTypes.Go.type,
    Pay: Settings.tileTypes.Pay.type,
}

module.exports = Tile;
