const Settings = require("./settings");

const transactions = Object.values(Settings.transactions);
const getTransactionId = (type) => {
    return transactions.find((t) => t.type == type).id;
}

class Transaction {
    constructor(type, properties) {
        this.type = type;
        this.typeId = getTransactionId(type);
        this.properties = properties;
    }

    static RollDouble(player, initialPosition) {
        player.transactions.push(new Transaction(Transaction.Type.RollDouble, { gain: Settings.doubleBonus, position: initialPosition }));
    }

    static PassGo(player, tileIndex) {
        player.transactions.push(new Transaction(Transaction.Type.PassGo, { gain: Settings.goReward, position: tileIndex }));
    }

    static PayRent(payer, cost) {
        payer.transactions.push(new Transaction(Transaction.Type.PayRent, { loss: cost, position: payer.position }));
    }

    static ReceiveRent(payer, receiver, cost) {
        receiver.transactions.push(new Transaction(Transaction.Type.ReceiveRent, { gain: cost, payerId: payer.id, position: payer.position }));
    }

    static PayTax(player, cost) {
        player.transactions.push(new Transaction(Transaction.Type.PayRent, { loss: cost, position: player.position }));
    }

    static PayBuild(player, cost) {
        player.transactions.push(new Transaction(Transaction.Type.PayBuild, { loss: cost, position: player.position }));
    }
}

Transaction.Type = {
    RollDouble: Settings.transactions.RollDouble.type,
    PassGo: Settings.transactions.PassGo.type,
    PayRent: Settings.transactions.PayRent.type,
    ReceiveRent: Settings.transactions.ReceiveRent.type,
    PayTax: Settings.transactions.PayTax.type,
    PayBuild: Settings.transactions.PayBuild.type,
}

module.exports = Transaction;
