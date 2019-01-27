using System.Collections;
using System.IO;
using Messages;
using UnityEngine;

public interface IMessenger {
    void Connect(System.Action<State> onStateReceived, System.Action<Tile[]> onBoardReceived);
    void SelectAction(Action action, int turnCount);
}

public class DummyMessenger : IMessenger
{
    private MonoBehaviour owner;
    private string basePath;
    private System.Action<State> onStateReceived;
    private System.Action<Tile[]> onBoardReceived;

    public DummyMessenger(MonoBehaviour owner) {
        this.owner = owner;
        this.basePath = Path.Combine(Application.streamingAssetsPath, "TestRun/");
    }

    public void Connect(System.Action<State> onStateReceived, System.Action<Tile[]> onBoardReceived)
    {
        this.onStateReceived = onStateReceived;
        this.onBoardReceived = onBoardReceived;

        string boardJson = File.ReadAllText(Path.Combine(this.basePath, "board.json"));
        Message.ParseMessage(boardJson, this.onStateReceived, this.onBoardReceived);

        string stateJson = File.ReadAllText(Path.Combine(this.basePath, "state0.json"));
        Message.ParseMessage(stateJson, this.onStateReceived, this.onBoardReceived);

        this.owner.StartCoroutine(this.RunGame());
    }

    IEnumerator RunGame() {
        for (int i = 1; i <= 10; i++)
        {
            yield return new WaitForSeconds(10);

            string stateName = "state" + i + ".json";
            string stateJson = File.ReadAllText(Path.Combine(this.basePath, stateName));
            Message.ParseMessage(stateJson, this.onStateReceived, this.onBoardReceived);
        }
    }

    public void SelectAction(Action action, int turnCount)
    {
        // Don't care
    }
}

public class SocketMessenger : IMessenger {
    private const string serverUrl = "ws://localhost";
    private const string serverPort = "2048";
    private const string serverProtocol = "ggj-protocol";

    private MonoBehaviour owner;
    private System.Action<State> onStateReceived;
    private System.Action<Tile[]> onBoardReceived;
    private WebSocket ws;

    public SocketMessenger(MonoBehaviour owner) {
        this.owner = owner;
    }

    public void Connect(System.Action<State> onStateReceived, System.Action<Tile[]> onBoardReceived)
    {
        this.onStateReceived = onStateReceived;
        this.onBoardReceived = onBoardReceived;

        this.owner.StartCoroutine(this.ConnectSocket());
    }

    public void SelectAction(Action action, int turnCount)
    {
        if (ws == null || !ws.IsConnected()) {
            Debug.LogError("No connection to send message to");
            return;
        }

        // TODO pass turn count
        string message = IntentMessage.MakeIntentMessage(action, turnCount);
        ws.SendString(message);
    }

    private IEnumerator ConnectSocket() {
		ws = new WebSocket(new System.Uri(serverUrl + ":" + serverPort), new string[1] { serverProtocol });
		yield return this.owner.StartCoroutine(ws.Connect());
		Debug.Log("Connected to server");
		this.owner.StartCoroutine(this.ListenSocket());
    }

    private IEnumerator ListenSocket() {
		while (true)
		{
			if (!ws.IsConnected() || ws.error != null)
			{
				Close();
				yield break;
			}

			string reply;
			while((reply = ws.RecvString()) != null)
			{
                Message.ParseMessage(reply, this.onStateReceived, this.onBoardReceived);
			}

			yield return null;
		}
    }

    private void Close()
	{
        Debug.Log("Connection closed");
		ws.Close();
	}
}
