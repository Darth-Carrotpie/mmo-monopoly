using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Messages;

public class BoardNetwork : MonoBehaviour
{
    public BoardTile[] tiles;
    private string nodeDataFileName = "board-message.json";
    string filePath;
    private void Awake()
    {
        EventManager.StartListening(EventName.System.NetworkUpdateReceived(), LoadNodeData);
        filePath = Path.Combine(Application.streamingAssetsPath, nodeDataFileName);
        Debug.Log("Start, Loading mockup json");
        LoadNodeData(GameMessage.Write());
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            EventManager.TriggerEvent(EventName.System.NetworkUpdateReceived(), GameMessage.Write());
        }
    }
    private void LoadNodeData(GameMessage msg)
    {
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            //Debug.Log("json: " + dataAsJson);
            BoardTileDataSerializable loadedData = JsonUtility.FromJson<BoardTileDataSerializable>(dataAsJson);

            tiles = loadedData.Export();
        }
        else
        {
            Debug.Log(filePath);
            Debug.LogError("Cannot load game data! - no file found");
        }

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
