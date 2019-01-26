using System;
using UnityEngine;

namespace Messages
{

[System.Serializable]
public class Message {
    public string messageType;

    public static void ParseMessage(string json, Action<State> onStateReceived, Action<Tile[]> onBoardReceived) {
        try
        {
            Message message = JsonUtility.FromJson<Message>(json);
            switch (message.messageType)
            {
                case "state":
                    StateMessage stateMessage = JsonUtility.FromJson<StateMessage>(json);
                    onStateReceived(stateMessage.state);
                    break;
                case "board":
                    BoardMessage boardMessage = JsonUtility.FromJson<BoardMessage>(json);
                    onBoardReceived(boardMessage.board);
                    break;
                default:
                    throw new Exception("Unsupported messageType: " + message.messageType);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to parse: " + json);
            Debug.LogError(ex.Message);
        }
    }
}

public enum Action {
    Nothing = 0,
    BuyHouse = 1,
    BuyHotel = 2
}

[System.Serializable]
public class Intent {
    public Action actionId;
    public int turnCount;
}

[System.Serializable]
public class IntentMessage : Message {
    public Intent intent;

    public static string MakeIntentMessage(Action action, int turnCount) {
        IntentMessage message = new IntentMessage();
        message.messageType = "intent";
        message.intent = new Intent();
        message.intent.turnCount = turnCount;
        message.intent.actionId = action;

        return JsonUtility.ToJson(message);
    }
}

[System.Serializable]
public class PossibleAction {
    public string type;
    public Action id;
    public int cost;
}

[System.Serializable]
public class Player {
    public int id;
    public int position;
    public int[] houses;
    public int[] hotels;
    public int cash;
    public int[] roll;
    public PossibleAction[] possibleActions;
}

[System.Serializable]
public class State {
    public int turnCount;
    public Player[] players;
    public Player me;
}

[System.Serializable]
public class StateMessage : Message {
    public State state;
}

// All possible properties for a tile, since can't have conditional parsing
[System.Serializable]
public class TileProperties {
    public int cost;
    public string color;
}

public enum TileType {
    Street = 0,
    Empty = 1
}

[System.Serializable]
public class Tile {
    public string type;
    public TileProperties properties;
    public TileType typeId;
}

[System.Serializable]
public class BoardMessage : Message {
    public Tile[] board;
}

}

