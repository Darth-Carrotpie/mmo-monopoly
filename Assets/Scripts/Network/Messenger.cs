using System.Collections;
using System.IO;
using Messages;
using UnityEngine;

public interface IMessenger {
    void Connect(System.Action<State> onStateReceived, System.Action<Tile[]> onBoardReceived);
    void SelectAction(Action action);
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

    public void SelectAction(Action action)
    {
        // Don't care
    }
}
