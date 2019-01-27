using System.IO;
using UnityEngine;
using UnityEditor;
using Messages;

static class Tester {

    [MenuItem("Test/Parse Intent Message")]
    static void ParseIntentMessage() {
        string filePath = Path.Combine(Application.streamingAssetsPath, "intent-message.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log("json: " + dataAsJson);
            IntentMessage message = JsonUtility.FromJson<IntentMessage>(dataAsJson);
            Debug.Log(message.intent);
        }
    }

    [MenuItem("Test/Serialize Intent Message")]
    static void SerializeIntentMessage() {
        IntentMessage message = new IntentMessage();
        message.messageType = "intent";
        message.intent = new Intent();
        message.intent.turnCount = 5;
        message.intent.actionId = Action.BuyHotel;

        string json = JsonUtility.ToJson(message);
        Debug.Log(json);
    }

    [MenuItem("Test/Make Intent Message")]
    static void MakeIntentMessage() {
        string json = IntentMessage.MakeIntentMessage(Action.BuyHouse, 4);
        Debug.Log(json);
    }

    [MenuItem("Test/Parse State Message")]
    static void ParseStateMessage() {
        string filePath = Path.Combine(Application.streamingAssetsPath, "state-message.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log("json: " + dataAsJson);
            StateMessage message = JsonUtility.FromJson<StateMessage>(dataAsJson);
            Debug.Log(message.state.players.Length);
            Debug.Log(message.state.me.cash);
        }
    }

    [MenuItem("Test/Parse Board Message")]
    static void ParseBoardMessage() {
        string filePath = Path.Combine(Application.streamingAssetsPath, "board-message.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log("json: " + dataAsJson);
            BoardMessage message = JsonUtility.FromJson<BoardMessage>(dataAsJson);
            Debug.Log(message.board.Length);
        }
    }

    [MenuItem("Test/Handle Board Message")]
    static void HandleBoardMessage() {
        string filePath = Path.Combine(Application.streamingAssetsPath, "board-message.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Message.ParseMessage(dataAsJson,
            (State state) => {
                Debug.Log("Getting state");
            }, (Tile[] tiles) => {
                Debug.Log("Getting tiles");
                Debug.Log(tiles.Length);
            });
        }
    }
}

public class MessageTester : MonoBehaviour {
    private IMessenger messenger;

    void Start() {
        this.messenger = new SocketMessenger(this);
        this.messenger.Connect(this.OnStateReceived, this.OnBoardReceived);
    }

    void OnStateReceived(State state) {
        Debug.Log(
            "Receiving state for turn: " + state.turnCount + " Players: " + state.players.Length + "\n" +
            "Cash: " + state.me.cash +
            " Position: " + state.me.position +
            " Roll: " + MessageTester.ArrayString(state.me.roll) +
            " Houses: " + MessageTester.ArrayString(state.me.houses) +
            " Hotels: " + MessageTester.ArrayString(state.me.hotels)
        );
    }

    void OnBoardReceived(Tile[] tiles) {
        int streets = 0;
        int empties = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            if (tile.typeId == TileType.Street) {
                streets++;
            }
            if (tile.typeId == TileType.Empty) {
                empties++;
            }
        }
        Debug.Log(
            "Board received.\n" +
            "Tiles: " + tiles.Length + " Streets: " + streets + " Empty: " + empties
        );
    }

    static string ArrayString(int[] array) {
        if (array.Length == 0) {
            return "[]";
        }

        string s = "[";
        for (int i = 0; i < array.Length; i++)
        {
            s += array[i] + ", ";
        }
        s = s.Remove(s.Length - 2);
        s += "]";
        return s;
    }
}
