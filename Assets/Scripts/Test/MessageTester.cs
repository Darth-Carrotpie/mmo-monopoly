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
}
