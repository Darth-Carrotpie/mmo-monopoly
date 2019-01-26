const Settings = require("./settings");

const tileTypes = Object.values(Settings.tileTypes);
const totalChance = tileTypes.map((tc) => tc.chance).reduce((a, b) => a + b, 0);

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

    static RandomColor() {
        return randomItem(Settings.tileColors);
    }

    static RandomColorExcept(color) {
        const colors = Settings.tileColors.map((c) => c);
        colors.splice(colors.indexOf(color), 1);
        return colors[Math.floor(Math.random() * colors.length)];
    }

    static RandomType() {
        let randomValue = Math.random() * totalChance;

        for (let i = 0; i < tileTypes.length; i++) {
            const tc = tileTypes[i];

            randomValue -= tc.chance;
            if (randomValue < 0) {
                return tc.type;
            }
        }

        console.error("Bad algorythm");
        return Tile.Type.Empty;
    }
}

Tile.Type = {
    Street: "street",
    Empty: "empty"
}

module.exports = Tile;
